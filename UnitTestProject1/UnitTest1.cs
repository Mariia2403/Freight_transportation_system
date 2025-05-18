using Freight_transportation_system;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTest1
{
    [TestClass]
    public class UnitTest1
    {
        public Transport transport;
        [TestMethod]
        //Створення замовлення з валідними даними
        public void CreateOrder_WithValidData_ShouldSucceed()
        {
           

            var dto = new OrderDTO
            {
                UserName = "Іван",
                LastName = "Петренко",
                PhoneNumber = "0931234567",
                Weight = "500",
                Volume = "3",
                Transport = "Фура",
                CargoType = "Побутова техніка",
                ConditionType = "Охолодження",
                Departure = "Київ",
                Arrival = "Одеса"
            };

            // Act
            RouteSelector selector = new RouteSelector();
            Route route = selector.GetOptimalRoute("Київ", "Одеса");
          
            transport = new Track(dto.Transport, double.Parse(dto.Weight), double.Parse(dto.Volume), dto.ConditionType, route);

            // Assert
            Assert.IsNotNull(transport);
            Assert.AreEqual("Фура", transport.GetTransportType());
        }

        //Некоректна вага — очікуємо помилку
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateOrder_WithInvalidWeight_ShouldThrow()
        {
            // Arrange
            var route = new RouteSelector().GetOptimalRoute("Київ", "Одеса");

            // Act
            var transport = new Track("Фура", -10, 3, "Охолодження", route); // некоректна вага

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


    }
}
