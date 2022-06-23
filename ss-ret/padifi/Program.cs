using padifi.Logistics;
using padifi.Models;
using padifi.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace padifi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var fileCustomer = args.First(x => x.Contains("customers.json"));
            var fileMap = args.First(x => x.Contains("map.json"));

            //var customers = CustomersLoader.FromFile(@"C:\Develop\ss-ret\ss-ret\padifi\customers.json");
            var customers = CustomersLoader.FromFile(fileCustomer);
            var varaints = Merge(customers);

            //var map = MapLoader.FronFile(@"C:\Develop\ss-ret\ss-ret\padifi\map.json");
            var map = MapLoader.FronFile(fileMap);
            var pathFinder = new Pathfinder(map);


            double bestDistance = double.MaxValue;
            List<Adress> bestPath = new List<Adress>();

            Console.WriteLine("Debug paths:");
            foreach (var item in varaints)
            {
                var start = item.Adresses.First();
                var finish = item.Adresses.Last();
                var extra = item.Adresses.Where(x => x.Id != start.Id && x.Id != finish.Id).ToList();

                var path = pathFinder.BuildRoute(start, finish, extra, out double finalDistance);
                if(finalDistance < bestDistance)
                {
                    bestDistance = finalDistance;
                    bestPath = path;
                }

                Console.WriteLine(finalDistance);
                Console.WriteLine(string.Join(" -> ", path.Select(x => $"[{x.Id}] {x.Name}")));
            }
            Console.WriteLine("----------");

            Console.WriteLine($"Best distance: {bestDistance}");
            Console.WriteLine($"Best route: {bestDistance}");
            Console.WriteLine(string.Join(" -> ", bestPath.Select(x => $"[{x.Id}] {x.Name}")));

            Console.ReadLine();
        }

        private static List<RoadMap> Merge(List<RoadMap> customers)
        {
            var map = new RoadMap[customers.Count];

            for (int i = 0; i < map.Length; i++)
            {
                map[i] = new RoadMap();
                map[i].Name = $"Route {i}";
                map[i].Adresses.AddRange(customers[i].Adresses);

                foreach (var customer in customers.Where(x => x.Name != customers[i].Name))
                {
                    foreach (var item in customer.Adresses)
                    {
                        if (!map[i].Adresses.Any(x => x.Id == item.Id))
                        {
                            map[i].Adresses.Insert(1, item);
                        }
                    }
                }
            }


            return map.ToList();
        }
    }

    

    

    
}
