using Gecko;
using System;
using System.Threading;
using System.Windows.Forms;
using WatiN.Core;

namespace WebNotifier
{
    class WebBody
    {
        string text = null;

        public WebBody(string text)
        {
            this.text = text;
        }
        public string Text
        {
            get => (string)text.Clone();
            set => text = value;
        }

    }


    class WebBrowserFacade : IDisposable, WebInterface
    {
        protected static object browser = null;
        protected string type = "";
        protected string body = null;
        protected string xml = null;
        protected bool disposed = false;
        protected static WebBrowserWrapper dis = null;
        protected WebBrowserFacade() { }

        protected WebBrowserFacade(string ty, object o, WebBrowserWrapper gui)
        {
            type = ty;
            browser = o;
            dis = gui;
        }


        public void ShowWindow(bool yes)
        {
            if (type.ToLower() == "ie")
            {
                if (yes)
                {
                    ((IE)browser).ShowWindow(WatiN.Core.Native.Windows.NativeMethods.WindowShowStyle.Show);
                }
                else
                {
                    ((IE)browser).ShowWindow(WatiN.Core.Native.Windows.NativeMethods.WindowShowStyle.Hide);
                }
            }
            if (type.ToLower() == "websocket")
            {

            }
            if (type.ToLower() == "webbrowser")
            {
                //dis.Visible = yes;
            }
            if (type.ToLower() == "gecko")
            {
                if (yes)
                {
                    ((GeckoWebBrowser)browser).Show();
                }
                else
                {
                    ((GeckoWebBrowser)browser).Hide();
                }
            }
        }
        public void GoToNoWait(MenuListItem item)
        {
            if (type.ToLower() == "ie")
            {


                ((IE)browser).GoToNoWait(item.Address);


            }
            if (type.ToLower() == "webbrowser")
            {


                dis.Navigate(item.Address);

                dis.WaitforComplete(item.Address, item.MaxWait);
            }
            if (type.ToLower() == "websocket")
            {
                body = ((WebSocket)browser).Request(item.Address,item.MaxWait * 1000,item.MaxTries);
            }
            if (type.ToLower() == "gecko")
            {
                // Gecko.GeckoLoadFlags  ?? no api , no wiki
                ((GeckoWebBrowser)browser).Navigate(item.Address);

            }
        }

        public void WaitForComplete(string url, int sec = 10)
        {
            if (type.ToLower() == "ie")
            {


                ((IE)browser).WaitForComplete(sec);


            }
            if (type.ToLower() == "websocket")
            {

            }
            if (type.ToLower() == "webbrowser")
            {
                dis.WaitforComplete(url, sec);
            }


            if (type.ToLower() == "gecko")
            {


            }
        }

        public void Dispose()
        {
            Dispose(true);
            //GC.SuppressFinalize(this);
        }
        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                if (type.ToLower() == "ie")
                {


                    ((IE)browser).Dispose();


                }
                if (type.ToLower() == "websocket")
                {

                }
                if (type.ToLower() == "webbrowser")
                {
                    //dis.Dispose();

                }
                if (type.ToLower() == "gecko")
                {
                    // Gecko.GeckoLoadFlags  ?? no api , no wiki
                    ((GeckoWebBrowser)browser).Dispose();
                }
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }
        public WebBody Body(string url)
        {

            if (type.ToLower() == "ie")
            {


                Body b = ((IE)browser).Body;
                WebBody body = new WebBody(b.Text);
                return body;

            }
            if (type.ToLower() == "webbrowser")
            {

                if (dis.IsCompleted(url))
                {
                    string re = dis.Body(url);

                    WebBody body = new WebBody(string.Copy(re));
                    re = null;
                    return body;
                }
            }
            if (type.ToLower() == "websocket")
            {
                XMLWorker xl = new XMLWorker();
                string bb = xl.ExtractBody(body);
                WebBody re = new WebBody(string.Copy(bb));
                xl = null;
                bb = null;
                return re;
            }
            if (type.ToLower() == "gecko")
            {

                GeckoHtmlElement b = ((GeckoWebBrowser)browser).Document.Body;
                WebBody body = new WebBody(b.TextContent);
                return body;
            }
            else
            {
                return null;
            }

        }
        public string Title(string url)
        {

            if (type.ToLower() == "ie")
            {


                string title = ((IE)browser).Title;

                return title;

            }
            if (type.ToLower() == "webbrowser")
            {
                // I hope this is right
                string title = dis.Title(url);
                return title;

            }
            if (type.ToLower() == "websocket")
            {
                XMLWorker xl = new XMLWorker();
                string bb = xl.ExtractTitle(body);



                return bb;
            }
            if (type.ToLower() == "gecko")
            {

                string b = ((GeckoWebBrowser)browser).Document.Title;

                return b;
            }
            else
            {
                return null;
            }
        }
        public string XML(string url, string element)
        {
            if (type.ToLower() == "ie")
            {


                string xml = ((IE)browser).ElementsWithTag(element)[0].ToString();

                return xml;

            }
            if (type.ToLower() == "webbrowser")
            {
                // I hope this is right
                string title = dis.XML(url);
                return title;

            }
            if (type.ToLower() == "websocket")
            {
                XMLWorker xl = new XMLWorker();
                string bb = xl.ExtractTitle(body);



                return bb;
            }
            if (type.ToLower() == "gecko")
            {

                string b = ((GeckoWebBrowser)browser).Document.Title;

                return b;
            }
            else
            {
                return null;
            }
        }
    }
    class WebBrowserFactory : WebBrowserFacade
    {

        private static WebBrowserFactory instance;
        private WebBrowserFactory() { }
        private WebBrowserFactory(string ty, object o, WebBrowserWrapper gui) : base(ty, o, gui)
        {

        }
        public static WebBrowserFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WebBrowserFactory();
                }
                return instance;
            }
        }
        public WebBrowserFacade BrowserFactory(string factory, WebWorker mutex)
        {

            lock (mutex)
            {
                if (browser != null)
                {
                    return null;
                }
                WebBrowserWrapper gui = null;
                object tmp_browser = null;


                // type = factory; // we need this info later
                if (factory.ToLower() == "ie")
                {
                    tmp_browser = new IE();

                }
                if (factory.ToLower() == "webbrowser")
                {
                    gui = WebBrowserWrapper.Instance;
                    //  tmp_browser = gui.webBrowser;
                }
                if (factory.ToLower() == "gecko")
                {
                    Xpcom.Initialize("Firefox");
                    string Archi = "", OS = "";

                    if (System.Environment.Is64BitOperatingSystem)
                    {  //for 64 bit OS
                        Archi = " WOW64;";

                    }
                    OS = (System.Environment.OSVersion.Version.Major.ToString() + "." + System.Environment.OSVersion.Version.Minor.ToString());

                    //Set the new user-agent
                    Gecko.GeckoPreferences.User["general.useragent.override"] = "Mozilla/5.0 (Windows NT " + OS + ";" + Archi + " rv:45.0) Gecko/20100101 Firefox/45.0";
                    //Set your request language
                    Gecko.GeckoPreferences.User["intl.accept_languages"] = "de-de";

                    tmp_browser = new Gecko.GeckoWebBrowser { Dock = DockStyle.Fill };
                    // var geckoWebBrowser = new GeckoWebBrowser 

                    System.Windows.Forms.Form f = new System.Windows.Forms.Form();

                    f.Controls.Add((Gecko.GeckoWebBrowser)tmp_browser);

                    //  geckoWebBrowser.Navigate("www.google.com");

                    Application.Run(f);
                }
                if (factory.ToLower() == "websocket")
                {
                    tmp_browser = new WebSocket();
                }
                else
                {
                    tmp_browser = null;
                }

                return new WebBrowserFactory(factory, tmp_browser, gui);
            }
        }
    }

}
