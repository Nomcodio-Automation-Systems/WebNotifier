using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace WebNotifier
{
    public static class FormUtils
    {
        private static Icon _defaultFormIcon;
        public static Icon DefaultFormIcon
        {
            get
            {
                if (_defaultFormIcon == null)
                {
                    _defaultFormIcon = (Icon)typeof(Form).
                        GetProperty("DefaultIcon", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static).GetValue(null, null);
                }

                return _defaultFormIcon;
            }
        }

        //public static void SetDefaultIcon()
        //{
        //    var icon = Icon.ExtractAssociatedIcon(EntryAssemblyInfo.ExecutablePath);
        //    typeof(Form)
        //        .GetField("defaultIcon", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
        //        .SetValue(null, icon);
        //}
    }

    public static class FormExtensions
    {
        internal static void GetIconIfDefault(this Form dest, Form source)
        {
            if (dest.Icon == FormUtils.DefaultFormIcon)
            {
                dest.Icon = source.Icon;
            }
        }
    }
}
