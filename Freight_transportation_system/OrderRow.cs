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

        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public static readonly List<DeliveryStatus> AllStatuses =
            Enum.GetValues(typeof(DeliveryStatus)).Cast<DeliveryStatus>().ToList();

        public List<DeliveryStatus> DeliveryStatusList => AllStatuses;

        // === Приватні поля ===
        private DateTime _createdAt;
        private string _number;
        private string _transport;
        private string _cargoType;
        private string _conditionType;
        private string _departure;
        private string _arrival;
        private string _sum;
        private string _weight;
        private string _volume;
        private Route _routeObject;
        private string _userName;
        private string _lastName;
        private string _phoneNumber;
        private bool _isSelected;
        private DeliveryStatus _deliveryStatus;

        // === Властивості ===

        public DateTime CreatedAt
        {
            get => _createdAt;
            set { _createdAt = value; OnPropertyChanged(nameof(CreatedAt)); OnPropertyChanged(nameof(CreatedDateOnly)); OnPropertyChanged(nameof(CreatedDateTime)); }
        }

        public string CreatedDateOnly => CreatedAt.ToString("dd.MM.yyyy");

        public string CreatedDateTime => CreatedAt.ToString("dd.MM.yyyy HH:mm");

        public string Number
        {
            get => _number;
            set { _number = value; OnPropertyChanged(nameof(Number)); }
        }

        public string Transport
        {
            get => _transport;
            set { _transport = value; OnPropertyChanged(nameof(Transport)); }
        }

        public string CargoType
        {
            get => _cargoType;
            set { _cargoType = value; OnPropertyChanged(nameof(CargoType)); }
        }

        public string ConditionType
        {
            get => _conditionType;
            set { _conditionType = value; OnPropertyChanged(nameof(ConditionType)); }
        }

        public string Departure
        {
            get => _departure;
            set { _departure = value; OnPropertyChanged(nameof(Departure)); }
        }

        public string Arrival
        {
            get => _arrival;
            set { _arrival = value; OnPropertyChanged(nameof(Arrival)); }
        }

        public string Sum
        {
            get => _sum;
            set { _sum = value; OnPropertyChanged(nameof(Sum)); }
        }

        public string Weight
        {
            get => _weight;
            set { _weight = value; OnPropertyChanged(nameof(Weight)); }
        }

        public string Volume
        {
            get => _volume;
            set { _volume = value; OnPropertyChanged(nameof(Volume)); }
        }

        public Route RouteObject
        {
            get => _routeObject;
            set { _routeObject = value; OnPropertyChanged(nameof(RouteObject)); }
        }

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string FullName => $"{UserName} {LastName}";

        public string PhoneNumber
        {
            get => _phoneNumber;
            set { _phoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); }
        }

        public bool IsSelected
        {
            get => _isSelected;
            set { _isSelected = value; OnPropertyChanged(nameof(IsSelected)); }
        }

        public DeliveryStatus DeliveryStatus
        {
            get => _deliveryStatus;
            set
            {
                if (_deliveryStatus != value)
                {
                    _deliveryStatus = value;
                    OnPropertyChanged(nameof(DeliveryStatus));
                    MainViewModel.NotifyDataChanged(); 
                }
            }
        }

        // === DTO конвертери ===

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
