using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freight_transportation_system
{
    internal class Gazell : Transport
    {
        public Gazell(string cargoType, double weight, double volume, string condition, Route route) : base(cargoType, weight, volume, condition, route)
        {

        }

        public override double MaxWeight => 1500;

        public override double MaxVolume => 12;
        public override double CalculateTransportationCost()
        {
            //Газель Дешевша в обслуговуванні, але короткі поїздки менш ефективні
            double baseRate = Route.Distance < 300 ? 0.08 : 0.05;
            return Cargo.Weight * Route.Distance * baseRate * GetConditionCostFactor();
        }
        public override string GetTransportType()
        {
            return "Gazell";
        }
    }
}
