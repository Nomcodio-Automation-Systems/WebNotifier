using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebNotifier
{
    class ListItem
    {
        private string text;
        private string Urln;
        public ListItem(string text, string Url)
        {
            Urln = Url;
            this.text = text;
        }
        public string URL
        {
            get => Urln; set => Urln = value;
        }
        public override string ToString()
        {
            return text;
        }
    }
}
