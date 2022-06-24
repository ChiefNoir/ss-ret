using System.Collections.Generic;

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
