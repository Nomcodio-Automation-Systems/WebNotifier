using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using WatiN.Core;
using System.Diagnostics;
using SHDocVw;
using System.Windows.Forms;
using System.Collections.Specialized;
//using mshtml;
namespace WebNotifier
{
    class WebWorker : IDisposable
    {
        Form1 view = null;
        string[] pages = new string[10];
        Thread thread_pages = null;

        private Dictionary<string, LinkedList<DiffItem>> dictionary =
        new Dictionary<string, LinkedList<DiffItem>>();

        private volatile bool _shouldStop;
        private volatile bool _watchDogStop;
        private volatile bool _started;
        private double waittime = 10;
        private string last_adresse = "";
        private bool debug = true;


        public WebWorker(Form1 other)
        {
            view = other;
            if (WebNotifier.Default.contentList == null)
            {
                WebNotifier.Default.contentList = new StringCollection();
            }

            if (WebNotifier.Default.webpageList == null)
            {
                WebNotifier.Default.webpageList = new StringCollection();
            }
        }
        public Form1 View
        {
            set => view = value;
            get => view;

        }
        public bool Running => _started;
        ~WebWorker()
        {
            Dispose();
            WebNotifier.Default.Save();
        }
        public Dictionary<string, LinkedList<DiffItem>> DIC
        {
            get => dictionary;
            set => dictionary = value;
        }
        private string[] Differ(string source1, string source2)
        {


            List<string> diff;
            IEnumerable<string> set1 = source1.Split(' ').Distinct();
            IEnumerable<string> set2 = source2.Split(' ').Distinct();

            if (set2.Count() > set1.Count())
            {
                diff = set2.Except(set1).ToList();
            }
            else
            {
                diff = set1.Except(set2).ToList();
            }
            return diff.ToArray();
        }
        private void NotEqual(string address, string new_webpage, string title, int index)
        {
            LinkedList<DiffItem> last = null;

            try
            {
                last = dictionary[address];
            }
            catch
            {

            }
            if (last == null)
            {
                try
                {
                    dictionary[address] = Analyse(pages[index], new_webpage);
                    return;
                }
                catch
                {

                }
            }

            LinkedList<DiffItem> now = Analyse(pages[index], new_webpage);
            bool result = StringAddon.Compare(last, now);
            if (!last.First.Value.NoDiff && now.ToArray<DiffItem>()[0].Start == 0 && now.ToArray<DiffItem>()[1].Start == 0)
            {
                if (result == true)
                {
                    if (now.ToArray<DiffItem>()[0].Start == 0 && now.ToArray<DiffItem>()[1].Start == 0)
                    {
                        dictionary[address] = last;
                    }
                    else
                    {

                        dictionary[address] = now;
                    }
                    pages[index] = new_webpage;
                    result = false;
                }
            }

            if (result)
            {

                lock (this)
                {
                    LastAddresse = address;
                    WebNotifier.Default.webpageList.Insert(index, address);
                    WebNotifier.Default.contentList.Insert(index, new_webpage);
                    view.SetText(title, address);
                    //String alpha = null;
                    //while ( alpha == null )
                    //{
                    //    alpha = collectInfo(address);
                    //}
                    //String beta = null;
                    //while ( beta == null )
                    //{
                    //    beta = collectInfo(address);
                    //}

                    //now = StringAddon.Analyse(alpha, beta);
                    if (debug)
                    {
                        MessageBox.Show(StringAddon.ShowDiff(pages[index], new_webpage), "Debug: " + title);
                    }

                    dictionary[address] = now;
                    pages[index] = new_webpage;
                }
                view.notifyIcon1.BalloonTipText = "HP Changed " + title + "";
                view.notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                view.notifyIcon1.BalloonTipTitle = "Alert!";
                view.notifyIcon1.ShowBalloonTip(500000);
            }
        }
        private void OuterCode(IE browser, TimeSpan span, int index)
        {
            if (browser == null)
            {
                return;
            }

            string one = (string)view.listBox2.Items[index];
            if (one != null && one != "")
            {
                try
                {
                    browser.GoTo(one);
                }
                catch (Exception e3)
                {

                }
                try
                {
                    browser.WaitForComplete();
                }
                catch (Exception e3)
                {

                }


                string one_s = browser.Body.Text;


                lock (this)
                {
                    string old_addresse = null;
                    try
                    {
                        old_addresse = WebNotifier.Default.webpageList[index];
                    }
                    catch
                    {

                    }

                    if (old_addresse != null)
                    {
                        if (view.textBox1.Text == old_addresse)
                        {
                            try
                            {
                                pages[index] = WebNotifier.Default.contentList[index];
                            }
                            catch
                            {

                            }
                        }
                    }
                }


                string title = browser.Title;
                if (title == null)
                {
                    title = "Unknown";
                    return;
                }
                if (pages[index] != null)
                {
                    if (!pages[index].Equals(one_s))
                    {

                        NotEqual(one, one_s, title, index);

                    }

                }
                else
                {
                    if (one_s != null && one_s != "")
                    {
                        pages[index] = one_s;
                        lock (this)
                        {

                            WebNotifier.Default.webpageList.Insert(index, one);
                            WebNotifier.Default.contentList.Insert(index, one_s);
                        }
                    }
                }
            }
            Thread.Sleep(span);
        }
        public string CollectInfo(string address)
        {
            string one_s = null;
            try
            {
                // IE browser = new IE();
                using (IE browser = new IE())
                {
                    try
                    {
                        browser.GoTo(address);
                    }
                    catch (Exception e3)
                    {

                    }
                    try
                    {
                        browser.WaitForComplete();
                    }
                    catch (Exception e3)
                    {

                    }


                    one_s = browser.Body.Text;

                }
            }
            catch
            {

            }
            return one_s;
        }
        public LinkedList<DiffItem> Analyse(string source1, string source2)
        {
            LinkedList<DiffItem> uerror = StringAddon.Analyse(source1, source2);
            //  String[] paku = this.Differ(source1, source2);
            //  String result = string.Join(" : . : ",paku);
            //  MessageBox.Show(result,"Diffrence");
            return uerror;
        }
        private void MeasureInterference()
        {
            int max = 5;
            for (int i = 0; i < view.listBox2.Items.Count; i++)
            {
                string address = (string)view.listBox2.Items[i];
                LinkedList<DiffItem> last = null;
                try
                {
                    last = dictionary[address];
                }
                catch
                {

                }

                if (last == null)
                {

                    string alpha = null;
                    string beta = null;

                    while (alpha == null)
                    {
                        alpha = CollectInfo(address);
                    }

                    // if((alpha.First()<DiffItem>
                    int m = 0;
                    do
                    {
                        m++;
                        while (beta == null)
                        {
                            beta = CollectInfo(address);
                        }

                    } while (beta.Equals(alpha) && m < max);

                    if (!alpha.Equals(beta))
                    {

                        LinkedList<DiffItem> now = StringAddon.Analyse(alpha, beta);
                        if (now == null)
                        {
                            MessageBox.Show("S336 Webworker Error NULL should not happened",
    "DebugMessage");
                        }

                        dictionary[address] = now;

                    }
                    else
                    {
                        DiffItem dummy = new DiffItem(0, 0)
                        {
                            NoDiff = true
                        };
                        LinkedList<DiffItem> now = new LinkedList<DiffItem>();
                        now.AddFirst(dummy);


                        dictionary[address] = now;
                    }
                    pages[i] = beta;
                }
            }
        }
        // This method will be called when the thread is started.
        public void DoWork()
        {
            lock (this)
            {
                _started = true;
            }
            MeasureInterference();
            try
            {
                // IE browser = new IE();
                using (IE browser = new IE())
                {

                    while (!_shouldStop)
                    {
                        if (view.checkBox1.Checked)
                        {
                            browser.ShowWindow(WatiN.Core.Native.Windows.NativeMethods.WindowShowStyle.Hide);
                        }
                        else
                        {
                            browser.ShowWindow(WatiN.Core.Native.Windows.NativeMethods.WindowShowStyle.ShowMaximized);
                        }

                        int k = (int)waittime;
                        double rest = waittime % (double)k;
                        double secounds = 60.0d * rest;
                        TimeSpan span = new TimeSpan(0, (int)waittime / 5, (int)secounds / 5);


                        for (int i = 0; i < view.listBox2.Items.Count; i++)
                        {
                            OuterCode(browser, span, i);

                        }
                    }
                }
            }
            catch (Exception e3)
            {
                _started = false;
            }

        }

        public void WatchDog()
        {
            _shouldStop = false;
            _watchDogStop = false;

            thread_pages = new Thread(new ThreadStart(DoWork));
            thread_pages.SetApartmentState(ApartmentState.STA);
            thread_pages.Start();
            Thread.Sleep(1000);

            while (!_watchDogStop)
            {
                if (!thread_pages.IsAlive && !_shouldStop)
                {
                    thread_pages = new Thread(new ThreadStart(DoWork));
                }
                Thread.Sleep(1000);
            }
            _watchDogStop = true;
            thread_pages.Abort();
            Thread.Sleep(2000);
            _shouldStop = true;
        }
        public void StopWatchDog()
        {
            _watchDogStop = true;
            Thread.Sleep(2000);
            _shouldStop = true;
        }
        public void RequestStop()
        {
            _shouldStop = true;
            _started = false;

        }
        public double WaitTime
        {
            get => waittime;
            set => waittime = value;
        }
        public string LastAddresse
        {
            get => last_adresse;
            set => last_adresse = value;
        }
        private string Compare2String(string alpha, string beta)
        {


            List<string> diff;
            IEnumerable<string> set1 = alpha.Split(' ').Distinct();
            IEnumerable<string> set2 = beta.Split(' ').Distinct();

            if (set2.Count() > set1.Count())
            {
                diff = set2.Except(set1).ToList();
            }
            else
            {
                diff = set1.Except(set2).ToList();
            }
            return diff.ToString();
        }
        
        public void Dispose()
        {

            StopWatchDog();
            if (thread_pages != null)
            {
                if (thread_pages.IsAlive)
                {
                    thread_pages.Abort();
                }
            }
        }
    }
}
