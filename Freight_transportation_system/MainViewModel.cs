using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.Json;


namespace Freight_transportation_system
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<OrderRow> Orders { get; set; } = new ObservableCollection<OrderRow>();

        private List<OrderDTO> _originalOrders;
        public ObservableCollection<OrderRow> TempOrders { get; set; } = new ObservableCollection<OrderRow>();

        public static Action NotifyDataChanged = () => { };
        public Dictionary<string, OrderRow> OrdersByNumber { get; set; } = new Dictionary<string, OrderRow>();

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
            var orderRow = OrderRow.FromDTO(dto);
            TempOrders.Add(orderRow);
            OrdersByNumber[orderRow.Number] = orderRow;
            UpdateTotalSum();
        }

        public void EditOrder(OrderRow original, OrderDTO updatedDto)
        {
            int index = TempOrders.IndexOf(original);
            if (index >= 0)
            {
                OrdersByNumber.Remove(original.Number);  // Видалити стару версію із словника

                var updatedRow = OrderRow.FromDTO(updatedDto);
                TempOrders[index] = updatedRow;           //  Оновити список

                OrdersByNumber[updatedRow.Number] = updatedRow; //  Додати нову версію у словник

                UpdateTotalSum();
            }
        }

        public void DeleteOrder(OrderRow selected)
        {
            OrdersByNumber.Remove(selected.Number);
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
            var filtered = TempOrders
         .Where(o => o.DeliveryStatus != DeliveryStatus.Скасовано)
         .ToList();

            var dtos = filtered
                .Select(o => o.ToDTO())
                .ToList();

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
            };

            var json = JsonSerializer.Serialize(dtos, options);
            File.WriteAllText("orders.json", json);

            // Після успішного збереження оновлюємо основні Orders
            //Orders = new ObservableCollection<OrderRow>(TempOrders.Select(o => o));
            //_originalOrders = Orders.Select(o => o.ToDTO()).ToList(); // Оновлюємо копію

            Orders = new ObservableCollection<OrderRow>(filtered);
            TempOrders = new ObservableCollection<OrderRow>(filtered);
            _originalOrders = dtos;
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

                    //Відфільтрувати при завантаженні
                    var filteredDtos = loadedDtos
                                       .Where(dto => dto.DeliveryStatus != DeliveryStatus.Скасовано)
                                       .ToList();

                    _originalOrders = loadedDtos;
                    Orders = new ObservableCollection<OrderRow>(loadedDtos.Select(OrderRow.FromDTO));
                    TempOrders = new ObservableCollection<OrderRow>(Orders.Select(o => o)); // ❗ Копія для роботи
                    UpdateTotalSum();
                }

                OrdersByNumber = new Dictionary<string, OrderRow>();
                foreach (var order in TempOrders)
                {
                    if (!OrdersByNumber.ContainsKey(order.Number))
                        OrdersByNumber[order.Number] = order;
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
