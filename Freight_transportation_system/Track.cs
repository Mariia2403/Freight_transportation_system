using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freight_transportation_system
{
    internal class Track : Transport
    {
        public override double MaxWeight => 5000;

        public override double MaxVolume => 40;

        public Track(string cargoType, double weight, double volume, string condition, Route route) : base(cargoType, weight, volume, condition, route)
        {

        }
        public override double CalculateTransportationCost()
        {
            //Дорога в обслуговуванні, але ефективна на далекі відстані
            double baseRate = Route.Distance < 300 ? 0.11 : 0.09;
            return Cargo.Weight * Route.Distance * baseRate * GetConditionCostFactor(); ;
        }

        public override string GetTransportType()
        {
            return "Track";
        }
    }
}
