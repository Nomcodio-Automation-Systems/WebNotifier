﻿using System;
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

namespace WebNotifier
{
    class WebWorker : IDisposable
    {
        Form1 view = null;
        List<string> webpages_content_buffer = new List<string>();
        Thread thread_pages = null;

        private Dictionary<string, List<DiffItem>> webpages_diff_buffer =
        new Dictionary<string, List<DiffItem>>();

        private volatile bool _shouldStop;
        private volatile bool _watchDogStop;
        private volatile bool _started;
        private double waittime = 10;
        private string last_adresse = "";
        private bool debug = true;
        private int waitforcomplete = 10; // sec
        private string which_browser = "webbrowser";

        public WebWorker(Form1 other)
        {
            view = other;

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
        public Dictionary<string, List<DiffItem>> DIC
        {
            get => webpages_diff_buffer;
            set => webpages_diff_buffer = value;
        }
        public List<string> PAGES
        {
            get => webpages_content_buffer;
            set => webpages_content_buffer = value;
        }
        private string[] Differ2Strings(string source1, string source2)
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
        private void NotEqual(string address, string new_webpage_content, string title, int index)
        {
            List<DiffItem> last = null;
            StringAddon add = new StringAddon();
            try
            {
                last = webpages_diff_buffer[address];
            }
            catch
            {

            }
            if (last == null)
            {
                try
                {
                    StringAddon strm = new StringAddon();
                    webpages_diff_buffer[address] = Analyse(strm.GetSafeFromList(webpages_content_buffer,index), new_webpage_content);
                    return;
                }
                catch
                {

                }
            }
            StringAddon str = new StringAddon();
            List<DiffItem> now = Analyse(str.GetSafeFromList(webpages_content_buffer,index), new_webpage_content);
            bool result = add.Compare2DiffItemLists(last, now);
            if (!last[0].NoDiff && now[0].Start == 0 && now[1].Start == 0)
            {
                if (result == true)
                {
                    if (now.ToArray<DiffItem>()[0].Start == 0 && now.ToArray<DiffItem>()[1].Start == 0)
                    {
                        webpages_diff_buffer[address] = last;
                    }
                    else
                    {

                        webpages_diff_buffer[address] = now;
                    }
                    str.InsertSafeList(webpages_content_buffer, new_webpage_content, index);
                   
                    result = false;
                }
            }

            if (result)
            {

                lock (this)
                {
                    LastAddresse = address;
                    add.InsertSafeCollection(WebNotifier.Default.webpageList, address, index);
                    //WebNotifier.Default.webpageList.Insert(index, address);
                    add.InsertSafeCollection(WebNotifier.Default.contentList, new_webpage_content, index);
                    //WebNotifier.Default.contentList.Insert(index, new_webpage_content);
                    view.SetTextList1(title, address);
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

                        MessageBox.Show(add.ShowDiff(webpages_content_buffer[index], new_webpage_content), "Debug: " + title);
                    }
                    
                    webpages_diff_buffer[address] = now;
                   str.InsertSafeList(webpages_content_buffer,new_webpage_content,index);
                }
                if (!view.checkBox3.Checked)
                {
                    view.notifyIcon1.BalloonTipText = "HP Changed " + title + "";
                    view.notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                    view.notifyIcon1.BalloonTipTitle = "Alert!";
                    view.notifyIcon1.ShowBalloonTip(500000);
                }
            }
        }
        private void OuterCode(WebBrowserFacade browser, TimeSpan span, int index)
        {
            if (browser == null)
            {
                return;
            }

            string web_addresse = (string)view.listBox2.Items[index];

            if (web_addresse != null && web_addresse != "")
            {
                try
                {
                    browser.GoToNoWait(web_addresse);
                }
                catch (Exception e3)
                {

                }
                try
                {
                    browser.WaitForComplete(waitforcomplete);
                }
                catch (Exception e3)
                {

                }


                string webpage_content = browser.Body.Text;


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
                                StringAddon str = new StringAddon();
                               str.InsertSafeList(webpages_content_buffer,WebNotifier.Default.contentList[index], index);
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
                if (webpages_content_buffer.ElementAtOrDefault(index) != null)
                {
                    if (!webpages_content_buffer[index].Equals(webpage_content))
                    {

                        NotEqual(web_addresse, webpage_content, title, index);

                    }

                }
                else
                {
                    if (webpage_content != null && webpage_content != "")
                    {
                        StringAddon str = new StringAddon();
                        str.InsertSafeList(webpages_content_buffer,webpage_content,index);
                        lock (this)
                        {

                            WebNotifier.Default.webpageList.Insert(index, web_addresse);
                            WebNotifier.Default.contentList.Insert(index, webpage_content);
                        }
                    }
                }
            }
            Thread.Sleep(span);
        }
        public string CollectInfo(string address)
        {
            string new_webpage_content = null;
            try
            {
                // IE browser = new IE();
                using (WebBrowserFacade browser = (new WebBrowserFactory()).BrowserFactory(which_browser, this))
                {
                    if (view.checkBox1.Checked)
                    {
                        browser.ShowWindow(false);
                    }
                    else
                    {
                        browser.ShowWindow(true);
                    }

                    try
                    {
                        browser.GoToNoWait(address);
                    }
                    catch (Exception e3)
                    {

                    }
                    try
                    {
                        browser.WaitForComplete(waitforcomplete);
                    }
                    catch (Exception e3)
                    {

                    }


                    new_webpage_content = browser.Body.Text;

                }
            }
            catch
            {
                CollectInfo(address);
            }
            return new_webpage_content;
        }
        public List<DiffItem> Analyse(string source1, string source2)
        {
            StringAddon add = new StringAddon();
            List<DiffItem> diff_list = add.Analyse(source1, source2);
            //  String[] paku = this.Differ(source1, source2);
            //  String result = string.Join(" : . : ",paku);
            //  MessageBox.Show(result,"Difference");
            return diff_list;
        }
        private void MeasureInterference()
        {
            int max_trys = 5;
            StringAddon str = new StringAddon();

            for (int i = 0; i < view.listBox2.Items.Count; i++)
            {
                string address = (string)view.listBox2.Items[i];
                List<DiffItem> last_diff = null;
                try
                {
                   
                    last_diff = str.GetSafeObjectFromDic(webpages_diff_buffer, address);
                }
                catch
                {

                }

                if (last_diff == null)
                {

                    string alpha_content = null;
                    string beta_content = null;

                    while (alpha_content == null)
                    {
                        alpha_content = CollectInfo(address);
                    }

                    int cut_off = 0;
                    do
                    {
                        cut_off++;
                        while (beta_content == null)
                        {
                            beta_content = CollectInfo(address);
                        }

                    } while (beta_content.Equals(alpha_content) && cut_off < max_trys);

                    if (!alpha_content.Equals(beta_content))
                    {

                        StringAddon add = new StringAddon();
                        List<DiffItem> now = add.Analyse(alpha_content, beta_content);
                        if (now == null)
                        {
                            //TODO:Replace with Utilities 
                            MessageBox.Show(" Webworker Error NULL should not happened",
    "DebugMessage");
                        }

                        webpages_diff_buffer[address] = now;

                    }
                    else
                    {
                        DiffItem dummy_item = new DiffItem(0, 0)
                        {
                            NoDiff = true
                        };
                        List<DiffItem> now = new List<DiffItem>
                        {
                            dummy_item
                        };
                        
                        webpages_diff_buffer[address] = now;
                    }
                   
                    str.InsertSafeList(webpages_content_buffer,string.Copy(beta_content), i);
                    alpha_content = null;
                    beta_content = null;
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
                using (WebBrowserFacade browser = (new WebBrowserFactory()).BrowserFactory(which_browser, this))
                {

                    while (!_shouldStop)
                    {
                        if (view.checkBox1.Checked)
                        {
                            browser.ShowWindow(false);
                        }
                        else
                        {
                            browser.ShowWindow(true);
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
