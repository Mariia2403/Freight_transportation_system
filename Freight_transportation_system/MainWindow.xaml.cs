using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using System.IO;//набір класів для роботи з файлами, потоками і папками.

namespace Freight_transportation_system
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //це спеціальний тип списку, який автоматично повідомляє інтерфейс (UI),
        //коли в ньому з'являються нові елементи, видаляються або змінюються.
        public ObservableCollection<OrderRow> Orders { get; set; } = new ObservableCollection<OrderRow>();
        //Це список усіх замовлень, які відображаються в таблиці DataGrid.
        //Коли додається новий елемент — DataGrid автоматично оновлюється.

        private List<Route> routeHistory = new List<Route>();
       // Це окремий список, в якому ти зберігаєш внутрішню інформацію про маршрут
       // (яку не показуєш у таблиці, але вона важлива, наприклад для деталей замовлення).
        public MainWindow()
        {
            InitializeComponent();
            LoadOrders();
            DataContext = this; // зв'язуємо XAML з цим класом
                                //Це найважливіший зв'язок! 



        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            SaveOrders();
            base.OnClosing(e);
        }

        //Якщо користувач натискає ліву кнопку миші по Border,
        //вікно викликає this.DragMove(); — це стандартний WPF метод,
        //який дозволяє перетягувати вікно за будь-який елемент,
        //а не тільки за стандартну заголовну панель.
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private bool IsMaximized = false;
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

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            var orderForm = new AddOrderWindow();

            if (orderForm.ShowDialog() == true) // ShowDialog повертає bool у WPF
            {
                var vm = orderForm.ViewModel;

                Transport transport = vm.CreatedTransport;
                //Route route = vm.CurrentRoute;

                Orders.Add(new OrderRow
                {
                    Number = GenerateOrderNumber(),

                    Transport = transport.GetTransportType(),

                    CargoType = vm.SelectedCargoType?.CargoType,

                    Departure = transport.Route.StartingPoint,
                    Arrival = transport.Route.ArrivalPoint,

                    Sum = transport.CalculateTransportationCost().ToString("C"),

                    Weight = vm.WeightText.ToString(),
                    Volume = vm.VolumeText.ToString(),

                    RouteObject = transport.Route,

                    UserName = vm.UserName,
                    LastName = vm.LastName,
                    PhoneNumber = vm.PhoneNumber,

                }); //Додаємо новий рядок у таблицю на екрані (таблиця автоматично оновиться)

             //   routeHistory.Add(route);//Додаємо інформацію про маршрут (для подальших дій,
                                        //наприклад перегляду деталей)
            } 
        }

        private void Archived_Click_1(object sender, RoutedEventArgs e)
        {

        }
        private void SaveOrders()
        {
            var json = JsonSerializer.Serialize(Orders);
            File.WriteAllText("orders.json", json);//Клас File живе у просторі імен System.IO
        }

        private void LoadOrders()
        {
            if (File.Exists("orders.json"))
            {
                var json = File.ReadAllText("orders.json");
                var loadedOrders = JsonSerializer.Deserialize<ObservableCollection<OrderRow>>(json);
                if (loadedOrders != null)
                {
                    Orders = loadedOrders;
                    DataContext = this; // Оновлюємо прив'язку, якщо Orders перезаписаний
                }
            }
        }

        private string GenerateOrderNumber()
        {
            var random = new Random();
            int number =  random.Next(10000000, 99999999); // 8 цифр
            return number.ToString();
        }
        private void dataGridView1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Активуємо кнопку "Деталі", якщо щось вибрано
            DetailsButton.IsEnabled = dataGridView1.SelectedItem != null;
            DeleteButton.IsEnabled = dataGridView1.SelectedItem != null;
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridView1.SelectedItem is OrderRow selectedOrder)
            {
                var detailsWindow = new OrderDetailsWindow(selectedOrder);
                detailsWindow.ShowDialog();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridView1.SelectedItem is OrderRow selestedOrder)
            { 
            var result = MessageBox.Show("Ви впевнені, що хочете видалити це замовлення ? ",
                                     "Підтвердження",
                                     MessageBoxButton.YesNo,
                                     MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    Orders.Remove(selestedOrder);
                }
            }
        }
    }
}
