using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebNotifier
{
    [Serializable]
    class DiffItem
    {
        private int start = 0;
        private int end = 0;
        private bool nodiff = false;
        public DiffItem(int start, int end)
        {
            this.end = end;
            this.start = start;
        }
        public int Start
        {
            get => start;
            set => start = value;
        }
        public int End
        {
            get => end;
            set => end = value;
        }
        public bool NoDiff
        {
            get => nodiff;
            set => nodiff = value;
        }
    }
}
