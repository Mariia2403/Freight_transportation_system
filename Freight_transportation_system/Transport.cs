using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freight_transportation_system
{
    public abstract class Transport
    {
        public abstract double MaxWeight { get; }
        public abstract double MaxVolume { get; }

        public string SpecialCondition { get; set; } = "No necessary";

        public Cargo Cargo { get; set; }

        public Route Route { get; set; }


        public Transport(string cargoType, double weight, double volume, string condition, Route route)
        {
            Cargo = new Cargo(cargoType, weight, volume, condition, MaxWeight, MaxVolume);
            Route = route;
            SpecialCondition = condition;
        }

        public double GetConditionCostFactor()
        {
            double factor;
            switch (SpecialCondition)
            {
                case "Cooling":
                    factor = 1.5;
                    break;
                case "Amortization":
                    factor = 1.2;
                    break;
                case "Sealed":
                    factor = 1.3;
                    break;
                case "No necessary":
                    factor = 1.0;
                    break;
                default:
                    factor = 1.0;
                    break;
            }
            return factor;

        }

        public abstract double CalculateTransportationCost();//
        public abstract string GetTransportType();
    }
}
