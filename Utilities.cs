using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
// using System.Diagnostics; 
namespace WebNotifier
{
    class Utilities
    {
        public void TraceMessage(string message,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
        {
            MessageBox.Show(sourceLineNumber + " Webworker Error should not happened",
    "DebugMessage");
        }
    }
}
