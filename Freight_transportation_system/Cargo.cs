using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Freight_transportation_system
{
  public class Cargo
    {
        public string type { get; set; }
        private double weight { get; set; }
        private double volume { get; set; }

        public string SpecialCondition { get; set; }

        private readonly double maxWeight; // Ліміт ваги
        private readonly double maxVolume; // Ліміт об'єму


        public bool ex = false;
        public virtual double Weight
        {
            get => weight;
            set
            {
                if (value < 0)
                {

                    throw new ArgumentException("The value cannot be less than zero.");

                }
                if (value > maxWeight)
                {
                    ex = true;
                    MessageBox.Show($"The volume cannot exceed the maximum limit of {maxVolume} m³.",
                    "Input Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                    return;
                    //  throw new ArgumentException($"The weight cannot exceed the maximum limit of {MaxWeight} kg.");

                }

                weight = value;

            }
        }

        public virtual double Volume
        {
            get => volume;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("The value cannot be less than zero.");

                }
                if (value > maxVolume)
                {
                    ex = true;
                    MessageBox.Show($"The volume cannot exceed the maximum limit of {maxVolume} m³.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                    // throw new ArgumentException($"The volume cannot exceed the maximum limit of {MaxVolume} m³.");

                }
                volume = value;
            }
        }

        public Cargo(string cargoType, double weight, double volume, string condition, double maxW, double maxV)
        {
            type = cargoType;
            maxWeight = maxW; // Отримуємо ліміти у конструкторі
            maxVolume = maxV;
            Weight = weight; // Встановлюємо через властивості (з перевіркою)
            Volume = volume;
            SpecialCondition = condition;
        }
    }
}
