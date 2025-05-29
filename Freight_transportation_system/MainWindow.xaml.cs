using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;


namespace Freight_transportation_system
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //це спеціальний тип списку, який автоматично повідомляє інтерфейс (UI),
        //коли в ньому з'являються нові елементи, видаляються або змінюються.
        public MainViewModel ViewModel { get; set; }
        private bool _isSavedManually = false;
        private bool _hasUnsavedChanges = false;
        //Це список усіх замовлень, які відображаються в таблиці DataGrid.
        //Коли додається новий елемент — DataGrid автоматично оновлюється.


        public MainWindow()
        {
            InitializeComponent();

           
            ViewModel = new MainViewModel();
            DataContext = ViewModel;
            MainViewModel.NotifyDataChanged = () => _hasUnsavedChanges = true;
            //LoadOrders();
            // зв'язуємо XAML з цим класом
            //Це найважливіший зв'язок! 



        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (!_hasUnsavedChanges)
            {
               
                base.OnClosing(e);
                return;
            }
            if (!_isSavedManually) // Якщо НЕ було збереження вручну
            {
                var result = MessageBox.Show(
                                        "Бажаєте зберегти зміни перед виходом?",
                                        "Підтвердження збереження",
                                        MessageBoxButton.YesNoCancel,
                                        MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    ViewModel.ApplyChanges(); // Зберігаємо ТА оновлюємо основний список
                }
                else if (result == MessageBoxResult.No)
                {
                    ViewModel.ResetOrders(); // Повертаємо як було
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }

                base.OnClosing(e);
            }
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

                var orderRow = OrderRow.FromDTO(dto);
                ViewModel.TempOrders.Add(orderRow);
                ViewModel.OrdersByNumber[orderRow.Number] = orderRow;
                ViewModel.UpdateTotalSum();
                _hasUnsavedChanges = true;
                // ViewModel.Orders.Add(OrderRow.FromDTO(dto));
                //routeHistory.Add(transport.Route);
            }
        }


        private string GenerateOrderNumber()
        {
            var random = new Random();
            string number;

            do
            {
                number = random.Next(10000000, 99999999).ToString(); // Генеруємо 8 цифр
            }
            while (ViewModel.OrdersByNumber.ContainsKey(number)); // Перевіряємо на існування

            return number;
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
            if (dataGridView1.SelectedItem is OrderRow selectedOrder)
            {
                var result = MessageBox.Show(
                    "Ви впевнені, що хочете видалити це замовлення?",
                    "Підтвердження",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    ViewModel.DeleteOrder(selectedOrder);
                }

                _hasUnsavedChanges = true;
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridView1.SelectedItem is OrderRow selectedOrder)
            {
                // 1. Створюємо DTO з вибраного елемента
                var dto = selectedOrder.ToDTO();

                // 2. Відкриваємо вікно AddOrderWindow з даними
                var editWindow = new AddOrderWindow(dto);

                // 3. Якщо користувач зберіг зміни
                if (editWindow.ShowDialog() == true)
                {
                    var vm = editWindow.ViewModel;

                    // 4. Створюємо оновлений DTO
                    OrderDTO updatedDto = new OrderDTO
                    {
                        CreatedAt = selectedOrder.CreatedAt,
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
                        DeliveryStatus = selectedOrder.DeliveryStatus
                    };

                    // 5. Викликаємо метод з ViewModel:
                    ViewModel.EditOrder(selectedOrder, updatedDto);
                    _hasUnsavedChanges = true;
                }
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string query = SearchBox.Text?.Trim();
            if (string.IsNullOrWhiteSpace(query))
            {
                MessageBox.Show("Введіть номер замовлення для пошуку.");
                return;
            }

            if (ViewModel.OrdersByNumber.TryGetValue(query, out var found))
            {
                dataGridView1.SelectedItem = found;
                dataGridView1.ScrollIntoView(found);
            }
            else
            {
                MessageBox.Show($"Замовлення з номером {query} не знайдено.");
            }
        }

        private void SaveOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ApplyChanges();
            _isSavedManually = true;
            _hasUnsavedChanges = false;
            MessageBox.Show("Замовлення успішно збережені!", "Збереження", MessageBoxButton.OK);
        }
    }
}
