using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Collections.ObjectModel;

namespace Freight_transportation_system
{
    /// <summary>
    /// Interaction logic for AddOrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {
        public AddOrderViewModel ViewModel { get; }

        private bool IsMaximized = false;//Що це 
      

        //РОЗКОМЕНТУЙ БО ТИ ЩЕ ЮЗАТИМИШ!!!
       // private RouteSelector routeSelector;
        public AddOrderWindow()
        {
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
            var vm = this.ViewModel;

            string transportType = vm.SelectedTransportOption?.Name;// Присвоює те що обрав користувач 
                       
            string cargoType = vm.SelectedCargoType?.CargoType;//Присвоюємо те що обрав користувач 
            double weight = double.Parse(ViewModel.WeightText);
            double volume = double.Parse(ViewModel.VolumeText);

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


            Transport createdTransport;

            try
            {
                switch (transportType)
                {
                    case "Газель":
                        createdTransport = new Gazell(transportType, weight, volume, vm.Condition, vm.CurrentRoute);
                        break;
                    case "Фура":
                        createdTransport = new Track(transportType, weight, volume, vm.Condition, vm.CurrentRoute);
                        break;
                    case "Бус":
                        createdTransport = new Beads(transportType, weight, volume, vm.Condition, vm.CurrentRoute);
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
