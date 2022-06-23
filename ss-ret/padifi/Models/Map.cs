using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace padifi.Models
{
    public class Map
    {
        /// <summary> TVertex </summary>
        public List<Adress> Adress { get; set; }

        /// <summary> TEdge </summary>
        public List<Route> Routes { get; set; }
    }
}
