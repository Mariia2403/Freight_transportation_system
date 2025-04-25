using System.Collections.Generic;
using System;
using System.Linq;
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

            if (orderForm.ShowDialog() == true)
            {
                var vm = orderForm.ViewModel;
                var transport = vm.CreatedTransport;

                var dto = new OrderDTO
                {
                    CreatedAt = DateTime.Now,
                    Number = GenerateOrderNumber(),
                    Transport = transport.GetTransportType(),
                    CargoType = vm.SelectedCargoType?.CargoType,
                    ConditionType = transport.SpecialCondition,
                    Departure = transport.Route.StartingPoint,
                    Arrival = transport.Route.ArrivalPoint,
                    Sum = transport.CalculateTransportationCost().ToString("C"),
                    Weight = vm.WeightText,
                    Volume = vm.VolumeText,
                    RouteObject = transport.Route,
                    UserName = vm.UserName,
                    LastName = vm.LastName,
                    PhoneNumber = vm.PhoneNumber,
                    DeliveryStatus = DeliveryStatus.Очікується
                };

                Orders.Add(OrderRow.FromDTO(dto));
                routeHistory.Add(transport.Route);
            }
        }

        private void Archived_Click_1(object sender, RoutedEventArgs e)
        {

        }
        private void SaveOrders()
        {
            var dtos = Orders.Select(o => o.ToDTO()).ToList();

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
            };

            var json = JsonSerializer.Serialize(dtos, options);
            File.WriteAllText("orders.json", json);
        }

        private void LoadOrders()
        {
            if (File.Exists("orders.json"))
            {
                var json = File.ReadAllText("orders.json");

                var options = new JsonSerializerOptions
                {
                    Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
                };

                var loadedDtos = JsonSerializer.Deserialize<List<OrderDTO>>(json, options);
                if (loadedDtos != null)
                {
                    Orders = new ObservableCollection<OrderRow>(loadedDtos.Select(OrderRow.FromDTO));
                    DataContext = this;
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
            EditButton.IsEnabled = dataGridView1.SelectedItem != null;
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

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridView1.SelectedItem is OrderRow selectedOrder)
            {
                // 1. Створюємо DTO з вибраного елемента
                var dto = selectedOrder.ToDTO();

                // 2. Відкриваємо вікно AddOrderWindow з даними
                var editWindow = new AddOrderWindow(dto); // ← Треба створити конструктор в AddOrderWindow який приймає DTO

                // 3. Якщо користувач зберіг зміни
                if (editWindow.ShowDialog() == true)
                {
                    var vm = editWindow.ViewModel;

                    // 4. Створюємо оновлений DTO
                    OrderDTO updatedDto = new OrderDTO
                    {
                        CreatedAt = selectedOrder.CreatedAt, // зберігаємо дату створення
                        Number = selectedOrder.Number,
                        Transport = vm.SelectedTransportOption?.Name,
                        CargoType = vm.SelectedCargoType?.CargoType,
                        ConditionType = vm.SelectedConditionType?.ConditionType,
                        Departure = vm.SelectedDepartureCity?.CitiesOfDeparture,
                        Arrival = vm.SelectedArrivalCity?.CitiesOfArrival,
                        Weight = vm.WeightText,
                        Volume = vm.VolumeText,
                        Sum = vm.CreatedTransport.CalculateTransportationCost().ToString("C"),
                        UserName = vm.UserName,
                        LastName = vm.LastName,
                        PhoneNumber = vm.PhoneNumber,
                        RouteObject = vm.CreatedTransport.Route,
                        DeliveryStatus = selectedOrder.DeliveryStatus // не змінюємо вручну тут
                    };

                    // 5. Оновлюємо у списку Orders
                    int index = Orders.IndexOf(selectedOrder);
                    if (index != -1)
                    {
                        Orders[index] = OrderRow.FromDTO(updatedDto);
                    }
                }
            }
        }
    }
}
