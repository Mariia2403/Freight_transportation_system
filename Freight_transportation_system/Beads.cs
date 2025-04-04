using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freight_transportation_system
{
    internal class Beads : Transport
    {
        public override double MaxWeight => 3000;
        public override double MaxVolume => 20;
        public Beads(string cargoType, double weight, double volume, string condition, Route route) : base(cargoType, weight, volume, condition, route)
        {
            // це конструктор підкласу(Beads), який передає параметри в конструктор базового класу(наприклад Transport або BaseTransport) через ключове слово base.
        }

        public override double CalculateTransportationCost()
        {
            //  Бус Середня собівартість, тариф теж помірний
            double baseRate = Route.Distance < 300 ? 0.09 : 0.07;
            return Cargo.Weight * Route.Distance * baseRate * GetConditionCostFactor();
        }

        public override string GetTransportType()
        {
            return "Beads";
        }
    }
}
