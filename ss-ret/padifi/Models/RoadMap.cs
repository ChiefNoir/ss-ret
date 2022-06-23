using System.Collections.Generic;

namespace padifi.Models
{
    public class RoadMap
    {
        public string Name { get; set; }
        public List<Adress> Adresses { get; set; } = new();
    }
}
