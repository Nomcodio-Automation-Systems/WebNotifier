using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebNotifier
{
    class UrlListItem
    {
        private string text;
        private string Urln;
        public UrlListItem(string text, string Url)
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
