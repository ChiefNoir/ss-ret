using padifi.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace padifi.Services
{
    public static class CustomersLoader
    {
        public static List<RoadMap> FromFile(string path)
        {
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<RoadMap>>(json);
        }
    }
}
