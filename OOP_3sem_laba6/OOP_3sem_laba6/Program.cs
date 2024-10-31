using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace OOP_3sem_laba6
{
    class CheckNumber : Exception
    {
        public CheckNumber() : base("Некорректное число")
        {
        }

        public CheckNumber(string message) : base(message)
        {
        }

        public static void CheckInt(int number) 
        {
            if (number < 0 || number > 100)
            {
                throw new CheckNumber($"Число {number} не корректно!!!");
            }
            else
            {
                Console.WriteLine($"Число {number} корректно.");
            }
        }
    }

    class CheckString : Exception
    {
        public CheckString() : base("Некорректная строка")
        {

        }

        public CheckString(string message) : base(message)
        {
        }

        public static void CheckStr(string line)
        {
            if (line.Length < 3 || line.Length > 20)
            {
                throw new CheckNumber($"Строка {line} не корректна!!!");
            }
            else
            {
                Console.WriteLine($"Строка {line} корректна!!!");
            }
        }
    }

    class CheckClass : Exception
    {
        public CheckClass() : base("Некорректный класс")
        {
        }

        public CheckClass(string message) : base(message)
        {
        }

        public static void CheckInstance(ExampleClass example)
        {
            if (example.InstanceValue < 3 || example.InstanceValue > 5)
            {
                throw new CheckClass($"Некорректное количество экземпляров: {example.InstanceValue}");
            }
            else
            {
                Console.WriteLine($"Количество экземпляров: {example.InstanceValue} корректно.");
            }
        }
    }

    class ExampleClass
    {
        static int Instance = 0;
        public int InstanceValue
        {
            get { return Instance; }
        }
        public ExampleClass() { Instance++; }
    }


    internal class Program
    {
        static void Method1()
        {
            try
            {
                Method2();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Исключение в Method1: {ex.Message}");
                throw; // Пробрасываем исключение выше
            }
        }

        static void Method2()
        {
            try
            {
                Method3();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Исключение в Method2: {ex.Message}");
                throw; // Пробрасываем исключение выше
            }
        }

        static void Method3()
        {
            throw new InvalidOperationException("Ошибка в Method3: Некорректная операция.");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Проверка чисел");
            int Test1_Number = 1000;
            int Test2_Number = -1;
            int Test3_Number = 5;

            try
            {
                CheckNumber.CheckInt(Test1_Number);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }
            finally
            {
                Console.WriteLine($"Мы присвоили значению {Test1_Number} максимальное значение 100");
                Test1_Number = 100;
            }

            try
            {
                CheckNumber.CheckInt(Test2_Number);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                CheckNumber.CheckInt(Test3_Number);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Проверка строк");

            string Test1_String = "ab";
            string Test2_String = "Hello";
            string Test3_String = "This string is too long";

            try
            {
                CheckString.CheckStr(Test1_String);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                CheckString.CheckStr(Test2_String);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                CheckString.CheckStr(Test3_String);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Проверка экземпляров");
            ExampleClass example5 = new ExampleClass();

            try
            {
                CheckClass.CheckInstance(example5);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //////////////////////////
            try
            {
                Method1();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Исключение в Main: {ex.Message}");
            }

            ///////////////////
            Console.WriteLine("Введите положительное число:");

            // Чтение числа от пользователя
            string input = Console.ReadLine();
            int number;

            // Проверка, удалось ли преобразовать введённую строку в число
            if (int.TryParse(input, out number))
            {
                // Используем Assert для проверки, что число положительное
                Debug.Assert(number >= 0, "Введенное число должно быть положительным!");

                Console.WriteLine($"Вы ввели: {number}");
            }
            else
            {
                Console.WriteLine("Ошибка: введено не число.");
            }
        }
    }
}
