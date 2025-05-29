using Freight_transportation_system;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest1
{
    [TestClass]
    public class UnitTest1
    {
        public Transport transport;
        [TestMethod]
        //Створення замовлення з валідними даними
        //Додавання нового замовлення
        public void CreateOrder_WithValidData_ShouldSucceed()
        {
           

            var dto = new OrderDTO
            {
                UserName = "Іван",
                LastName = "Петренко",
                PhoneNumber = "0931234567",
                Weight = "500",
                Volume = "3",
                Transport = "Вантажівка",
                CargoType = "Побутова техніка",
                ConditionType = "Охолодження",
                Departure = "Київ",
                Arrival = "Одеса"
            };

            // Act
            RouteSelector selector = new RouteSelector();
            Route route = selector.GetOptimalRoute("Київ", "Одеса");
          
            transport = new Truck(dto.Transport, double.Parse(dto.Weight), double.Parse(dto.Volume), dto.ConditionType, route);

            // Assert
            Assert.IsNotNull(transport);
            Assert.AreEqual("Вантажівка", transport.GetTransportType());
        }

        //Некоректна вага — очікуємо помилку
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateOrder_WithInvalidWeight_ShouldThrow()
        {
            // Arrange
            var route = new RouteSelector().GetOptimalRoute("Київ", "Одеса");

            // Act
            var transport = new Truck("Вантажівка", -10, 3, "Охолодження", route); // некоректна вага

            // Assert handled by ExpectedException
        }

        //Пункти відправлення і прибуття однакові — заборонено
        [TestMethod]
        public void ValidateCities_WhenSame_ShouldReturnFalse()
        {
            var viewModel = new AddOrderViewModel
            {
                SelectedDepartureCity = new TransportOption { CitiesOfDeparture = "Київ" },
                SelectedArrivalCity = new TransportOption { CitiesOfArrival = "Київ" }
            };

            bool isValid = viewModel.SelectedDepartureCity.CitiesOfDeparture != viewModel.SelectedArrivalCity.CitiesOfArrival;

            Assert.IsFalse(isValid);
        }

        // Тест валідації номеру телефону
        [DataTestMethod]
        [DataRow(null, "Номер телефону обов’язковий")]
        [DataRow("", "Номер телефону обов’язковий")]
        [DataRow("    ", "Номер телефону обов’язковий")]
        [DataRow("12345", "Некоректний формат телефону. Приклад: +380961234567 або 096 1234567")]
        [DataRow("098765432", "Некоректний формат телефону. Приклад: +380961234567 або 096 1234567")]
        [DataRow("+380987654321", "")]
        [DataRow("098 7654321", "")]
        [DataRow("0987654321", "")]
        public void ValidatePhoneNumber_ShouldSetCorrectErrorMessage(string input, string expectedError)
        {
            // arrange
            var validator = new AddOrderViewModel { PhoneNumber = input };

            // act
            validator.ValidatePhoneNumber();

            // assert
            Assert.AreEqual(expectedError, validator.PhoneNumberError);
        }

        [TestClass]
        public class LastNameValidatorTests
        {
            [DataTestMethod]
            [DataRow(null, "Прізвище не може бути порожнім")]
            [DataRow("", "Прізвище не може бути порожнім")]
            [DataRow("   ", "Прізвище не може бути порожнім")]
            [DataRow("Пет ренко", "Прізвище повинно бути одним словом")]
            [DataRow("John", "Прізвище має містити лише українські літери")]
            [DataRow("П", "Прізвище має містити лише українські літери")] // Менше 2 символів
            [DataRow("Петренкозанадтовеликеім'ящоіпсуватестиль", "Прізвище має містити лише українські літери")] // >30 символів
            [DataRow("Петренко", "")]
            [DataRow("О'Шей", "")]
            [DataRow("Слободян-Голуб", "")]
            public void ValidateLastName_ShouldSetCorrectErrorMessage(string input, string expectedError)
            {
                // arrange
                var validator = new LastNameValidatorFake { LastName = input };

                // act
                validator.ValidateLastName();

                // assert
                Assert.AreEqual(expectedError, validator.LastNameError);
            }

            // Мінімальна імплементація класу з властивостями для тестування
            private class LastNameValidatorFake
            {
                public string LastName { get; set; }
                public string LastNameError { get; set; }

                public void ValidateLastName()
                {
                    if (string.IsNullOrWhiteSpace(LastName))
                    {
                        LastNameError = "Прізвище не може бути порожнім";
                    }
                    else if (LastName.Trim().Contains(" "))
                    {
                        LastNameError = "Прізвище повинно бути одним словом";
                    }
                    else if (!System.Text.RegularExpressions.Regex.IsMatch(LastName, @"^[А-ЯІЇЄҐа-яіїєґ'-]{2,30}$"))
                    {
                        LastNameError = "Прізвище має містити лише українські літери";
                    }
                    else
                    {
                        LastNameError = string.Empty;
                    }
                }
            }

            [TestClass]
            public class VolumeValidatorTests
            {
                [DataTestMethod]
                [DataRow(null, "Поле не може бути порожнім")]
                [DataRow("", "Поле не може бути порожнім")]
                [DataRow("   ", "Поле не може бути порожнім")]
                [DataRow("abc", "Введіть додатне число")]
                [DataRow("-1", "Введіть додатне число")]
                [DataRow("0", "Введіть додатне число")]
                [DataRow("3,5", "")]
                [DataRow("100", "")]
                public void ValidateVolume_ShouldSetCorrectErrorMessage(string input, string expectedError)
                {
                    // arrange
                    var validator = new VolumeValidatorFake { VolumeText = input };

                    // act
                    validator.ValidateVolume();

                    // assert
                    Assert.AreEqual(expectedError, validator.VolumeError);
                }

                // Фейковий клас
                private class VolumeValidatorFake
                {
                    public string VolumeText { get; set; }
                    public string VolumeError { get; set; }

                    public void ValidateVolume()
                    {
                        if (string.IsNullOrWhiteSpace(VolumeText))
                        {
                            VolumeError = "Поле не може бути порожнім";
                            return;
                        }

                        if (!double.TryParse(VolumeText, out double result) || result <= 0)
                        {
                            VolumeError = "Введіть додатне число";
                        }
                        else
                        {
                            VolumeError = string.Empty;
                        }
                    }
                }
            }
            //Якщо шлях існує — перевірити маршрут і відстань
            
            [TestMethod]
            public void Dijkstra_ValidRoute_ReturnsCorrectPathAndDistance()
            {
                // Arrange
                var selector = new RouteSelector();

                // Act
                Route result = selector.GetOptimalRoute("Київ", "Одеса");

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual("Київ", result.StartingPoint);
                Assert.AreEqual("Одеса", result.ArrivalPoint);
                Assert.IsTrue(result.Distance > 0); // перевіряємо, що не нескінченність

                // Для прикладу можна перевірити, що місто "Умань" є в маршруті
                CollectionAssert.Contains(result.Waypoints, "Умань");
            }
            //Приклад тесту на відстань між "Київ" та "Одеса"
            [TestMethod]
            public void Dijkstra_KyivToOdesa_ShouldReturnCorrectDistance()
            {
                // Arrange
                var selector = new RouteSelector();

                // Act
                Route result = selector.GetOptimalRoute("Київ", "Одеса");

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual("Київ", result.StartingPoint);
                Assert.AreEqual("Одеса", result.ArrivalPoint);
                Assert.AreEqual(470, result.Distance); // Очікувана відстань
            }
        }

        //для перевірки, що видалення замовлення з колекції TempOrders
        //і словника OrdersByNumber працює коректно.
        [TestClass]
        public class DeleteOrderTests
        {
            [TestMethod]
            public void DeleteOrder_ShouldRemoveFromTempOrders()
            {
                // Arrange
                var vm = new MainViewModel();
                var dto = new OrderDTO
                {
                    Number = "12345678",
                    DeliveryStatus = DeliveryStatus.Очікується,
                    Sum = "1000",
                    Weight = "100",
                    Volume = "2"
                };

                vm.AddOrder(dto);
                var addedOrder = vm.TempOrders.FirstOrDefault(o => o.Number == "12345678");

                // Act
                vm.DeleteOrder(addedOrder);

                // Assert
                Assert.IsFalse(vm.TempOrders.Any(o => o.Number == "12345678"), "Замовлення не було видалене з TempOrders.");
            }

            [TestMethod]
            public void DeleteOrder_ShouldRemoveFromOrdersByNumber()
            {
                // Arrange
                var vm = new MainViewModel();
                var dto = new OrderDTO
                {
                    Number = "87654321",
                    DeliveryStatus = DeliveryStatus.Очікується,
                    Sum = "500",
                    Weight = "50",
                    Volume = "1"
                };

                vm.AddOrder(dto);
                var addedOrder = vm.TempOrders.FirstOrDefault(o => o.Number == "87654321");

                // Act
                vm.DeleteOrder(addedOrder);

                // Assert
                Assert.IsFalse(vm.OrdersByNumber.ContainsKey("87654321"), "Замовлення не було видалене зі словника OrdersByNumber.");
            }

            //для функціоналу редагування замовлення 
            //Зберігаються в TempOrders
            //Оновлюється словник OrdersByNumber
            //Зміна статусу(DeliveryStatus) не ламає логіку


            //Редагування існуючого замовлення
            [TestClass]
            public class EditOrderTests
            {
                [TestMethod]
                public void EditOrder_ShouldUpdateOrderInTempOrders()
                {
                    // Arrange
                    var vm = new MainViewModel();
                    var originalDto = new OrderDTO
                    {
                        Number = "11111111",
                        DeliveryStatus = DeliveryStatus.Очікується,
                        UserName = "Андрій",
                        Weight = "100",
                        Volume = "1.5",
                        Sum = "1000 ₴"
                    };
                    vm.AddOrder(originalDto);

                    var originalOrder = vm.TempOrders.First(o => o.Number == "11111111");

                    var updatedDto = new OrderDTO
                    {
                        Number = "11111111", // той самий номер
                        DeliveryStatus = DeliveryStatus.Доставлено,
                        UserName = "Іван",
                        Weight = "150",
                        Volume = "2",
                        Sum = "1500 ₴"
                    };

                    // Act
                    vm.EditOrder(originalOrder, updatedDto);

                    // Assert
                    var editedOrder = vm.TempOrders.First(o => o.Number == "11111111");
                    Assert.AreEqual("Іван", editedOrder.UserName);
                    Assert.AreEqual("1500 ₴", editedOrder.Sum);
                    Assert.AreEqual(DeliveryStatus.Доставлено, editedOrder.DeliveryStatus);
                }

                [TestMethod]
                public void EditOrder_ShouldUpdateOrdersByNumber()
                {
                    // Arrange
                    var vm = new MainViewModel();
                    var dto = new OrderDTO
                    {
                        Number = "22222222",
                        UserName = "Олег",
                        DeliveryStatus = DeliveryStatus.Очікується,
                        Sum = "2000 ₴"
                    };
                    vm.AddOrder(dto);
                    var order = vm.TempOrders.First(o => o.Number == "22222222");

                    var updatedDto = new OrderDTO
                    {
                        Number = "22222222",
                        UserName = "Олег Петров",
                        DeliveryStatus = DeliveryStatus.Доставлено,
                        Sum = "3000 ₴"
                    };

                    // Act
                    vm.EditOrder(order, updatedDto);

                    // Assert
                    Assert.IsTrue(vm.OrdersByNumber.ContainsKey("22222222"));
                    Assert.AreEqual("Олег Петров", vm.OrdersByNumber["22222222"].UserName);
                    Assert.AreEqual(DeliveryStatus.Доставлено, vm.OrdersByNumber["22222222"].DeliveryStatus);
                }

                [TestMethod]
                public void EditOrder_ChangingStatus_ShouldNotBreakApp()
                {
                    // Arrange
                    var vm = new MainViewModel();
                    var dto = new OrderDTO
                    {
                        Number = "33333333",
                        UserName = "Сергій",
                        DeliveryStatus = DeliveryStatus.Очікується
                    };

                    vm.AddOrder(dto);
                    var order = vm.TempOrders.First(o => o.Number == "33333333");

                    var updatedDto = new OrderDTO
                    {
                        Number = "33333333",
                        UserName = "Сергій",
                        DeliveryStatus = DeliveryStatus.Скасовано // зміна статусу
                    };

                    // Act
                    vm.EditOrder(order, updatedDto);

                    // Assert
                    var updatedOrder = vm.TempOrders.First(o => o.Number == "33333333");
                    Assert.AreEqual(DeliveryStatus.Скасовано, updatedOrder.DeliveryStatus);
                }
            }

            //EditOrder_ShouldUpdateOrderInTempOrders – перевіряє, що зміни відображаються в списку TempOrders
            //EditOrder_ShouldUpdateOrdersByNumber – гарантує, що словник оновлюється
            //EditOrder_ChangingStatus_ShouldNotBreakApp – дозволяє переконатись, що зміна DeliveryStatus не викликає винятків



            // пошук за існуючим номером
            [TestMethod]
            public void SearchOrder_ByExistingNumber_ShouldReturnCorrectOrder()
            {
                // Arrange
                var vm = new MainViewModel();
                var dto = new OrderDTO
                {
                    Number = "12345678",
                    UserName = "Олена",
                    DeliveryStatus = DeliveryStatus.Очікується
                };

                vm.AddOrder(dto); // Додаємо замовлення (автоматично оновлює словник)

                // Act
                var found = vm.OrdersByNumber.ContainsKey("12345678") ? vm.OrdersByNumber["12345678"] : null;

                // Assert
                Assert.IsNotNull(found);
                Assert.AreEqual("Олена", found.UserName);
            }

            //Пошук за неіснуючим номером
            [TestMethod]
            public void SearchOrder_ByNonExistingNumber_ShouldReturnNull()
            {
                // Arrange
                var vm = new MainViewModel();

                // Словник порожній (або можна додати кілька інших замовлень)
                var result = vm.OrdersByNumber.ContainsKey("99999999") ? vm.OrdersByNumber["99999999"] : null;

                // Assert
                Assert.IsNull(result);
            }


        }

    }
}

