using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebNotifier
{
    public sealed class WebBrowserWrapper 
    {
        private string body = null;
        private string title = null;
        private string text = null;
        private static bool iscompleted = false;
        private static WebBrowserWrapper instance = null;
        private static readonly object padlock = new object();


        WebBrowserWrapper() { }
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
        public void Navigate(string url)
        {
            body = null;
            title = null;
            text = null;
            IsCompleted = false;
            Thread th = new Thread(() =>
            {
                WebBrowser br = new WebBrowser();
                br.DocumentCompleted += Browser_DocumentCompleted;
                br.ScriptErrorsSuppressed = true;
                br.Navigate(url);
                Application.Run();
            });
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

            // we must see if this is correct or better without
            th.Join();
        }

        void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            lock (this)
            {
                WebBrowser br = sender as WebBrowser;
                if (br.Url == e.Url)
                {
                    IsCompleted = true;
                    Text = string.Copy(br.DocumentText);
                    Body = string.Copy(br.Document.Body.InnerText);
                    Title = string.Copy(br.Document.Title);
                    if( Title == null)
                    {
                        Title = "default";
                    }
                    br.Dispose();
                    Application.ExitThread();   // Stops the thread
                }
            }
        }
        public bool IsCompleted
        {
            
            get => iscompleted;
            set => iscompleted = value;
        }
        public string Body { get => string.Copy(body); set => body = value; }
        public string Title { get => string.Copy(title); set => title = value; }
        public string Text { get => string.Copy(text); set => text = value; }

        public bool WaitforComplete(int sec = 2)
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            lock (this)
            {
                if (IsCompleted) { return true; }
            }
            //await PageLoad(sec);
            int calc = (int)Math.Ceiling((1.0/0.5) *(double)sec);
            for (int i = 0; i < calc; i++)
            {
                lock (this)
                {
                    if (IsCompleted)
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
                if (IsCompleted || Body != null)
                {
                    IsCompleted = true;
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

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
