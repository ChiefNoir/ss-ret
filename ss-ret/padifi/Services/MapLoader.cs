using padifi.Models;
using System.IO;
using System.Text.Json;

namespace padifi.Services
{
    public static class MapLoader
    {
        public static Map FronFile(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Map>(json);
        }
    }
}
