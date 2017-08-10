using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebNotifier
{
    class MenuListItem
    {
        public string Address { get; set; }
        //TODO maybe adding a range detector later
        public int Browser { get; set; }
        public bool Filters { get; set; }

        MenuListItem(string address)
        {
            Address = address;
            Filters = false;
            Browser = 1;
        }
        public override string ToString()
        {
            return Address;
        }
    }
}
