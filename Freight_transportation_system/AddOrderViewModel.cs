using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Freight_transportation_system
{
    //все має зберігатися у ViewModel, а View просто має до неї доступ.
    public class AddOrderViewModel : INotifyPropertyChanged // інтерфейс, який дозволяє автоматично оновлювати прив’язані елементи UI,
                                                            // коли змінюються властивості.
    {
        // Для заповнення комбобоксу 
        public ObservableCollection<TransportOption> TransportTypes { get; set; }//Список, який буде джерелом для ComboBox у XAML.
                                                                                 // Він містить об'єкти TransportOption — кожен з яких має,
                                                                                 // наприклад, Name = "Газель".

        public ObservableCollection<TransportOption> CargoType { get; set; }
        public ObservableCollection<TransportOption> ConditionType { get; set; }
        public ObservableCollection<TransportOption> CitiesOfDeparture { get; set; }
        public ObservableCollection<TransportOption> CitiesOfArrival { get; set; }

        /////////////////////////////////////////////
        
        public Transport CreatedTransport { get; set; } // ← буде збережено після Save
     //   public Cargo Cargo { get; private set; }
        private string weightText; // Змінна для зберігання введеної ваги (в текстовому вигляді, бо юзер вводить рядок)
        private string volumeText;// Аналогічно для об'єму
        public Route CurrentRoute { get; set; } // маєш його створювати перед цим

        ////////////////////////////////////
        //WPF використовує Binding, щоб передати значення з ComboBox → ViewModel.
        //Але якщо немає логіки set + OnPropertyChanged — WPF не може “поставити” значення.
        //І твоя властивість залишається null.

        private TransportOption selectedTransportOption;
        public TransportOption SelectedTransportOption
        {
            get => selectedTransportOption;
            set
            {
                selectedTransportOption = value;
                OnPropertyChanged(nameof(SelectedTransportOption));
            }
        }


        //Для вантажу присвоєння , тут зберігається вантаж його тип ,
        //потім буде потрібно присвоїти щоб це відображалось в детальній інформації

        private TransportOption selectedCargoType;
        public TransportOption SelectedCargoType
        {
            get => selectedCargoType;
            set
            {
                selectedCargoType = value;
                OnPropertyChanged(nameof(SelectedCargoType));
            }
        }


        ////////////////////////////////////////////////

        private TransportOption selectedConditionType;

        public TransportOption SelectedConditionType
        {
            get => selectedConditionType;
            set
            {
                selectedConditionType = value;
                OnPropertyChanged(nameof(selectedConditionType));
            }
        }

        ///////////////////////////////////////////////

        private TransportOption selectedDepartureCity;

        public TransportOption SelectedDepartureCity
        {

            get => selectedDepartureCity;
            set
            {
                selectedDepartureCity = value;
                OnPropertyChanged(nameof(selectedConditionType));

            }
        }

        ////////////////////////////////////////////////
        private TransportOption selectedArrivalCity;

        public TransportOption SelectedArrivalCity
        {
            get => selectedArrivalCity;
            set
            {
                selectedArrivalCity = value;
                OnPropertyChanged(nameof(selectedArrivalCity));
            }
        }

        ////////////////////////////////////////////////
        public string WeightText
        {
            get => weightText;
            set
            {
                weightText = value;
                OnPropertyChanged(nameof(WeightText));// Сповіщаємо, що змінилося значення
                ValidateWeight();// Перевірка валідності вводу одразу при зміні
            }
        }

        // Повідомлення про помилки вводу ваги (відображається під полем)
        private string weightError;
        public string WeightError
        {
            get => weightError;
            set
            {
                weightError = value;
                OnPropertyChanged(nameof(WeightError));// Щоб TextBlock під полем оновився
            }
        }


        /////////////////////////////////////////////////////////


        public string VolumeText
        {
            get => volumeText;
            set
            {
                volumeText = value;
                OnPropertyChanged(nameof(VolumeText));
                ValidateVolume(); // Валідатор
            }
        }
        // Аналогічно — повідомлення про помилки об'єму
        private string volumeError;
        public string VolumeError
        {
            get => volumeError;
            set
            {
                volumeError = value;
                OnPropertyChanged(nameof(VolumeError));
            }
        }


        /////////////////////////////////////////////////
        private string userName;
        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged(nameof(UserName));
                ValidateUserName();
            }
        }


        /////////////////////////////////////////////////

        private string lastName;
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
                ValidateLastName();
            }
        }

        /////////////////////////////////////////////////
        private string phoneNumber;
        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
                ValidatePhoneNumber();
            }
        }


        /////////////////////////////////////////////////

        // Повідомлення про помилки
        public string UserNameError { get; set; }
        public string LastNameError { get; set; }
        public string PhoneNumberError { get; set; }
        /////////////////////////////////////////////////


        public AddOrderViewModel()//Заповнює список TransportTypes,
                                  //щоб ComboBox міг щось показати.
                                  //Конструктор ViewModel

        {
            TransportTypes = new ObservableCollection<TransportOption>
            {
            new TransportOption { Name = "Бус" },
            new TransportOption { Name = "Вантажівка" },
            new TransportOption { Name = "Газель" }
            };
            CargoType = new ObservableCollection<TransportOption>
            {
                new TransportOption {CargoType = "Побутова техніка"},
                new TransportOption {CargoType = "Продукти харчування"},
                new TransportOption {CargoType = "Меблі"},
                new TransportOption {CargoType = "Інше"}

            };
            ConditionType = new ObservableCollection<TransportOption>
            {
               new TransportOption { ConditionType = "Охолодження" },
               new TransportOption { ConditionType = "Амортизація" },
               new TransportOption { ConditionType = "Герметичний" },
               new TransportOption { ConditionType = "Не потрібно" },
            };
            CitiesOfDeparture = new ObservableCollection<TransportOption>
            {
              new TransportOption {CitiesOfDeparture = "Київ"},
              new TransportOption {CitiesOfDeparture = "Львів"},
              new TransportOption {CitiesOfDeparture = "Одеса"},
              new TransportOption {CitiesOfDeparture = "Житомир"},
              new TransportOption {CitiesOfDeparture = "Рівне"},
              new TransportOption {CitiesOfDeparture = "Харків"},
              new TransportOption {CitiesOfDeparture = "Умань"},
              new TransportOption {CitiesOfDeparture = "Полтава"},
              new TransportOption {CitiesOfDeparture = "Тернопіль"},
              new TransportOption {CitiesOfDeparture = "Черкаси"},
              new TransportOption {CitiesOfDeparture = "Вінниця"},
              new TransportOption {CitiesOfDeparture = "Миколаїв"},
              new TransportOption {CitiesOfDeparture = "Дніпро"},

            };
            CitiesOfArrival = new ObservableCollection<TransportOption>
            {
              new TransportOption {CitiesOfArrival = "Київ"},
              new TransportOption {CitiesOfArrival = "Львів"},
              new TransportOption {CitiesOfArrival = "Одеса"},
              new TransportOption {CitiesOfArrival = "Житомир"},
              new TransportOption {CitiesOfArrival = "Рівне"},
              new TransportOption {CitiesOfArrival = "Харків"},
              new TransportOption {CitiesOfArrival = "Умань"},
              new TransportOption {CitiesOfArrival   = "Полтава"},
              new TransportOption {CitiesOfArrival = "Тернопіль"},
              new TransportOption {CitiesOfArrival = "Черкаси"},
              new TransportOption {CitiesOfArrival = "Вінниця"},
              new TransportOption {CitiesOfArrival = "Миколаїв"},
              new TransportOption {CitiesOfArrival = "Дніпро"},
            };
        }

        public AddOrderViewModel(OrderDTO dto) : this() // ← викликає базовий конструктор, щоб наповнити списки
        {
            SelectedTransportOption = TransportTypes.FirstOrDefault(t => t.Name == dto.Transport);
            SelectedCargoType = CargoType.FirstOrDefault(c => c.CargoType == dto.CargoType);
            SelectedConditionType = ConditionType.FirstOrDefault(c => c.ConditionType == dto.ConditionType);
            SelectedDepartureCity = CitiesOfDeparture.FirstOrDefault(c => c.CitiesOfDeparture == dto.Departure);
            SelectedArrivalCity = CitiesOfArrival.FirstOrDefault(c => c.CitiesOfArrival == dto.Arrival);

            WeightText = dto.Weight;
            VolumeText = dto.Volume;

            UserName = dto.UserName;
            LastName = dto.LastName;
            PhoneNumber = dto.PhoneNumber;

            CurrentRoute = dto.RouteObject;

           
            if (dto.Transport == "Газель")
                CreatedTransport = new Gazell(dto.Transport, double.Parse(dto.Weight), double.Parse(dto.Volume), dto.ConditionType, dto.RouteObject);
            else if (dto.Transport == "Вантажівка")
                CreatedTransport = new Truck(dto.Transport, double.Parse(dto.Weight), double.Parse(dto.Volume), dto.ConditionType, dto.RouteObject);
            else if (dto.Transport == "Бус")
                CreatedTransport = new Beads(dto.Transport, double.Parse(dto.Weight), double.Parse(dto.Volume), dto.ConditionType, dto.RouteObject);
        }

        public event PropertyChangedEventHandler PropertyChanged;//Це сигналізація для WPF, коли якась властивість змінюється.

        // Метод викликається, коли змінюється якась властивість
        protected void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));


        }

        // Метод перевірки ваги (при зміні тексту)
        private void ValidateWeight()
        {
            if (string.IsNullOrWhiteSpace(WeightText))
            {
                WeightError = "Поле не може бути порожнім";
                return;
            }

            if (!double.TryParse(WeightText, out double result) || result <= 0)
            {
                WeightError = "Введіть додатне число";
            }
            else
            {
                WeightError = string.Empty;
            }
        }

        // Аналогічно — метод перевірки об'єму
        private void ValidateVolume()
        {
            if (string.IsNullOrWhiteSpace(VolumeText))
            {
                VolumeError = "Поле не може бути порожнім";
                return;
            }

            if (!double.TryParse(VolumeText, out double result) || result <= 0)
            {
                VolumeError = "Введіть додатне число";
            }
            else
            {
                VolumeError = string.Empty;
            }
        }

        //Методи перевірки для інформації "ПРО СЕБЕ"
        public void ValidateUserName()
        {
            if (string.IsNullOrWhiteSpace(UserName))
            {
                UserNameError = "Ім’я не може бути порожнім";
            }

            else if (UserName.Trim().Contains(" "))
            {
                UserNameError = "Ім’я повинно складатися з одного слова";
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(UserName, @"^[А-ЯІЇЄҐа-яіїєґ'-]{2,20}$"))
            {
                UserNameError = "Ім’я має містити лише літери українського алфавіту ";
            }
            else
            {
                UserNameError = string.Empty;
            }

            OnPropertyChanged(nameof(UserNameError));
        }

        public void ValidateLastName()
        {
            if (string.IsNullOrWhiteSpace(LastName))
            {
                LastNameError = "Прізвище не може бути порожнім";
            }
            else if (LastName.Trim().Contains(" "))
            {
                LastNameError = "Прізвище повинно бути одним словом";
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(LastName, @"^[А-ЯІЇЄҐа-яіїєґ'-]{2,30}$"))
            {
                LastNameError = "Прізвище має містити лише українські літери";
            }
            else
            {
                LastNameError = string.Empty;
            }

            OnPropertyChanged(nameof(LastNameError));
        }

        public void ValidatePhoneNumber()
        {
            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                PhoneNumberError = "Номер телефону обов’язковий";
            }
            else
            {
                var cleanedNumber = PhoneNumber.Replace(" ", ""); // видаляємо пробіли
                var pattern = @"^(?:\+38)?0\d{9}$"; // допускає +380XXXXXXXXX або 0XXXXXXXXX

                if (!System.Text.RegularExpressions.Regex.IsMatch(cleanedNumber, pattern))
                    PhoneNumberError = "Некоректний формат телефону. Приклад: +380961234567 або 096 1234567";
                else
                    PhoneNumberError = string.Empty;
            }

            OnPropertyChanged(nameof(PhoneNumberError));
        }


    }
}
