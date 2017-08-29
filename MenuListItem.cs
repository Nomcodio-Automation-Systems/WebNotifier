using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace WebNotifier
{
    [Serializable]
    public class MenuListItem : IEquatable<MenuListItem>
    {
        public string Address { get; set; }
        //TODO maybe adding a range detector later
        public int Browser { get; set; }
        public bool Filters { get; set; }
        public int IgnoreChars { get; set; }
        // private List<DiffItem> DiffList { get; set; }
        public int Position { get; set; }
        public int MaxTries { get; set; }
        public int MaxWait { get; set; }

        public MenuListItem(string address)
        {
            Address = address;
            Filters = false;
            Browser = 1;
            IgnoreChars = 250;
            MaxTries = 5;
            MaxWait = 3;
        }
        public override string ToString()
        {
            return Address;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            MenuListItem objAsPart = obj as MenuListItem;
            if (objAsPart == null)
            {
                return false;
            }
            else
            {
                return Equals(objAsPart);
            }
        }
        public override int GetHashCode()
        {
            return Address.GetHashCode();
        }
        public bool Equals(MenuListItem other)
        {
            if (other == null) { return false; }

            if (Address == other.Address)
            {
                return true;
            }
            else
            {
                return false;

            }
        }
        public string WhichBrowser()
        {
            switch (Browser)
            {
                case 1:
                    return "webbrowser";

                case 2:
                    return "websocket";
                default:
                    return "webbrowser";

            }
        }
    }
}
