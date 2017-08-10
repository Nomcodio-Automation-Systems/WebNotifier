using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebNotifier
{
    interface WebInterface
    {
        void ShowWindow(bool yes);
        void GoToNoWait(string URL);
        void WaitForComplete(int sec = 10);

    }
}
