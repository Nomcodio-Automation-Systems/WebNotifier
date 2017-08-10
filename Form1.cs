using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using WatiN.Core;

namespace WebNotifier
{
    public partial class Form1 : System.Windows.Forms.Form
    {

        private WebWorker webi = null;
        Thread thread_web = null;
        private bool showflag = false;
        delegate void SetTextCallback1(string text, string url);
        delegate void SetTextCallback2(string text);
        public Form1()
        {
            InitializeComponent();
            notifyIcon1.Icon = new Icon(GetType(), "uicon.ico");
            webi = new WebWorker(this);
            textBox1.Text = WebNotifier.Default.textBox1;
            //textBox2.Text = WebNotifier.Default.textBox2;
            //textBox3.Text = WebNotifier.Default.textBox3;
            //textBox4.Text = WebNotifier.Default.textBox4;
            //textBox5.Text = WebNotifier.Default.textBox5;
            textBox6.Text = WebNotifier.Default.textBox6;
            if (textBox6.Text == null || textBox6.Text == "")
            {
                textBox6.Text = "5";
            }
            checkBox1.Checked = WebNotifier.Default.checkBox1;
            checkBox2.Checked = WebNotifier.Default.checkBox2;
            checkBox3.Checked = WebNotifier.Default.PopUp;
            if (WebNotifier.Default.webpageList != null)
            {
                foreach (string alpha_value in WebNotifier.Default.webpageList)
                {
                    listBox2.Items.Add(alpha_value);
                }

            }
            if (WebNotifier.Default.contentList == null)
            {
                WebNotifier.Default.contentList = new StringCollection();
            }

            if (WebNotifier.Default.webpageList == null)
            {
                WebNotifier.Default.webpageList = new StringCollection();
            }
            if (WebNotifier.Default.diffrentList == null)
            {
                WebNotifier.Default.diffrentList = new StringCollection();
            }
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            WebNotifier.Default.textBox1 = textBox1.Text;
            WebNotifier.Default.Save();
        }

        //private void textBox2_TextChanged(object sender, EventArgs e)
        //{
        //    WebNotifier.Default.textBox2 = textBox2.Text;
        //    WebNotifier.Default.Save();
        //}

        //private void textBox3_TextChanged(object sender, EventArgs e)
        //{
        //    WebNotifier.Default.textBox3 = textBox3.Text;
        //    WebNotifier.Default.Save();
        //}

        //private void textBox4_TextChanged(object sender, EventArgs e)
        //{
        //    WebNotifier.Default.textBox4 = textBox4.Text;
        //    WebNotifier.Default.Save();
        //}

        //private void textBox5_TextChanged(object sender, EventArgs e)
        //{
        //    WebNotifier.Default.textBox5 = textBox5.Text;
        //    WebNotifier.Default.Save();
        //}

        private void button1_Click(object sender, EventArgs e)
        {


            double nn;
            bool isNumeric = int.TryParse(textBox6.Text, out int n);
            if (!isNumeric)
            {
                bool isDouble = double.TryParse(textBox6.Text, out nn);
                if (!isDouble)
                {
                    MessageBox.Show("Setting for the wait time isn't a number");
                    return;
                }

            }
            else
            {
                nn = (double)n;
            }

            lock (webi)
            {
                if (!webi.Running)
                {
                    webi.WaitTime = nn;

                    thread_web = new Thread(new ThreadStart(webi.WatchDog));
                    thread_web.SetApartmentState(ApartmentState.STA);
                    thread_web.Start();
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            webi.StopWatchDog();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowInTaskbar = true;
            notifyIcon1.Visible = false;
            Show();
            WindowState = FormWindowState.Normal;
            showflag = true;
            SafeNativeMethods.Show2(this);

            // this.TopMost = true;
            // this.TopMost = false;
            //ShowMe.Show(this);
            // Set focus to the control, if it can receive focus.


        }
        private void notifyIcon1_Click_1(object sender, System.EventArgs e)
        {
            ShowInTaskbar = true;
            notifyIcon1.Visible = false;
            Show();
            WindowState = FormWindowState.Normal;
            showflag = true;
            SafeNativeMethods.Show2(this);
            // ShowMe.Show2(this);
            //this.TopMost = true;
            // this.TopMost = false;
            // ShowMe.Show(this);
        }

        private void Form1_Resize(object sender, System.EventArgs e)
        {
            if (checkBox2.Checked)
            {
                if (FormWindowState.Minimized == WindowState || showflag)
                {
                    ShowInTaskbar = false;
                    notifyIcon1.Visible = true;
                    //this.notifyIcon1.ContextMenu = contextMenu1;

                    //this.notifyIcon1.BalloonTipText = "The quick brown fox. Jump!";
                    //this.notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                    //this.notifyIcon1.BalloonTipTitle = "Alert!";
                    //this.notifyIcon1.ShowBalloonTip(500);
                    showflag = false;
                    Hide();
                }

                else if (FormWindowState.Normal == WindowState)
                {
                    showflag = false;
                    ShowInTaskbar = true;
                    notifyIcon1.Visible = false;
                    Show();
                    WindowState = FormWindowState.Normal;
                    SafeNativeMethods.Show2(this);
                    //ShowMe.Show2(this);
                }
            }
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            //this.ShowInTaskbar = true;
            //this.notifyIcon1.Visible = false;
            //this.WindowState = FormWindowState.Normal;
            //this.showflag = false;
            //this.Show();
            string path = webi.LastAddresse;
            System.Diagnostics.Process.Start(path);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

            WebNotifier.Default.textBox6 = textBox6.Text;
            WebNotifier.Default.Save();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            WebNotifier.Default.checkBox1 = checkBox1.Checked;
            WebNotifier.Default.Save();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            WebNotifier.Default.checkBox2 = checkBox2.Checked;
            WebNotifier.Default.Save();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public void SetTextList1(string text, string url)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (listBox1.InvokeRequired)
            {
                SetTextCallback1 d = new SetTextCallback1(SetTextList1);
                Invoke(d, new object[] { text, url });
            }
            else
            {
                listBox1.BeginUpdate();
                listBox1.Items.Add(new UrlListItem(text, url));
                listBox1.EndUpdate();

            }
        }
        public void SetTextList2(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (listBox2.InvokeRequired)
            {
                SetTextCallback2 d = new SetTextCallback2(SetTextList2);
                Invoke(d, new object[] { text });
            }
            else
            {
                listBox2.BeginUpdate();
                listBox2.Items.Add((text));
                listBox2.EndUpdate();

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox lb = sender as ListBox;


            UrlListItem myObject = (UrlListItem)lb.SelectedItem;
            if (myObject != null)
            {
                System.Diagnostics.Process.Start(myObject.URL);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        { string box = textBox1.Text;
            if (box == null || box == "")
            {
                MessageBox.Show("Setting are no Internet address");
                return;
            }

            if (box != null && box != "")
            {
                if ( !box.StartsWith("http")|| !box.StartsWith("https")){
                    box = "http://" + box;
                }

                bool result = Uri.TryCreate(box, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                if (!result)
                {
                    MessageBox.Show("Setting are no Internet address");
                    return;
                }
            }


            if (WebNotifier.Default.webpageList == null)
            {
                WebNotifier.Default.webpageList = new StringCollection();
            }
            WebNotifier.Default.webpageList.Add(textBox1.Text);


            if (WebNotifier.Default.diffrentList == null)
            {
                WebNotifier.Default.diffrentList = new StringCollection();
            }
            string address = box; ;

            Thread t1 = new Thread
          (delegate ()
          {
              Dictionary<string, List<DiffItem>> dic;
              List<string> list = new List<string>();
              StringAddon add = new StringAddon();
              lock (webi)
              {
                  dic = webi.DIC;
                  list = webi.PAGES;
              }
              string alpha = null;
              while (alpha == null)
              {
                  alpha = webi.CollectInfo(address);
              }
              string beta = null;
              while (beta == null)
              {
                  beta = webi.CollectInfo(address);
              }
              List<DiffItem> now = webi.Analyse(alpha, beta);
              lock (webi)
              {
                  dic[address] = now;
                  list.Add(beta);
                  webi.DIC = dic;
                  webi.PAGES = list;
                  SetTextList2(address);
                  //TODO we need a index 
                  WebNotifier.Default.webpageList.Add(address);
                  WebNotifier.Default.contentList.Add(beta);

                  WebNotifier.Default.Save();
              }
          });
            t1.SetApartmentState(ApartmentState.STA);
            t1.Start();

            textBox1.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string copy = null;
            int i = 0;
            if (WebNotifier.Default.webpageList == null)
            {
                WebNotifier.Default.webpageList = new StringCollection();
            }

            while (listBox2.SelectedItems.Count != 0)

            {
                copy = (string)listBox2.SelectedItems[0];
                listBox2.Items.Remove(listBox2.SelectedItems[0]);
                i++;

            }
            LinkedList<string> List = new LinkedList<string>(WebNotifier.Default.webpageList.Cast<string>());
            if (copy != null)
            {
                List.Remove(copy);
            }
            WebNotifier.Default.webpageList.Clear();
            WebNotifier.Default.webpageList.AddRange(List.ToArray());
            WebNotifier.Default.Save();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string url = "";
            string cmd = "_s-xclick";
            // string business = "test@msn.com";  // your paypal email
            //string description = "Donation";            // '%20' represents a space. remember HTML!
            //string country = "DE";                  // AU, US, etc.
            // string currency = "EUR";                 // AUD, USD, etc.
            string buttonID = "STWQN7AUYV6LA";
            url += "https://www.paypal.com/cgi-bin/webscr" +
                "?cmd=" + cmd +
                //  "&business=" + business +
                "&hosted_button_id=" + buttonID +
            //  "&lc=" + country +
            //   "&item_name=" + description +
            //  "&currency_code=" + currency +
            //    "&bn=" + "PP%2dDonationsBF";
            "";
            System.Diagnostics.Process.Start(url);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            WebNotifier.Default.PopUp = checkBox3.Checked;
            WebNotifier.Default.Save();
        }
    }
}
