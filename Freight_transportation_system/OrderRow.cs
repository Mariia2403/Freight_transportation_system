using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freight_transportation_system
{
    public class OrderRow : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public DateTime CreatedAt { get; set; }
        public string Number { get; set; }
        public string Transport { get; set; }
        public string CargoType { get; set; }
        public string ConditionType { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public string Sum { get; set; }
        public string Weight { get; set; }
        public string Volume { get; set; }
        public Route RouteObject { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{UserName} {LastName}";
        public string PhoneNumber { get; set; }
        public bool IsSelected { get; set; }


        public string CreatedDateOnly => CreatedAt.ToString("dd.MM.yyyy");
        public string CreatedDateTime => CreatedAt.ToString("dd.MM.yyyy HH:mm");

        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
      //П?
        public List<DeliveryStatus> DeliveryStatusList { get; set; } =
     Enum.GetValues(typeof(DeliveryStatus)).Cast<DeliveryStatus>().ToList();

        private DeliveryStatus deliveryStatus;

        public DeliveryStatus DeliveryStatus
        {
            get => deliveryStatus;
            set
            {
                deliveryStatus = value;
                OnPropertyChanged(nameof(DeliveryStatus));
            }
        }
        // Метод для створення з DTO
        public static OrderRow FromDTO(OrderDTO dto)
        {
            if (dto == null) return null;
            return new OrderRow
            {
                CreatedAt = dto.CreatedAt,
                Number = dto.Number,
                Transport = dto.Transport,
                CargoType = dto.CargoType,
                ConditionType = dto.ConditionType,
                Departure = dto.Departure,
                Arrival = dto.Arrival,
                Sum = dto.Sum,
                Weight = dto.Weight,
                Volume = dto.Volume,
                RouteObject = dto.RouteObject,
                UserName = dto.UserName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                DeliveryStatus = dto.DeliveryStatus
            };
        }

        // Метод для конвертації назад у DTO
        public OrderDTO ToDTO()
        {
            return new OrderDTO
            {
                CreatedAt = CreatedAt,
                Number = Number,
                Transport = Transport,
                CargoType = CargoType,
                ConditionType = ConditionType,
                Departure = Departure,
                Arrival = Arrival,
                Sum = Sum,
                Weight = Weight,
                Volume = Volume,
                RouteObject = RouteObject,
                UserName = UserName,
                LastName = LastName,
                PhoneNumber = PhoneNumber,
                DeliveryStatus = DeliveryStatus
            };
        }
    }

}
