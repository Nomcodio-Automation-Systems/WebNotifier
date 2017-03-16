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
        delegate void SetTextCallback(string text, string url);

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
            checkBox1.Checked = WebNotifier.Default.checkBox1;
            checkBox2.Checked = WebNotifier.Default.checkBox2;
            if ( WebNotifier.Default.webpageList != null )
            {
                foreach (string alpha_value in WebNotifier.Default.webpageList )
                {
                    listBox2.Items.Add(alpha_value);
                }

            }
        }
        ~Form1()
        {
            webi.Dispose();
            // thread_web.Abort();
            Dispose();
            
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
            if ( !isNumeric )
            {
                bool isDouble = double.TryParse(textBox6.Text, out nn);
                if ( !isDouble )
                {
                    MessageBox.Show("Setting for the wait time isn't a number");
                    return;
                }

            }
            else
            {
                nn = ( double )n;
            }
           
            lock ( webi )
            {
                if ( !webi.Running )
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
            if (checkBox2.Checked )
            {
                if ( FormWindowState.Minimized == WindowState || showflag)
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

                else if ( FormWindowState.Normal == WindowState)
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
        public void SetText(string text, string url)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (listBox1.InvokeRequired )
            {
                SetTextCallback d = new SetTextCallback(SetText);
                Invoke(d, new object[] { text, url });
            }
            else
            {
                listBox1.BeginUpdate();
                listBox1.Items.Add(new ListItem(text, url));
                listBox1.EndUpdate();

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox lb = sender as ListBox;


            ListItem myObject = ( ListItem )lb.SelectedItem;
            if ( myObject != null )
            {
                System.Diagnostics.Process.Start(myObject.URL);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ( textBox1.Text == null || textBox1.Text == "" )
            {
                MessageBox.Show("Setting are no Internet addresse");
                return;
            }
            if ( textBox1.Text != null && textBox1.Text != "" )
            {
                bool result = Uri.TryCreate(textBox1.Text, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                if ( !result )
                {
                    MessageBox.Show("Setting are no Internet addresse");
                    return;
                }
            }
            listBox2.Items.Add(textBox1.Text);

            if ( WebNotifier.Default.webpageList == null )
            {
                WebNotifier.Default.webpageList = new StringCollection();
            }
            WebNotifier.Default.webpageList.Add(textBox1.Text);


            if ( WebNotifier.Default.diffrentList == null )
            {
                WebNotifier.Default.diffrentList = new StringCollection();
            }
            string address = textBox1.Text;
            Dictionary<string, LinkedList<DiffItem>> dic = webi.DIC;
            string alpha = null;
            while ( alpha == null )
            {
                alpha = webi.CollectInfo(address);
            }
            string beta = null;
            while ( beta == null )
            {
                beta = webi.CollectInfo(address);
            }
            LinkedList<DiffItem> now = webi.Analyse(alpha, beta);

            dic[ address ] = now;

            WebNotifier.Default.Save();
            textBox1.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string copy = null;
            int i = 0;
            if ( WebNotifier.Default.webpageList == null )
            {
                WebNotifier.Default.webpageList = new StringCollection();
            }

            while ( listBox2.SelectedItems.Count != 0 )

            {
                copy = (string)listBox2.SelectedItems[ 0 ];
                listBox2.Items.Remove(listBox2.SelectedItems[ 0 ]);
                i++;

            }
            LinkedList<string> List = new LinkedList<string>(WebNotifier.Default.webpageList.Cast<string>());
            if ( copy != null )
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
            string business = "test@msn.com";  // your paypal email
            string description = "Donation";            // '%20' represents a space. remember HTML!
            string country = "DE";                  // AU, US, etc.
            string currency = "EUR";                 // AUD, USD, etc.
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
    }
}
