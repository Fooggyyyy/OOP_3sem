using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace OOP_Lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Типы, пункт а
            bool bool1 = false;
            byte byte1 = 12;
            sbyte sbyte1 = -12;
            char char1 = 'a';
            decimal decimal1 = 3;
            double double1 = 3.54;
            float float1 = 1.2f;
            int int1 = -800;
            uint uint1 = 800;
            long long1 = -100000;
            ulong ulong1 = 100000;
            short short1 = -12;
            ushort ushort1 = 12;
            object obj = "str";
            string string1 = Console.ReadLine();
            dynamic dynamic1 = null;
            Console.WriteLine(char1);
            Console.WriteLine(string1);

            //Типы, пункт b

            //Не явное приведение
            short nepriv1 = 12;
            int nepriv2 = nepriv1;

            float nepriv3 = 3.5f;
            double nepriv4 = nepriv3;

            char nepriv5 = 'a';
            object nepriv6 = nepriv5;

            short nepriv7 = 13;
            float nepriv8 = nepriv7;

            int nepriv9 = 100;
            long nepriv10 = nepriv9;

            //Явное приведение
            short priv1 = 12;
            int priv2 = (short)nepriv1;

            float priv3 = 3.5f;
            double priv4 = (float)nepriv3;

            char priv5 = 'a';
            object priv6 = (char)nepriv5;

            short priv7 = 13;
            float priv8 = (short)nepriv7;

            int priv9 = 100;
            long priv10 = (int)nepriv9;

            //Типы, пункт c

            //Упаковка
            int upakovka1 = 13;
            object upakovka = upakovka1;
            //Распаковка
            int raspakovka = (int)upakovka;

            //Convert
            int n = Convert.ToInt32("23");
            bool b = true;
            double d = Convert.ToDouble(b);

            //Типы, пункт d
            var var1 = 13;
            var var2 = "12";
            var var3 = new List<string>();

            //Типы, пункт с
            int? nullable = null;
            Console.WriteLine(nullable == null);
            Console.WriteLine(nullable.HasValue);
            Console.WriteLine(nullable ?? 55);

            //Типы, пункт f
            //После объявления за var "закрепляется" конкретный тип данных

            //Строки, пункт а
            string stru1 = "a";
            string stru2 = "bcfd345";

            int result = string.Compare(stru1, stru2);

            if (result < 0)
            {
                Console.WriteLine("Строка s1 перед строкой s2");
            }
            else if (result > 0)
            {
                Console.WriteLine("Строка s1 стоит после строки s2");
            }
            else
            {
                Console.WriteLine("Строки s1 и s2 идентичны");
            }

            //Строки, пункт b
            string str1;
            string str2 = "Hello, ";
            string str3 = "World!";

            str1 = str2 + str3;
            str2 = str3;
            Console.WriteLine(str1);
            string word = str1.Substring(0, str1.IndexOf('r'));
            Console.WriteLine(word);

            string[] words = str1.Split(' ');

            foreach (string word2 in words)
            {
                Console.WriteLine(word2);
            }

            str2 = str2.Insert(6, str3);
            Console.WriteLine(str2);

            str1 = str1.Remove(10);
            Console.WriteLine(str1);


            //Строки, пункт c
            string empty = " ";
            string nulll = null;

            Console.WriteLine(string.IsNullOrEmpty(nulll));
            Console.WriteLine(string.IsNullOrEmpty(empty));
            Console.WriteLine(string.IsNullOrWhiteSpace(empty));
            Console.WriteLine(string.IsNullOrWhiteSpace(nulll));

            //Строки, пункт d
            StringBuilder sb = new StringBuilder("Привет мир");
            sb.Remove(3, 4);
            sb.Insert(0, "Начало");
            sb.Append("Конец");
            Console.WriteLine(sb);

            //Массивы, пункт а
            
            int[,] array1 = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(array1[i, j] + " ");
                }
                Console.WriteLine();
            }

            //Массивы, пункт b
            string[] array2 = { "Hello", "My", "Name", "is", "Prokhor" };
            foreach (string s in array2)
            { Console.WriteLine(s); }
            Console.WriteLine("Length array2 is " + array2.Length);

            Console.Write("Выберите номер подстроки для замены до " + (array2.Length - 1));
            var choicestr1 = Console.ReadLine();

            int choice = Convert.ToInt32(choicestr1);

            string choicestr = Console.ReadLine();

            array2[choice] = choicestr;

            foreach (string s in array2)
            { Console.WriteLine(s); }

            //Массивы, пункт c

            double[][] jaggedArray = new double[3][];
            jaggedArray[0] = new double[2];
            jaggedArray[1] = new double[3];
            jaggedArray[2] = new double[4];

            for (int i = 0; i < jaggedArray.Length; i++)
            {
                for (int j = 0; j < jaggedArray[i].Length; j++)
                {
                    Console.Write($"Введите значение для jaggedArray[{i}][{j}]: ");
                    jaggedArray[i][j] = Convert.ToDouble(Console.ReadLine());
                }
            }

            Console.WriteLine("Введенные значения:");
            for (int i = 0; i < jaggedArray.Length; i++)
            {
                for (int j = 0; j < jaggedArray[i].Length; j++)
                {
                    Console.Write(jaggedArray[i][j] + " ");
                }
                Console.WriteLine();
            }
            //Массивы, пункт d
            var nocheck = "string";
            //var nocheck1 = { 1, 2, 3, 4, 5 };

            //Кортежи, пункт а
            var cortege = (1, 'c', "Hello", "sgr", 1234567890);
            Console.WriteLine(cortege.Item1);
            Console.WriteLine(cortege.Item2);
            Console.WriteLine(cortege.Item3);
            Console.WriteLine(cortege.Item4);
            Console.WriteLine(cortege.Item5);

            //Кортежи, пункт b
            var iitem1 = cortege.Item1;
            var iitem2 = cortege.Item2;
            var itiem3 = cortege.Item3;
            var iteim4 = cortege.Item4;
            var itemi5 = cortege.Item5;

            var (item1, _, item3, _, item5) = cortege;

            Console.WriteLine($"Item1: {item1}");
            Console.WriteLine($"Item3: {item3}");
            Console.WriteLine($"Item5: {item5}");

            //Кортежи, пункт c

            var tuple = (1, 'x', "gegreg", "wfergerg", 54654654645);

            Console.WriteLine(cortege == tuple);

            //Локальные функции

            (int, int, char) LocalFunction(int[] array, string str)
            {
                return (array.Max(), array.Min(), str[0]);
            }

            var LocalFunc = LocalFunction(new int[] { 1, 2, 3, 4 }, "Hello");
            Console.WriteLine(LocalFunc.Item1 + " " + LocalFunc.Item2 + " " + LocalFunc.Item3);

            //Checked, uncheked

            void LocalFunc1()
            {
                checked
                {
                    int i = int.MaxValue;
                    Console.WriteLine("checked");
                }
            }

            void LocalFunc2()
            {
                unchecked
                {
                    int i = int.MaxValue;
                    Console.WriteLine("unchecked");
                }

            }

            LocalFunc1();
            LocalFunc2();
        }


    }
}
