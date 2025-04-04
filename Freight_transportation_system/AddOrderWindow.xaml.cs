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
    /// Interaction logic for AddOrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {
        public Transport SelectedTransport { get; private set; }
        public Cargo Cargo { get; private set; }

        public Route currentRoute;

        private RouteSelector routeSelector;
        public AddOrderWindow()
        {
            InitializeComponent();
            //this.DialogResult = true;
            //this.Close();
        }
    }
}
