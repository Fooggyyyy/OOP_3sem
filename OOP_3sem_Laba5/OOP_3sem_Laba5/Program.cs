using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OOP_3sem_Laba5
{
    public interface IDrive //Интерфейс для определения скорости в зависимости от роста и кол во колес
    {
        double Drive(int weight, int height);
    }

    public interface ITransform //Интерфейс для трансформирования
    {
        void Transform();
    }

    public interface IWheels //Интерфейс для установления количества колес в зависимости от роста и массы
    {
        int Wheels(int weight, int height);
    }

    //-------------------------------------------------------------

    public abstract partial class Vehicle : IDrive, IWheels //Транспортное средство
    {
        public VehicleType Type { get; set; }
        protected int weight { get; set; }
        protected int height { get; set; }
        public abstract int Wheels(int weight); //По заданию 4) 

        public int Wheels(int weight, int height) //С интерфейса
        {
            double result = weight / height;
            return (int)result;
        }
        double IDrive.Drive(int weight, int height) //С интерфейса          
        {
            return (this.weight / 10) * this.height * Wheels(this.weight, this.height);
        }

    }


    abstract class Sentient_Being //Разумное существо
    {
        //У каждого живого существа есть рост и вес
        protected int weight { get; set; }
        protected int height { get; set; }
        protected int age { get; set; }

        public int WeightValue
        {
            get { return weight; }
        }

        public int HeightValue
        {
            get { return height; }
        }

        public int AgeValue
        {
            get { return age; }
        }

    }

    //-------------------------------------------------------------

    sealed class Transformer : Car, ITransform //Трансформер наследуется от машины
    {
        private EngineParameters engineParameters;
        private int year { get; set; }
        private int strong { get; set; }

        public int StrongValue
        {
            get { return strong; }
        }

        public int WeightValue
        {
            get { return weight; }
        }

        public int HeightValue
        {
            get { return height; }
        }

        public int YeartValue
        {
            get { return year; }
        }
        public Transformer() : base()
        {
            strong = 0;
        }

        public Transformer(string name, int weight, int height, int strong, int age) : base(name, weight, height)
        {
            this.strong = strong;
            this.year = age;
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

        public Transformer(EngineParameters engineParameters)
        {
            this.engineParameters = engineParameters;
            Console.WriteLine($"Тестирование двигателя: {engineParameters.ToString()}");
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
            return (int)weight / 50;
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
    public partial class Printer
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
    //5 Лаба
    public enum VehicleType //Предусмотренные типы транспортного средства
    {
        Motorcycle,
        Truck,
        Bicycle,
        Car,
        Transformer
    }

    public struct EngineParameters //Параметр мощности двигателя
    {
        public int HorsePower { get; set; }
        public int Volume { get; set; }

        public EngineParameters(int horsePower, int volume)
        {
            HorsePower = horsePower;
            Volume = volume;
        }

        public override string ToString()
        {
            return $"Мощность: {HorsePower}, Объем: {Volume} куб.см";
        }
    }

    public class Army<T> : IEnumerable<T> where T : class
    {
        private List<T> units;

        public Army(List<T> units)
        {
            this.units = units;
        }

        public Army()
        {
            units = new List<T>();
        }

        public void Add(T unit)
        {
            units.Add(unit);
        }

        public void Remove(T unit)
        {
            units.Remove(unit);
        }

        public void PrintUnit()
        {
            foreach (T unit in units)
                Console.WriteLine(unit.ToString());

        }

        public T GetUnit(int index)
        {
            if (index >= 0 && index < units.Count)
            {
                return units[index];
            }
            else
            {
                return null;
            }
        }

        public int GetCount()
        {
            return units.Count();
        }


        public void TransformerStrong(int strong)
        {
            foreach (T unit in units)
            {
                if (unit is Transformer)
                {
                    Transformer transformer = unit as Transformer;
                    if ((transformer.WeightValue * transformer.HeightValue * transformer.StrongValue) == strong)
                        Console.WriteLine(transformer.ToString());
                }
            }
        }

        public void UnitAge(int age)
        {
            foreach (T unit in units)
            {
                if (unit is Transformer)
                {
                    Transformer transformer = unit as Transformer;
                    if (transformer.YeartValue == age)
                        Console.WriteLine(transformer.ToString());
                }
                else if (unit is Human)
                {
                    Human Human = unit as Human;
                    if (Human.AgeValue == age)
                        Console.WriteLine(Human.ToString());

                }
            }
        }

        // Реализация интерфейса IEnumerable<T>
        public IEnumerator<T> GetEnumerator()
        {
            return units.GetEnumerator();
        }

        // Необходимая реализация интерфейса IEnumerable
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }

    class ArmyController
    {
        private Army<Transformer> transformerArmy;
        private Army<Human> humanArmy;

        public ArmyController(Army<Transformer> transformerArmy, Army<Human> humanArmy)
        {
            this.transformerArmy = transformerArmy;

            this.humanArmy = humanArmy;
        }

        public void PrintTransformersWithStrength(int strength)
        {
            Console.WriteLine($"Трансформеры с силой {strength}:");
            transformerArmy.TransformerStrong(strength);
        }

        public void PrintTotalCombatUnits()
        {
            int transformerCount = transformerArmy.GetCount();
            int humanCount = humanArmy.GetCount();
            Console.WriteLine($"Общее количество боевых единиц в армии: {transformerCount + humanCount}");
        }

        //Дополнительное задание 1
        public void LoadTransformersFromFile(string filePath = "C:/Users/user/source/repos/OOP_3sem_Laba5/OOP_3sem_Laba5/DOP1.txt")
        {

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length == 5)
                    {
                        string name = parts[0].Trim();
                        int weight = int.Parse(parts[1].Trim());
                        int height = int.Parse(parts[2].Trim());
                        int strong = int.Parse(parts[3].Trim());
                        int age = int.Parse(parts[4].Trim());

                        Transformer transformer = new Transformer(name, weight, height, strong, age);
                        transformerArmy.Add(transformer);
                    }
                }
            }
                Console.WriteLine("Трансформеры успешно загружены из файла.");
        }


    }


    //-------------------------------------------------------------
    internal class Program
    {
        static void Main(string[] args)
        {
            // Создаем объект Transformer
            Transformer transformer = new Transformer("Optimus Prime", 5000, 600, 100, 70);
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
            Transformer transformer1 = new Transformer("Optimus", 5000, 600, 100, 80);
            Car car1 = new Car("BMW", 2000, 140);

            // Массив объектов, где объекты Car и Transformer наследуются от Vehicle
            object[] beings = new object[] { human1, transformer1, car1 };

            // Вызов метода IAmPrinting для каждого объекта
            foreach (var being in beings)
            {
                printer.IAmPrinting(being);
            }

            //5 лаба 
            VehicleType typeENUM = VehicleType.Car;
            VehicleType typeENUM_1 = VehicleType.Transformer;
            Console.WriteLine("Тип средства передвижения по Enum: " + typeENUM + ". Так же его номер в перечислении: " + (int)typeENUM);
            Console.WriteLine("Второй Тип средства передвижения по Enum: " + typeENUM_1 + ". Так же его номер в перечислении: " + (int)typeENUM_1);

            EngineParameters engineParameters = new EngineParameters(300, 5000);
            Transformer transformerForEngineTest = new Transformer(engineParameters);

            transformerForEngineTest.ToString();

            // Создаем список объектов для армии
            List<Transformer> transformerArmy = new List<Transformer>()
        {
            new Transformer("Optimus Prime", 300, 300, 100, 70),
            new Transformer("Bumblebee", 7500, 500, 3000, 700),
            new Transformer("Megatron", 17000, 850, 10000, 800)
        };

            // Создаем объект армии трансформеров
            Army<Transformer> armyOfTransformers = new Army<Transformer>(transformerArmy);

            // Добавляем трансформера в армию
            armyOfTransformers.Add(new Transformer("Starscream", 12000, 700, 7000, 700));

            // Удаляем трансформера из армии
            Transformer toRemove = armyOfTransformers.GetUnit(1); // Удаляем Bumblebee
            armyOfTransformers.Remove(toRemove);

            // Выводим список всех трансформеров в армии
            Console.WriteLine("Армия трансформеров:");
            armyOfTransformers.PrintUnit();

            // Ищем трансформера с заданной силой
            Console.WriteLine("\nТрансформеры с силой 9 000 000:");
            armyOfTransformers.TransformerStrong(9000000);

            // Ищем трансформеров с определенным возрастом
            Console.WriteLine("\nТрансформеры с годом создания 700:");
            armyOfTransformers.UnitAge(700);

            // Создаем список людей для армии
            List<Human> humanArmy = new List<Human>()
        {
            new Human("John", "Doe", "Smith", 80, 180, 25),
            new Human("Jane", "Doe", "Brown", 65, 165, 30),
            new Human("Alice", "Cooper", "Johnson", 70, 170, 28)
        };

            // Создаем объект армии людей
            Army<Human> armyOfHumans = new Army<Human>(humanArmy);

            // Добавляем человека в армию
            armyOfHumans.Add(new Human("Bob", "Marley", "Jones", 75, 175, 40));

            // Удаляем человека из армии
            Human humanToRemove = armyOfHumans.GetUnit(0); // Удаляем John Doe
            armyOfHumans.Remove(humanToRemove);

            // Выводим список всех людей в армии
            Console.WriteLine("\nАрмия людей:");
            armyOfHumans.PrintUnit();

            // Ищем людей с определенным возрастом
            Console.WriteLine("\nЛюди с возрастом 30:");
            armyOfHumans.UnitAge(30);

            Console.WriteLine("Контроллер");

            // Создаем объекты Transformer
            Transformer transformer2 = new Transformer("Optimus Prime", 3, 3, 1, 70);
            Transformer transformer3 = new Transformer("Megatron", 17000, 850, 10000, 800);


            // Создаем список объектов для армии трансформеров
            List<Transformer> transformerArmy1 = new List<Transformer> { transformer1, transformer2, transformer3 };

            // Создаем объект армии трансформеров
            Army<Transformer> armyOfTransformers1 = new Army<Transformer>(transformerArmy1);

            // Создаем список людей для армии
            List<Human> humanArmy1 = new List<Human>
        {
            new Human("John", "Doe", "Smith", 80, 180, 25),
            new Human("Jane", "Doe", "Brown", 65, 165, 30),
            new Human("Alice", "Cooper", "Johnson", 70, 170, 28)
        };

            // Создаем контроллер армии
            ArmyController armyController = new ArmyController(armyOfTransformers1, armyOfHumans);
            armyController.LoadTransformersFromFile();

            // Печатаем трансформеров с заданной мощностью
            int desiredStrength = 9;
            armyController.PrintTransformersWithStrength(desiredStrength);

            // Печатаем общее количество боевых единиц в армии
            armyController.PrintTotalCombatUnits();

        }
    }
}

