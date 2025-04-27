using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;


namespace Freight_transportation_system
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<OrderRow> Orders { get; set; } = new ObservableCollection<OrderRow>();

        private List<OrderDTO> _originalOrders;
        public ObservableCollection<OrderRow> TempOrders { get; set; } = new ObservableCollection<OrderRow>();

        public static Action NotifyDataChanged = () => { };
        private string _totalSumText;
        public string TotalSumText
        {
            get => _totalSumText;
            set
            {
                _totalSumText = value;
                OnPropertyChanged(nameof(TotalSumText));
            }
        }

        public MainViewModel()
        {
            LoadOrders();
            UpdateTotalSum();
        }

        public void AddOrder(OrderDTO dto)
        {
            TempOrders.Add(OrderRow.FromDTO(dto));
            UpdateTotalSum();
        }

        public void EditOrder(OrderRow original, OrderDTO updatedDto)
        {
            int index = TempOrders.IndexOf(original);
            if (index >= 0)
            {
                TempOrders[index] = OrderRow.FromDTO(updatedDto);
                UpdateTotalSum();
            }
        }

        public void DeleteOrder(OrderRow selected)
        {
            TempOrders.Remove(selected);
            UpdateTotalSum();
        }

        public void UpdateTotalSum()
        {
            try
            {
                double sum = TempOrders
                    .Select(o => double.TryParse(o.Sum.Replace("₴", "").Trim(), out double val) ? val : 0)
                    .Sum();
                TotalSumText = $"Загальна сума: {sum:F2} ₴";
            }
            catch
            {
                TotalSumText = "Помилка обчислення суми";
            }
        }

        public void SaveOrders()
        {
            var dtos = TempOrders.Select(o => o.ToDTO()).ToList();

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
            };

            var json = JsonSerializer.Serialize(dtos, options);
            File.WriteAllText("orders.json", json);

            // Після успішного збереження оновлюємо основні Orders
            Orders = new ObservableCollection<OrderRow>(TempOrders.Select(o => o));
            _originalOrders = Orders.Select(o => o.ToDTO()).ToList(); // Оновлюємо копію
        }

        public void LoadOrders()
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
                    _originalOrders = loadedDtos;
                    Orders = new ObservableCollection<OrderRow>(loadedDtos.Select(OrderRow.FromDTO));
                    TempOrders = new ObservableCollection<OrderRow>(Orders.Select(o => o)); // ❗ Копія для роботи
                    UpdateTotalSum();
                }
            }
        }
        public void ResetOrders()
        {
            if (_originalOrders != null)
            {
                TempOrders = new ObservableCollection<OrderRow>(_originalOrders.Select(OrderRow.FromDTO));
                UpdateTotalSum();
            }
        }

        public void ApplyChanges()
        {
            Orders = new ObservableCollection<OrderRow>(TempOrders.Select(o => o));
            _originalOrders = Orders.Select(o => o.ToDTO()).ToList();
            SaveOrders();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
}
