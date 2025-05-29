using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freight_transportation_system
{
    public class Truck : Transport
    {
        public override double MaxWeight => 5000;

        public override double MaxVolume => 40;

        public Truck(string cargoType, double weight, double volume, string condition, Route route) : base(cargoType, weight, volume, condition, route)
        {

        }

       

        public override double CalculateTransportationCost()
        {
            //Дорога в обслуговуванні, але ефективна на далекі відстані !!!!!!!!!!!!!! змінити рядки
           double baseRate = Route.Distance < 300 ? 0.11 : 0.09;
           return Cargo.Weight * Route.Distance * baseRate * GetConditionCostFactor(); ;
           // return Cargo.Weight * GetConditionCostFactor(); ;
        }

        public override string GetTransportType()
        {
            return "Вантажівка";
        }
    }
}
