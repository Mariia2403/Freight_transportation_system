using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freight_transportation_system
{
    public class Route
    {

       
        private string _startingPoint;
        private string _arrivalPoint;
        private List<string> _waypoints;
        private double _distance;

        
        public string StartingPoint
        {
            get => _startingPoint;
            set => _startingPoint = value;
        }

        public string ArrivalPoint
        {
            get => _arrivalPoint;
            set => _arrivalPoint = value;
        }

        public List<string> Waypoints
        {
            get => _waypoints;
            set => _waypoints = value;
        }

        public double Distance
        {
            get => _distance;
            set => _distance = value;
        }

        public Route()
        {
        }

        public Route(string start, string end, List<string> waypoints, double distance)
        {
            StartingPoint = start;
            ArrivalPoint = end;
            Waypoints = waypoints;
            Distance = distance;
        }
    }
}
