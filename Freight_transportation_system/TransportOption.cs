using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freight_transportation_system
{
    /// <summary>
    // це довідник, список можливих варіантів для ComboBox.
    //Його завдання:показати назву типу транспорту(наприклад, “Бус”)
    //бути джерелом для вибору
    /// </summary>
    public class TransportOption 
    {
        public string Name { get; set; }
        public string CargoType { get; set; }

        public string ConditionType { get; set; }

        public string CitiesOfDeparture { get; set; }

        public string CitiesOfArrival { get; set; }
    }
}
