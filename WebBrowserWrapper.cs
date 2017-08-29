using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebNotifier
{
    public class WebBrowserWrapper : IDisposable
    {
        private static Dictionary<string, string> body = new Dictionary<string, string>();
        private static Dictionary<string, string> title = new Dictionary<string, string>();
        private static Dictionary<string, string> text = new Dictionary<string, string>();
        private static Dictionary<string, string> xml = new Dictionary<string, string>();
        private static Dictionary<string, bool> iscompleted = new Dictionary<string, bool>();
        private List<WebBrowser> br = new List<WebBrowser>();
        private List<Thread> th = new List<Thread>();
        [ThreadStatic] static WebBrowserWrapper instance = null;
        private static readonly object padlock = new object();


        private WebBrowserWrapper() { IsReleased = false; }
        public static WebBrowserWrapper Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new WebBrowserWrapper();
                        }
                    }
                }
                return instance;
            }
        }
        public bool IsReleased { get; private set; }
        public void Release()
        {
            IsReleased = true;
            instance = null;
        }

        public void Navigate(string url)
        {
            // System.Diagnostics.Debug.Write("Calling Url :" + url);
            body[url] = null;
            title[url] = null;
            text[url] = null;
            iscompleted[url] = false;
            Thread thplus = new Thread(() =>
             {
                 WebBrowser brplus = new WebBrowser();
                 brplus.DocumentCompleted += Browser_DocumentCompleted;
                 brplus.ScriptErrorsSuppressed = true;
                 brplus.Navigate(url);
                 lock (this)
                 {
                     br.Add(brplus);
                 }
                 Application.Run();
             });
            thplus.SetApartmentState(ApartmentState.STA);
            thplus.Start();

            lock (this)
            {
                th.Add(thplus);
            }
            // we must see if this is correct or better without
            thplus.Join();
        }

        void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            lock (this)
            {
                WebBrowser br = sender as WebBrowser;
                if (br.Url == e.Url)
                {

                    //  System.Diagnostics.Debug.Write("Calling Url :" + br.Url);
                    iscompleted[br.Url.ToString()] = true;
                    text[br.Url.ToString()] = string.Copy(br.DocumentText);
                    body[br.Url.ToString()] = string.Copy(br.Document.Body.InnerText);
                    title[br.Url.ToString()] = string.Copy(br.Document.Title);
                    xml[br.Url.ToString()] = string.Copy(br.DocumentText);
                    if (title[br.Url.ToString()] == null)
                    {
                        title[br.Url.ToString()] = "default";
                    }
                    br.Dispose();
                    Application.ExitThread();   // Stops the thread
                }
            }
        }
        public bool IsCompleted(string url)
        {
            lock (this)
            {
                return iscompleted[url];
            }
        }
        public string Body(string url)
        {
            lock (this)
            {
                return string.Copy(body[url]);
            }
        }
        public string Title(string url)
        {
            lock (this)
            {
                return string.Copy(title[url]);
            }
        }
        public string Text(string url)
        {
            lock (this)
            {
                return string.Copy(text[url]);
            }
        }
        public string XML(string url)
        {
            lock (this)
            {
                return string.Copy(xml[url]);
            }
        }

        public bool WaitforComplete(string url, int sec = 2)
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            lock (this)
            {
                if (iscompleted[url]) { return true; }
            }
            //await PageLoad(sec);
            int calc = (int)Math.Ceiling((1.0 / 0.5) * (double)sec);
            for (int i = 0; i < calc; i++)
            {
                lock (this)
                {
                    if (iscompleted[url])
                    {


                        return true;
                    }
                }
                Thread.Sleep(500);
                //}
                //while (webBrowser.IsBusy || !IsCompleted)
                //{
                //    if (IsCompleted)
                //    {
                //        watch.Stop();
                //    }
                //}
                //watch.Stop();
            }
            lock (this)
            {
                if (iscompleted[url] || body[url] != null)
                {
                    iscompleted[url] = true;
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }



        #region IDisposable Support
        private bool disposedValue = false; // Dient zur Erkennung redundanter Aufrufe.

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    foreach (Thread t in th)
                    {
                        t.Abort();

                    }
                    foreach (WebBrowser b in br)
                    {
                        try
                        {
                            b.Dispose();
                        }
                        catch (Exception e)
                        {

                        }


                    }

                    th.Clear();
                    br.Clear();

                }

                // TODO: nicht verwaltete Ressourcen (nicht verwaltete Objekte) freigeben und Finalizer weiter unten überschreiben.
                // TODO: große Felder auf Null setzen.

                disposedValue = true;
            }
        }

        // TODO: Finalizer nur überschreiben, wenn Dispose(bool disposing) weiter oben Code für die Freigabe nicht verwalteter Ressourcen enthält.
        // ~WebBrowserWrapper() {
        //   // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(bool disposing) weiter oben ein.
        //   Dispose(false);
        // }

        // Dieser Code wird hinzugefügt, um das Dispose-Muster richtig zu implementieren.
        public void Dispose()
        {
            // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(bool disposing) weiter oben ein.
            Dispose(true);
            // TODO: Auskommentierung der folgenden Zeile aufheben, wenn der Finalizer weiter oben überschrieben wird.
            // GC.SuppressFinalize(this);
        }
        #endregion

        #region IDisposable Support
        //  private bool disposedValue = false; // Dient zur Erkennung redundanter Aufrufe.

        //public virtual void Dispose(bool disposing)
        //{
        //    if (!disposedValue)
        //    {
        //        if (disposing)
        //        {

        //        }

        //        // TODO: nicht verwaltete Ressourcen (nicht verwaltete Objekte) freigeben und Finalizer weiter unten überschreiben.
        //        // TODO: große Felder auf Null setzen.

        //        disposedValue = true;
        //    }
        //}

        // TODO: Finalizer nur überschreiben, wenn Dispose(bool disposing) weiter oben Code für die Freigabe nicht verwalteter Ressourcen enthält.
        // ~WebBrowserWrapper() {
        //   // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(bool disposing) weiter oben ein.
        //   Dispose(false);
        // }

        // Dieser Code wird hinzugefügt, um das Dispose-Muster richtig zu implementieren.
        //public void Dispose()
        // {
        //     // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(bool disposing) weiter oben ein.
        //     Dispose(true);
        //     // TODO: Auskommentierung der folgenden Zeile aufheben, wenn der Finalizer weiter oben überschrieben wird.
        //     // GC.SuppressFinalize(this);
        // }
        #endregion
    }
}
