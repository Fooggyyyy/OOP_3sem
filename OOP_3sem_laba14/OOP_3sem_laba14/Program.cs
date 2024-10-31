using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;
using System.IO;
using System.Timers;

namespace OOP_3sem_laba14
{
    internal class Program
    {
        private static Mutex mutex = new Mutex();
        private static string filePath = "C:\\Users\\user\\source\\repos\\OOP_3sem_laba14\\OOP_3sem_laba14\\output.txt";
        private static int n = 10; 

        private static System.Timers.Timer _timer1;
        static void Main(string[] args)
        {
            //1 Задание
            Process[] All_Process = Process.GetProcesses();

            foreach (Process Process in All_Process)
            {
                try
                {
                    Console.WriteLine($"ID: {Process.Id}");
                    Console.WriteLine($"Имя: {Process.ProcessName}");
                    Console.WriteLine($"Приоритет: {Process.BasePriority}");
                    Console.WriteLine($"Время запуска: {Process.StartTime}");
                    Console.WriteLine($"Состояние: {(Process.Responding ? "Работает" : "Не отвечает")}");
                    Console.WriteLine($"Время работы процессора: {Process.TotalProcessorTime}\n");

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка в потоке {Process.ProcessName}, Ошибка: {ex.Message}\n");   
                }
            }

            //2 Задание
            AppDomain currentDomain = AppDomain.CurrentDomain;
            Console.WriteLine($"Имя домена: {currentDomain.FriendlyName}");
            Console.WriteLine("Загруженные сборки:");

            Assembly[] assemblies = currentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                Console.WriteLine(assembly.FullName);
            }

            AppDomain newDomain = AppDomain.CreateDomain("NewAppDomain");
            Console.WriteLine($"Создан новый домен: {newDomain.FriendlyName}");

            try
            {
                string assemblyPath = @"C:\Users\user\source\repos\OOP_3sem_laba13\packages\SoapCore.1.1.0.51\lib\net6.0\SoapCore.dll";
                Assembly assembly = Assembly.LoadFrom(assemblyPath);
                Console.WriteLine($"Сборка {assembly.FullName} загружена в новый домен.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке сборки: {ex.Message}");
            }

            // Выгрузка нового домена
            AppDomain.Unload(newDomain);
            Console.WriteLine("Домен выгружен.");

            //2 Задание

            // Создаем два потока
            Thread thread1 = new Thread(PrintEvenNumbers);
            Thread thread2 = new Thread(PrintOddNumbers);

            // Установим приоритет для одного из потоков
            thread1.Priority = ThreadPriority.Highest;

            // Запускаем потоки
            thread1.Start();
            thread2.Start();

            // Ждем завершения потоков
            thread1.Join();
            thread2.Join();

            Thread thread3 = new Thread(PrintEvenNumbers1);
            thread3.Start();

            thread3.Suspend();
            
            thread3.Resume();

            thread3.Join();
            //3 Задание

            _timer1 = new System.Timers.Timer(2000); 

            _timer1.Elapsed += OnTimedEvent;

            _timer1.Start();

            Console.WriteLine("Таймер запущен. Нажмите Enter для выхода...");
            Console.ReadLine();


            _timer1.Stop();
            _timer1.Dispose();


        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            // Выводим сообщение
            Console.WriteLine("Прошло 2 секунды!");
        }

        static void PrintEvenNumbers1()
        {
            for (int i = 0; i < n; i++)
            {
                if (i % 2 == 0)
                {
                    mutex.WaitOne();
                    Console.WriteLine("Четное число: " + i);
                    File.AppendAllText(filePath, "Четное число: " + i + Environment.NewLine);
                    mutex.ReleaseMutex(); // Освобождаем мьютекс
                    Thread.Sleep(1000);
                }
            }
        }
        static void PrintEvenNumbers()
        {
            for (int i = 0; i < n; i++)
            {
                if (i % 2 == 0)
                {
                    mutex.WaitOne(); 
                    Console.WriteLine("Четное число: " + i);
                    File.AppendAllText(filePath, "Четное число: " + i + Environment.NewLine);
                    mutex.ReleaseMutex(); // Освобождаем мьютекс
                }
            }
        }

        static void PrintOddNumbers()
        {
            for (int i = 0; i < n; i++)
            {
                if (i % 2 == 1)
                {
                    mutex.WaitOne(); 
                    Console.WriteLine("Нечетное число: " + i);
                    File.AppendAllText(filePath, "Нечетное число: " + i + Environment.NewLine);
                    mutex.ReleaseMutex(); // Освобождаем мьютекс
                }
            }
        }

    }
}
