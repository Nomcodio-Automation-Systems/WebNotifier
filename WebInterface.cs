﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebNotifier
{
    interface WebInterface
    {
        void ShowWindow(bool yes);
        void GoToNoWait(MenuListItem item);
        void WaitForComplete(string url,int sec = 10);

    }
}
