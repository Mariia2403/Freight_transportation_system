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


       
        public virtual double Weight
        {
            get => weight;
            set
            {
                if (value < 0)
                {

                    throw new ArgumentException("Значення не може бути меншим за нуль.");

                }
                if (value > maxWeight)
                {
                  

                    throw new ArgumentException($" Вага не може перевищувати максимальну межу {maxWeight} кг.");



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
                    throw new ArgumentException("Значення не може бути меншим за нуль.");

                }
                if (value > maxVolume)
                {
                   
                    throw new ArgumentException($"Об'єм не може перевищувати максимальний ліміт {maxVolume} m³.");
                    
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
