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
        delegate void SetTextCallback2(MenuListItem item);
        private List<MenuListItem> menuList = new List<MenuListItem>();
        private MenuListItem _selected = null;
        public Form1()
        {
            InitializeComponent();
            notifyIcon1.Icon = new Icon(GetType(), "uicon.ico");
            menuList = ObjectBinarySerialize.LoadOListasFile<MenuListItem>("menu.bin");

            foreach (MenuListItem alpha_value in menuList)
            {
                listBox2.Items.Add(alpha_value);
            }
            webi = new WebWorker(this, menuList);
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
            //if (WebNotifier.Default.webpageList != null)
            //{
            //    foreach (string alpha_value in WebNotifier.Default.webpageList)
            //    {
            //        listBox2.Items.Add(new MenuListItem(alpha_value));
            //    }

            //}
            if (WebNotifier.Default.contentList == null)
            {
                WebNotifier.Default.contentList = new StringCollection();
            }

            //if (WebNotifier.Default.webpageList == null)
            //{
            //    WebNotifier.Default.webpageList = new StringCollection();
            //}

            if (WebNotifier.Default.diffrentList == null)
            {
                WebNotifier.Default.diffrentList = new StringCollection();
            }
            Text = "WebNotifier      " + new AssemblyInfo().VersionFull.ToString();
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
        ~Form1()
        {
            Dispose();
        }
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
                if (!webi.Running())
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
        public void SetTextList2(MenuListItem item)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (listBox2.InvokeRequired)
            {
                SetTextCallback2 d = new SetTextCallback2(SetTextList2);
                Invoke(d, new object[] { item });
            }
            else
            {
                listBox2.BeginUpdate();
                listBox2.Items.Add((item));
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
        {
            string box = textBox1.Text;
            if (box == null || box == "")
            {
                MessageBox.Show("Setting are no Internet address");
                return;
            }

            if (box != null && box != "")
            {
                if (!box.StartsWith("http") && !box.StartsWith("https"))
                {
                    box = "http://" + box;
                }

                bool result = Uri.TryCreate(box, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                if (!result)
                {
                    MessageBox.Show("Setting are no Internet address");
                    return;
                }
            }

            if (menuList == null)
            {
                menuList = ObjectBinarySerialize.LoadOListasFile<MenuListItem>("menu.bin");
            }
            //if (WebNotifier.Default.webpageList == null)
            //{
            //    WebNotifier.Default.webpageList = new StringCollection();
            //}

            MenuListItem item = new MenuListItem(box);

            if (WebNotifier.Default.diffrentList == null)
            {
                WebNotifier.Default.diffrentList = new StringCollection();
            }
            if (menuList == null)
            {
                menuList = ObjectBinarySerialize.LoadOListasFile<MenuListItem>("menu.bin");
            }
            //if (WebNotifier.Default.webpageList.Contains(box)){
            //    MessageBox.Show("Duplicate adresse");
            //    return;
            //}
            if (menuList != null && menuList.Contains(item))
            {
                MessageBox.Show("Duplicate adresse");
                return;
            }
            StringAddon stra = new StringAddon();

            // string address = box; 

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

              alpha = webi.CollectInfo(item, item.MaxTries);

              string beta = null;

              beta = webi.CollectInfo(item, item.MaxTries);

              List<DiffItem> now = webi.Analyse(alpha, beta);
              lock (webi)
              {
                  dic[item.Address] = now;
                  list.Add(beta);
                  webi.DIC = dic;
                  webi.PAGES = list;
                  SetTextList2(item);
                  menuList.Add(item);
                  ObjectBinarySerialize.SaveOListasFile("menu.bin", menuList);
                  //TODO we need a index 
                  // WebNotifier.Default.webpageList.Add(address);
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
            MenuListItem copy = null;
            int i = 0;
            //if (WebNotifier.Default.webpageList == null)
            //{
            //    WebNotifier.Default.webpageList = new StringCollection();
            //}

            while (listBox2.SelectedItems.Count != 0)

            {

                copy = (MenuListItem)listBox2.SelectedItems[0];
                listBox2.Items.Remove(listBox2.SelectedItems[0]);
                i++;

            }
            //LinkedList<string> List = new LinkedList<string>(WebNotifier.Default.webpageList.Cast<string>());
            if (copy != null)
            {
                menuList.Remove(copy);
            }
            //WebNotifier.Default.webpageList.Clear();
            //WebNotifier.Default.webpageList.AddRange(List.ToArray());
            //WebNotifier.Default.Save();

            webi.DIC[copy.Address] = null;
            ObjectBinarySerialize.SaveOListasFile("menu.bin", menuList);

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

        private void listBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            int index = listBox2.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                _selected = (MenuListItem)listBox2.Items[index];
                contextMenuStrip1.Show(Cursor.Position);
                updateMenuStrip(_selected);
                contextMenuStrip1.Visible = true;
            }
            else
            {
                contextMenuStrip1.Visible = false;
            }
        }

        private void webBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = sender as ToolStripMenuItem;


            string answer = menu.Tag.ToString();

            switch (answer)
            {
                case "WebBrowser":
                    _selected.Browser = 1;
                    break;
                case "WebSocket":
                    _selected.Browser = 2;
                    break;
                case "WaitTime3":
                    _selected.MaxWait = 3;
                    break;
                case "WaitTime5":
                    _selected.MaxWait = 5;
                    break;
                case "WaitTime10":
                    _selected.MaxWait = 10;
                    break;
                case "WaitTime15":
                    _selected.MaxWait = 15;
                    break;
                case "MaxTries3":
                    _selected.MaxTries = 3;
                    break;
                case "MaxTries5":
                    _selected.MaxTries = 5;
                    break;
                case "MaxTries10":
                    _selected.MaxTries = 10;
                    break;
                case "MaxTries30":
                    _selected.MaxTries = 20;
                    break;
                case "IgnoreChars0":
                    _selected.IgnoreChars = 0;
                    break;
                case "IgnoreChars50":
                    _selected.IgnoreChars = 50;
                    break;
                case "IgnoreChars100":
                    _selected.IgnoreChars = 100;
                    break;
                case "IgnoreChars150":
                    _selected.IgnoreChars = 150;
                    break;
                case "IgnoreChars200":
                    _selected.IgnoreChars = 200;
                    break;
                case "IgnoreChars250":
                    _selected.IgnoreChars = 250;
                    break;
                case "IgnoreChars500":
                    _selected.IgnoreChars = 500;
                    break;
                default:
                    break;

            }

            updateMenuStrip(_selected);

        }
        private void updateMenuStrip(MenuListItem item)
        {

            webBrowserToolStripMenuItem.Checked = false;
            websocketToolStripMenuItem.Checked = false;

            toolStripMenuWaitTime3.Checked = false;
            toolStripMenuWaitTime5.Checked = false;
            toolStripMenuWaitTime10.Checked = false;
            toolStripMenuWaitTime15.Checked = false;

            toolStripMenuMaxTries3.Checked = false;
            toolStripMenuMaxTries5.Checked = false;
            toolStripMenuMaxTries10.Checked = false;
            toolStripMenuMaxTries20.Checked = false;

            toolStripMenuIgnoreChars50.Checked = false;
            toolStripMenuIgnoreChars100.Checked = false;
            toolStripMenuIgnoreChars150.Checked = false;
            toolStripMenuIgnoreChars200.Checked = false;
            toolStripMenuIgnoreChars250.Checked = false;
            toolStripMenuIgnoreChars500.Checked = false;
            toolStripMenuIgnoreChars0.Checked = false;

            switch (item.Browser)
            {
                case 1:
                    webBrowserToolStripMenuItem.Checked = true;
                    break;
                case 2:
                    websocketToolStripMenuItem.Checked = true;
                    break;
                default:
                    break;
            }
            switch (item.MaxWait)
            {
                case 3:
                    toolStripMenuWaitTime3.Checked = true;
                    break;
                case 5:
                    toolStripMenuWaitTime5.Checked = true;
                    break;
                case 10:
                    toolStripMenuWaitTime10.Checked = true;
                    break;
                case 15:
                    toolStripMenuWaitTime15.Checked = true;
                    break;

            }
            switch (item.MaxTries)
            {
                case 3:
                    toolStripMenuMaxTries3.Checked = true;
                    break;
                case 5:
                    toolStripMenuMaxTries5.Checked = true;
                    break;
                case 10:
                    toolStripMenuMaxTries10.Checked = true;
                    break;
                case 20:
                    toolStripMenuMaxTries20.Checked = true;
                    break;

            }
            switch (item.IgnoreChars)
            {
                case 0:
                    toolStripMenuIgnoreChars0.Checked = true;
                    break;
                case 50:
                    toolStripMenuIgnoreChars50.Checked = true;
                    break;
                case 100:
                    toolStripMenuIgnoreChars100.Checked = true;
                    break;
                case 150:
                    toolStripMenuIgnoreChars150.Checked = true;
                    break;
                case 200:
                    toolStripMenuIgnoreChars200.Checked = true;
                    break;
                case 250:
                    toolStripMenuIgnoreChars250.Checked = true;
                    break;
                case 500:
                    toolStripMenuIgnoreChars500.Checked = true;
                    break;
            }
        }
    }
}
