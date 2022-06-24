using padifi.Models;
using QuikGraph;
using QuikGraph.Algorithms.ShortestPath;
using System.Collections.Generic;
using System.Linq;

namespace padifi.Logistics
{
    public class Pathfinder
    {
        private readonly AdjacencyGraph<Adress, Edge<Adress>> _graph;
        private readonly FloydWarshallAllShortestPathAlgorithm<Adress, Edge<Adress>> fw;

        public Pathfinder(Map map)
        {
            _graph = new AdjacencyGraph<Adress, Edge<Adress>>();

            foreach (var item in map.Adress)
            {
                _graph.AddVertex(item);
            }
            foreach (var item in map.Routes)
            {
                _graph.AddEdge
                (
                    new Edge<Adress>(
                        map.Adress.First(x => x.Id == item.StartId),
                        map.Adress.First(x => x.Id == item.FinishId)
                        )
                );
            }

            fw = new FloydWarshallAllShortestPathAlgorithm<Adress, Edge<Adress>>(_graph,
                (x) =>
                {
                    return
                    map.Routes.First(r => r.StartId == x.Source.Id && r.FinishId == x.Target.Id).Duration;
                }
                );

            fw.Compute();
        }

        public List<Adress> BuildRoute(Adress source, Adress target, List<Adress> extra, out double finalDistance)
        {
            var result = new List<Adress>
            {
                _graph.Vertices.First(x => x.Id == source.Id)
            };
            finalDistance = 0;

            extra = extra.Distinct().ToList();
            var pointsOfInterests = extra.Count;

            while (pointsOfInterests > 0)
            {
                IEnumerable<Edge<Adress>> shortPath = null;
                double shortDist = double.MaxValue;

                foreach (var item in extra)
                {
                    fw.TryGetPath(result.Last(), _graph.Vertices.First(x => x.Id == item.Id), out IEnumerable<Edge<Adress>> path);
                    if (path == null)
                    {
                        continue;
                    }
                    var dist = CalcDistance(path);

                    if (dist < shortDist)
                    {
                        shortDist = dist;
                        shortPath = path;
                    }
                }

                foreach (var item in shortPath)
                {
                    extra = extra.Where(x => x.Id != item.Source.Id && x.Id != item.Target.Id).ToList();
                    result.Add(item.Target);
                }
                pointsOfInterests = extra.Count;
                finalDistance += shortDist;
            }

            fw.TryGetPath(result.Last(), _graph.Vertices.First(x => x.Id == target.Id), out IEnumerable<Edge<Adress>> lastMile);
            foreach (var item in lastMile)
            {
                result.Add(item.Source);
                result.Add(item.Target);
            }

            return result;
        }

        private double CalcDistance(IEnumerable<Edge<Adress>> path)
        {
            double result = 0;

            foreach (var item in path)
            {
                fw.TryGetDistance(item.Source, item.Target, out double tmp);
                result += tmp;
            }
            return result;
        }
    }
}
