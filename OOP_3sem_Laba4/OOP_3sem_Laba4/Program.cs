using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OOP_3sem_Laba4
{
    interface IDrive //Интерфейс для определения скорости в зависимости от роста и кол во колес
    {
        double Drive(int weight, int height);
    }

    interface ITransform //Интерфейс для трансформирования
    {
        void Transform();
    }

    interface IWheels //Интерфейс для установления количества колес в зависимости от роста и массы
    {
        int Wheels(int weight, int height);
    }

    //-------------------------------------------------------------

    abstract class Vehicle : IDrive, IWheels //Транспортное средство
    {
        abstract class Driving_Vehicle // Управление транспортным средством
        {
            protected internal bool Flag = false;

            public bool Start()
            {
                if(!this.Flag)
                {
                    Console.WriteLine("Заводим машину!!!");
                    this.Flag = true;
                    return this.Flag;
                }
                else
                {
                    Console.WriteLine("Машина уже заведена");
                    return this.Flag;
                }
            }
        }
        abstract class Engine //Двигатель
        {
            protected internal int strong { get; set; }

            public void EngineIsWoundUp(Driving_Vehicle dv)
            {
                if (dv.Start())
                    Console.WriteLine("Двигатель заведен");
                else
                    Console.WriteLine("Двигатель не заведен");
            }
        }

        protected int weight {  get; set; }
        protected int height { get; set; }
        public abstract int Wheels(int weight); //По заданию 4) 

        public int Wheels(int weight, int height) //С интерфейса
        {
            double result = weight / height;
            return (int)result;
        }
        double IDrive.Drive(int weight, int height) //С интерфейса          
        {
            return (this.weight/10) * this.height  * Wheels(this.weight, this.height);
        }

    }
     
    abstract class Sentient_Being //Разумное существо
    {
        //У каждого живого существа есть рост и вес
        protected int weight { get; set; }
        protected int height { get; set; }
        protected int age { get; set; }

    }

    //-------------------------------------------------------------

    sealed class Transformer : Car, ITransform //Трансформер наследуется от машины
    {
        private int strong { get; set; }

        public Transformer() : base() 
        {
            strong = 0;
        }

        public Transformer(string name, int weight, int height, int strong) : base(name, weight, height) 
        {
            this.strong = strong;
        }

        public override string ToString() 
        {
            return $"Transformer {Name}, Сила: {strong}, Вес: {weight} кг, Рост: {height} см";
        }

        void ITransform.Transform()
        {
            Console.WriteLine("Транфсформируемся в " + Name);
        }

        public int Strong(int weight, int height, int strong)
        {
            return weight * strong * height;
        }
    }

    sealed class Human : Sentient_Being, IDrive //Человек наследуется от разумного существа
    {
        private string Name { get; set; }
        private string Lastname { get; set; }
        private string Surname { get; set; }
        public Human() // Конструктор по умолчанию
        {
            Name = "Неизвестно";
            Lastname = "Неизвестно";
            Surname = "Неизвестно";
            weight = 0;
            height = 0;
            age = 0;
        }

        public Human(string name, string lastname, string surname, int weight, int height, int age) // Конструктор с параметрами
        {
            this.Name = name;
            this.Lastname = lastname;
            this.Surname = surname;
            this.weight = weight;
            this.height = height;
            this.age = age;
        }

        //Переопределяем все методы наследуемые от Object
        public override string ToString()
        {
            return $"{Name} {Lastname} {Surname}, Возраст: {age}, Вес: {weight} кг, Рост: {height} см";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Human))
                return false;

            Human other = (Human)obj;
            return this.Name == other.Name &&
                   this.Lastname == other.Lastname &&
                   this.Surname == other.Surname &&
                   this.age == other.age &&
                   this.weight == other.weight &&
                   this.height == other.height;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + (Name?.GetHashCode() ?? 0);
            hash = hash * 23 + (Lastname?.GetHashCode() ?? 0);
            hash = hash * 23 + (Surname?.GetHashCode() ?? 0);
            hash = hash * 23 + age.GetHashCode();
            hash = hash * 23 + weight.GetHashCode();
            hash = hash * 23 + height.GetHashCode();
            return hash;
        }
        //
        double IDrive.Drive(int weight, int height)
        {
            return (height * 10) / weight;
        }

    }

    //-------------------------------------------------------------

    class Car : Vehicle //Машина наследуется от транспортного средства
    {
        protected string Name { get; set; } // Имя Машины

        override public int Wheels(int weight)
        {
            return (int)weight/50;
        }

        public Car() // Конструктор по умолчанию
        {
            Name = "Неизвестно";
            weight = 0;
            height = 0;
        }

        public Car(string name, int weight, int height) // Конструктор с параметрами
        {
            this.Name = name;
            this.weight = weight;
            this.height = height;
        }
    }

    //-------------------------------------------------------------
    class Printer
    {
        public void IAmPrinting(object obj)
        {
            if (obj is Human human)
            {
                Console.WriteLine($"Это человек: {human.ToString()}");
            }
            else if (obj is Vehicle vehicle)
            {
                Console.WriteLine($"Это транспортное средство: {vehicle.ToString()}");
            }
            else
            {
                Console.WriteLine("Неизвестный объект.");
            }
        }
    }



    //-------------------------------------------------------------
    internal class Program
    {
        static void Main(string[] args)
        {
            // Создаем объект Transformer
            Transformer transformer = new Transformer("Optimus Prime", 5000, 600, 100);
            // Создаем объект Human
            Human human = new Human("John", "Doe", "Smith", 80, 180, 25);
            // Создаем объект Car
            Car car = new Car("BMW", 2000, 150);

            // Работа через интерфейс IDrive
            IDrive driveTransformer = transformer as IDrive;
            IDrive driveHuman = human as IDrive;

            // Вызов методов для Transformer через IDrive
            if (driveTransformer != null)
            {
                Console.WriteLine($"Transformer: {transformer.ToString()}");
                Console.WriteLine($"Drive speed: {driveTransformer.Drive(5000, 600)}\n");
            }

            // Вызов методов для Human через IDrive
            if (driveHuman != null)
            {
                Console.WriteLine($"Human: {human.ToString()}");
                Console.WriteLine($"Drive speed: {driveHuman.Drive(80, 180)}\n");
            }

            // Работа через интерфейс ITransform для Transformer
            ITransform transformable = transformer as ITransform;
            if (transformable != null)
            {
                transformable.Transform();
            }

            // Вызов методов для Car через Vehicle и интерфейс IWheels
            if (car is Vehicle vehicleCar)
            {
                Console.WriteLine($"Car: {car.ToString()}");
                Console.WriteLine($"Wheels count: {vehicleCar.Wheels(2000)}\n");
            }

            // Работа с Vehicle через ссылку на Vehicle
            Vehicle vehicleTransformer = transformer as Vehicle;
            if (vehicleTransformer != null)
            {
                Console.WriteLine($"Vehicle Transformer Wheels: {vehicleTransformer.Wheels(5000)}");
            }

            // Дополнительные методы классов:
            // Проверяем метод Equals у Human
            Human human2 = new Human("John", "Doe", "Smith", 80, 180, 25);
            Console.WriteLine($"Human1 Equals Human2: {human.Equals(human2)}");

            // Вызов метода GetHashCode для Human
            Console.WriteLine($"Human HashCode: {human.GetHashCode()}");

            // Вызов метода Strong для Transformer
            Console.WriteLine($"Transformer Strength: {transformer.Strong(5000, 600, 100)}");

            // Вызов метода Wheels для Transformer через Vehicle
            Console.WriteLine($"Transformer Wheels (через Vehicle): {vehicleTransformer.Wheels(5000)}");

            // Вызов методов для Car:
            Console.WriteLine($"Car Wheels: {car.Wheels(2000)}");

            // Прямой вызов метода Wheels для Transformer через объект
            Console.WriteLine($"Transformer Wheels (прямой вызов): {transformer.Wheels(5000, 600)}");

            // Создание объектов разных типов
            Printer printer = new Printer();
            Human human1 = new Human("Иван", "Иванов", "Иванович", 70, 175, 30);
            Transformer transformer1 = new Transformer("Optimus", 5000, 600, 100);
            Car car1 = new Car("BMW", 2000, 140);
             
            // Массив объектов, где объекты Car и Transformer наследуются от Vehicle
            object[] beings = new object[] { human1, transformer1, car1 };

            // Вызов метода IAmPrinting для каждого объекта
            foreach (var being in beings)
            {
                printer.IAmPrinting(being);
            }
        }
    }
}
