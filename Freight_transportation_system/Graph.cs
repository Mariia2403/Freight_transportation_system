using System.Collections.Generic;

namespace Freight_transportation_system
{
    public class Graph
    {
        //Graph.Dijkstra() — знаходить найкоротший шлях.
        private Dictionary<string, List<(string, int)>> adjacencyList;

        public Graph()
        {
            adjacencyList = new Dictionary<string, List<(string, int)>>();
        }

        // Додавання ребра між містами з вказаною відстанню
        public void AddEdge(string from, string to, int distance)
        {
            if (!adjacencyList.ContainsKey(from))
                adjacencyList[from] = new List<(string, int)>();

            if (!adjacencyList.ContainsKey(to))
                adjacencyList[to] = new List<(string, int)>();

            adjacencyList[from].Add((to, distance));
            adjacencyList[to].Add((from, distance)); // Граф неорієнтований
        }

        // Реалізація алгоритму Дейкстри
        public (List<string> shortestPath, int distance) Dijkstra(string start, string end)
        {
            var distances = new Dictionary<string, int>(); // Найкоротша відстань до кожного міста
            var previous = new Dictionary<string, string>(); // Відстеження попередніх міст у шляху
            var priorityQueue = new SortedSet<(int, string)>(); // Пріоритетна черга для вибору найкоротшого шляху
            //Упорядковано за відстанню(int) автоматично!

            // Ініціалізація графа
            foreach (var node in adjacencyList.Keys)
            {
                distances[node] = int.MaxValue;
                previous[node] = null;
            }

            distances[start] = 0;
            priorityQueue.Add((0, start));

            while (priorityQueue.Count > 0)
            {
                //це тпу розпаування кортежу
                // var (currentDistance, currentNode) = priorityQueue.Min;
                //Його модна записати так
                var minItem = priorityQueue.Min;
                int currentDistance = minItem.Item1;
                string currentNode = minItem.Item2;

                //Обов’язково треба видалити цей вузол з черги, бо ми вже його обробили. Інакше буде повторно використовуватись.
                priorityQueue.Remove(priorityQueue.Min);

                if (currentNode == end)
                    break;

                foreach (var (neighbor, weight) in adjacencyList[currentNode])
                {
                    int newDistance = currentDistance + weight;
                    if (newDistance < distances[neighbor])
                    {
                        priorityQueue.Remove((distances[neighbor], neighbor));
                        distances[neighbor] = newDistance;
                        previous[neighbor] = currentNode;
                        priorityQueue.Add((newDistance, neighbor));
                    }
                }
            }

            // Відновлення найкоротшого шляху
            List<string> shortestPath = new List<string>();
            string step = end;

            while (step != null)
            {
                shortestPath.Insert(0, step);
                step = previous[step];
            }

            //Якщо відстань усе ще дорівнює int.MaxValue, це означає шляху немає → повертаємо -1
            //shortestPath — маршрут як список міст
            return (shortestPath, distances[end] == int.MaxValue ? -1 : distances[end]);
        }
    }
}
