using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freight_transportation_system
{
    public class OrderRow
    {
        public bool IsSelected { get; set; }
        public string Number { get; set; }
        public string Transport { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public string Sum { get; set; }
    }
}
