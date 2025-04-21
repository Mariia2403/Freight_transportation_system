using System;
using System.Windows;
using System.Windows.Input;

namespace Freight_transportation_system
{
    /// <summary>
    /// Interaction logic for AddOrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {
        public AddOrderViewModel ViewModel { get; }
        //Це публічна властивість ViewModel,
        //яка дозволяє іншим класам (наприклад MainWindow)
        //отримати доступ до даних, які зберігаються у AddOrderViewModel.

        private bool IsMaximized = false;


        //РОЗКОМЕНТУЙ БО ТИ ЩЕ ЮЗАТИМИШ!!!
        // private RouteSelector routeSelector;
        public AddOrderWindow()
        {
            //ПОЯСНИТИ!
            InitializeComponent();
            ViewModel = new AddOrderViewModel();
            DataContext = ViewModel;
            //this.DialogResult = true;
            //this.Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        //Виконується при подвійному кліку лівою кнопкою.

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (!IsMaximized)
                {
                    this.WindowState = WindowState.Maximized;
                    IsMaximized = true;
                }
                else
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;
                    IsMaximized = false;
                }
            }
        }

        private void Save_Click_1(object sender, RoutedEventArgs e)
        {
            AddOrderViewModel vm = this.ViewModel;

            vm.ValidateUserName();
            vm.ValidateLastName();
            vm.ValidatePhoneNumber();

            if (!string.IsNullOrEmpty(vm.UserNameError) ||
                !string.IsNullOrEmpty(vm.LastNameError) ||
                !string.IsNullOrEmpty(vm.PhoneNumberError))
            {
                MessageBox.Show("Будь ласка, виправте помилки у формі.");
                return;
            }

            string transportType = vm.SelectedTransportOption?.Name;// Присвоює те що обрав користувач 
            string cargoType = vm.SelectedCargoType?.CargoType;//Присвоюємо те що обрав користувач 
            string conditionType = vm.SelectedConditionType?.ConditionType;



            if (string.IsNullOrWhiteSpace(transportType))
            {
                MessageBox.Show("Оберіть тип транспорту!");
                return;
            }
            if (string.IsNullOrWhiteSpace(cargoType))
            {
                MessageBox.Show("Оберіть тип вантажу!");
                return;
            }
            if (!double.TryParse(vm.WeightText, out double weight) || weight <= 0)
            {
                MessageBox.Show("Вкажіть коректну вагу (додатне число)!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!double.TryParse(vm.VolumeText, out double volume) || volume <= 0)
            {
                MessageBox.Show("Вкажіть коректний обʼєм (додатне число)!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //Пункт прибуття та відправлення

            string start = vm.SelectedDepartureCity?.CitiesOfDeparture;
            string end = vm.SelectedArrivalCity?.CitiesOfArrival;

            if (string.IsNullOrWhiteSpace(start) || string.IsNullOrWhiteSpace(end))
            {
                MessageBox.Show("Оберіть пункти відправлення та прибуття!");
                return;
            }

            if (start == end)
            {
                MessageBox.Show("Пункт відправлення і прибуття не можуть бути однаковими.");
                return;
            }

            //Тут відбувається знаходження оптимального шляху
            RouteSelector selector = new RouteSelector();
            Route rout = selector.GetOptimalRoute(start, end);

            //Збереження у viewModel
           // vm.CurrentRoute = rout;

            


            try
            {

                Transport createdTransport;
                switch (transportType)
                {
                    case "Газель":
                        createdTransport = new Gazell(transportType, weight, volume, conditionType, rout);
                        break;
                    case "Фура":
                        createdTransport = new Track(transportType, weight, volume, conditionType, rout);
                        break;
                    case "Бус":
                        createdTransport = new Beads(transportType, weight, volume, conditionType, rout);
                        break;
                    default:
                        MessageBox.Show("Невідомий тип транспорту!");
                        return;
                }

                vm.CreatedTransport = createdTransport; // ← створений об'єкт зберігається у ViewModel
                this.DialogResult = true;
                this.Close();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Невірні дані", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
