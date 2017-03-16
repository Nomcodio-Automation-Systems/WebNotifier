using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebNotifier
{
    sealed class WebWorkerSingelton
    {
        private static WebWorkerSingelton instance = null;
        private static readonly object padlock = new object();

        WebWorkerSingelton()
        {
        }

        public static WebWorkerSingelton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new WebWorkerSingelton();
                        }
                    }
                }
                return instance;
            }
        }
    }
}
