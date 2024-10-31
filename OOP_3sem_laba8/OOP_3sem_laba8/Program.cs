using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_3sem_laba8
{
    class Worker
    {
        public string Name { get; set; }
        public string Post { get; set; }
        public int Money { get; set; } //В гривнах
        public Worker(string Name, string Post, int Money)
        {
            this.Name = Name;
            this.Post = Post;
            this.Money = Money;
        }

        public void DisplayInfo()
        {
            if(Name == "Декстер")
                Console.WriteLine("хаахахаххаахаахах декстер лох");
            else
                Console.WriteLine($"Имя: {Name}");
            Console.WriteLine($"Должность: {Post}");
            Console.WriteLine($"Зарплата: {Money}\n\n");
        }
    }
    class Direktor
    {
        private string Name { get; set; }

        public event Action<Worker, int> Up;
        public event Action<Worker, int> Down;

        public Direktor(string Name)
        {
            this.Name = Name;
        }

        public void UpMoney(Worker a, int b)
        {
            Up?.Invoke(a, b);
        }

        public void DownMoney(Worker a, int b)
        {
            Down?.Invoke(a, b);
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1-ое задание");
            Direktor dir = new Direktor("Егор Гончар");//Это что бы вы лабу сразу поставили

            //Ну да, декстер петух получает больше чем богдан на говно джеесе, ебет?
            Worker worker1 = new Worker("Богдан Кавецкий", "JS-разработчик", 40);
            Worker worker2 = new Worker("Декстер", "Петух", 80);

            worker1.DisplayInfo();
            worker2.DisplayInfo();

            dir.Up += (a, b) =>
            {
                a.Money += b;
                Console.WriteLine($"Этому {a.Name} который у нас {a.Post} докидываем на пиво и меф {b}");
            };

            dir.Down += (a, b) =>
            {
                a.Money -= b;
                Console.WriteLine($"{a.Name} {a.Post} соси хахахахаха, облапошен на {b}");
            };

            Console.WriteLine("События: ");
            dir.UpMoney(worker1, 400);
            dir.DownMoney(worker1, 8000);//А ибо нехер на говне писать богдан

            dir.UpMoney(worker2, 100000);//Потому что таких флэшрояльных ебанатов ценить надо

            worker1.DisplayInfo();
            worker2.DisplayInfo();

            Console.WriteLine("2-ое задание");

            //1-ый метод
            Console.WriteLine("\n1-ый метод\n\n");
            Console.WriteLine("Исходный: Тестим");
            Predicate<string> One = input => string.IsNullOrEmpty(input);
            Console.WriteLine("Он пустой? Ответ: " + One("Тестим"));

            //2-ой метод
            Console.WriteLine("\n\n2-ой метод\n\n");
            Func<string, string> removePunctuation = str =>
            {
                string result = "";
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] == ',' || str[i] == '.' || str[i] == '?' || str[i] == '!')
                        continue;
                    result += str[i];
                }
                return result;
            };
            Console.WriteLine("Исходный: Оп. OOp, HEllo!!!");
            string Two = removePunctuation("Оп. OOp, HEllo!!!");
            Console.WriteLine(Two);

            //3-ий метод
            Console.WriteLine("\n\n3-ий метод\n\n");
            Func<string, string> addSymbol = str =>
            {
                return str + "!";
            };
            Console.WriteLine("Исходный: Привет");
            string Three = addSymbol("Привет");
            Console.WriteLine("Добавление символа: " + Three);

            //4-ый метод
            Console.WriteLine("\n\n4-ый метод\n\n");
            Func<string, string> toUpperCase = str =>
            {
                return str.ToUpper();
            };
            Console.WriteLine("Исходный: hello world");
            string Four = toUpperCase("hello world");
            Console.WriteLine("Заглавные буквы: " + Four);
            
            //5-ый метод
            Console.WriteLine("\n\n5-ый метод\n\n");
            Func<string, string> removeAllSpaces = str =>
            {
                return str.Replace(" ", "");
            };
            Console.WriteLine("Исходный:  Это   пример  строки  ");
            string Five = removeAllSpaces("  Это   пример  строки  ");
            Console.WriteLine("Без пробелов: " + Five);

            Console.WriteLine("\n\n");

        }
    }
}
