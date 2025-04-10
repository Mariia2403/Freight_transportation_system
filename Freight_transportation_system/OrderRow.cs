using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freight_transportation_system
{
    public class OrderRow
    {
      
        public string  Number { get; set; }
        public string Transport { get; set; }
        public string CargoType { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public string Sum { get; set; }


        public bool IsSelected { get; set; }
        public string Weight    { get; set; }
        public string Volume { get; set; }

        public Route RouteObject { get; set; }

        public string UserName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{UserName} {LastName}";
        public string PhoneNumber { get; set; }


    }
}
