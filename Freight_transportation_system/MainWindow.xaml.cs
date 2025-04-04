using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<OrderRow> Orders { get; set; } = new ObservableCollection<OrderRow>();
        //Це список усіх замовлень, які відображаються в таблиці DataGrid.
        //Коли додається новий елемент — DataGrid автоматично оновлюється.

        private List<Route> routeHistory = new List<Route>();
       // Це окремий список, в якому ти зберігаєш внутрішню інформацію про маршрут
       // (яку не показуєш у таблиці, але вона важлива, наприклад для деталей замовлення).
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this; // зв'язуємо XAML з цим класом
                                //Це найважливіший зв'язок! 

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
                Transport transport = orderForm.SelectedTransport;
                Route route = orderForm.currentRoute;

                Orders.Add(new OrderRow 
                {
                    Transport = transport.GetTransportType(),
                    Departure = route.StartingPoint,
                    Arrival = route.ArrivalPoint,
                    Sum = transport.CalculateTransportationCost().ToString("C")
                }); //Додаємо новий рядок у таблицю на екрані (таблиця автоматично оновиться)

                routeHistory.Add(route);//Додаємо інформацію про маршрут (для подальших дій,
                                        //наприклад перегляду деталей)
            } 
        }

        private void Archived_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
