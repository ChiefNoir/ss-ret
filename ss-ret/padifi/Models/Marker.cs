using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace padifi.Models
{
    public class Marker
    {
        public int Index { get; set; }
        public Adress Start { get; set; }
        public Adress Finish { get; set; }
        public double Duration { get; set; }
    }
}
