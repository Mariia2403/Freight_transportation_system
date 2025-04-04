using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freight_transportation_system
{
    internal class RouteSelector
    {
        //RouteSelector — повертає об'єкт Route.
        private Graph graph;

        public RouteSelector()
        {
            graph = new Graph();

            graph.AddEdge("Київ", "Житомир", 130);
            graph.AddEdge("Житомир", "Рівне", 180);
            graph.AddEdge("Рівне", "Львів", 210);
            graph.AddEdge("Київ", "Полтава", 340);
            graph.AddEdge("Полтава", "Харків", 140);
            graph.AddEdge("Київ", "Умань", 210);
            graph.AddEdge("Умань", "Одеса", 260);
            graph.AddEdge("Львів", "Тернопіль", 130);
            graph.AddEdge("Тернопіль", "Вінниця", 180);
            graph.AddEdge("Вінниця", "Одеса", 420);
            graph.AddEdge("Миколаїв", "Дніпро", 300);
            graph.AddEdge("Одеса", "Миколаїв", 130);
            graph.AddEdge("Дніпро", "Харків", 210);
            graph.AddEdge("Київ", "Черкаси", 200);
            graph.AddEdge("Черкаси", "Одеса", 450);
        }

        public Route GetOptimalRoute(string start, string end)
        {
            var (path, distance) = graph.Dijkstra(start, end);
            if (path.Count == 0) return new Route(start, end, new List<string>(), double.PositiveInfinity);

            return new Route(start, end, path.GetRange(1, path.Count - 2), distance);
        }

    }
}
