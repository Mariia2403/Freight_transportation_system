using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Freight_transportation_system
{
    //все має зберігатися у ViewModel, а View просто має до неї доступ.
    public class AddOrderViewModel : INotifyPropertyChanged // інтерфейс, який дозволяє автоматично оновлювати прив’язані елементи UI,
                                                            // коли змінюються властивості.
    {
        public ObservableCollection<TransportOption> TransportTypes { get; set; }//Список, який буде джерелом для ComboBox у XAML.
                                                                                 // Він містить об'єкти TransportOption — кожен з яких має,
                                                                                 // наприклад, Name = "Газель".

        public ObservableCollection<TransportOption> CargoType { get; set; }
        // public TransportOption SelectedTransportOption { get; set; } // ← те, що обирає користувач


        /////////////////////////////////////////////
        public Transport CreatedTransport { get; set; } // ← буде збережено після Save

        public Cargo Cargo { get; private set; }


        private string weightText; // Змінна для зберігання введеної ваги (в текстовому вигляді, бо юзер вводить рядок)
        private string volumeText;// Аналогічно для об'єму
        public string Condition { get; set; } // "Холод", "Без умов", "Сухий вантаж", і т.д.
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
       

        public AddOrderViewModel()//Заповнює список TransportTypes,
                                  //щоб ComboBox міг щось показати.
                                  //Конструктор ViewModel

        {
            TransportTypes = new ObservableCollection<TransportOption>
            {
            new TransportOption { Name = "Бус" },
            new TransportOption { Name = "Фура" },
            new TransportOption { Name = "Газель" }
            };
            CargoType = new ObservableCollection<TransportOption>
            {
                new TransportOption {CargoType = "Побутова техніка"},
                new TransportOption {CargoType = "Продукти харчування"},
                new TransportOption {CargoType = "Меблі"},
                new TransportOption {CargoType = "Інше"}

            };
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
    }
}
