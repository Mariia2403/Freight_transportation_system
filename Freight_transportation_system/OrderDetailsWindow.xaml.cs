using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Freight_transportation_system
{
    /// <summary>
    /// Interaction logic for OrderDetailsWindow.xaml
    /// </summary>
    public partial class OrderDetailsWindow : Window
    {
        private readonly Route _route;
        public OrderDetailsWindow(OrderRow order)
        {
            InitializeComponent();
            DataContext = order; // Прив'язка до об'єкта замовлення
            /* 
                        // Відображення деталей замовлення
                        TransportText.Text = order.Transport;
                        DepartureText.Text = order.Departure;
                        ArrivalText.Text = order.Arrival;
                        SumText.Text = order.Sum;
                        NumberText.Text = order.Number.ToString();*/

            _route = order.RouteObject;
        }


    }
}
