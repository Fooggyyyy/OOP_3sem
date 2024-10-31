using System;
using System.Threading;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;


internal class Program
{
    static public async Task Main(string[] args)
    {
        //Ко 2-ому заданию
        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        cancelTokenSource.CancelAfter(3000);
        CancellationToken token = cancelTokenSource.Token;

        //1-ое задание
        Console.WriteLine("1-ое задание. Выполнение алгроитма Эрастофена и вывод информации о задаче");

        Stopwatch stopwatch = new Stopwatch();

        Task taskErastofen = new Task(() => Erastofen(token));

        Task TaskInf = new Task(() => Console.WriteLine($"\nПромежуточный статус задачи: {taskErastofen.Status}" +
            $", Заверешна ли задача: {taskErastofen.IsCompleted}"));

        stopwatch.Start();
        taskErastofen.Start();

        TaskInf.Start();

        taskErastofen.Wait();
        stopwatch.Stop();

        InfarmationOfTask(taskErastofen);
        Console.WriteLine($"Время выполнения задачи: {stopwatch.ElapsedMilliseconds} мс");

        //2-ое задание
        Console.WriteLine("2-ое задание. Повторение первого, но с возможностью отменой задачи");

        Task taskErastofen1 = new Task(() => Erastofen(token), token);

        taskErastofen1.Start();

        taskErastofen1.Wait();

        //3-ee задание
        Console.WriteLine("3-ее и 4-ое задание с GetAwaiter и GetResult");
        int N = 30;

        Task<List<int>> EvenTask0 = Task<List<int>>.Run(() => Even(N));
        Task<List<int>> High10Task0 = new Task<List<int>>(() => High10(EvenTask0.Result));
        Task<List<int>> Low20Task0 = new Task<List<int>>(() => Low20(High10Task0.Result));
        var awaiter_even = EvenTask0.GetAwaiter();
        EvenTask0.Wait();

        awaiter_even.OnCompleted(() =>
        {
            High10Task0.Start();

            var awaiter_high = EvenTask0.GetAwaiter();

            High10Task0.Wait();

            awaiter_high.OnCompleted(() =>
            {
                Low20Task0.Start();
            });

        });

        //4-ое Задание

        Thread.Sleep(2000);
        Console.WriteLine("3-ее и 4-ое задание с ContinueWith");
        Task<List<int>> task_continue1 = new Task<List<int>>(() => Even(N));
        Task<List<int>> task_continue2 = task_continue1.ContinueWith(t1 => High10(t1.Result));
        Task<List<int>> task_continue3 = task_continue2.ContinueWith(t2 => Low20(t2.Result));

        task_continue1.Start();

        task_continue3.Wait();

        //5-ое задание
        Console.WriteLine("5-ое задание. Сравнение Паралельных и не паралельных for");

        Console.WriteLine("Сравнение for и Parallel.for на сортировке пузырьком");
        int[] sort_array = { 5,1,9,5,2,8 };

        Stopwatch stopwatch_for = new Stopwatch();
        Stopwatch stopwatch_parallel = new Stopwatch();

        stopwatch_for.Start();
        var for_array = BubbleSort_for(sort_array);
        stopwatch_for.Stop();

        stopwatch_parallel.Start();
        var parallel_array = BubbleSort_Parallel(sort_array);
        stopwatch_parallel.Stop();

        //Умный источник сказал, юзать Parallel.For/Parallel.Foreach использовать Parallel только если элементы не связаны между собой, например заполнение колллекции
        Console.WriteLine("Скорость обычного for при пузырьковой сортировке: " + stopwatch_for.ElapsedMilliseconds + " мс");
        Console.WriteLine("Скорость Parallel.for при пузырьковой сортировке: " + stopwatch_parallel.ElapsedMilliseconds + " мс");

        //6-ое задание
        Console.WriteLine("6-ое задание. Использовать Invoke, для параллельного использования задач");

        Parallel.Invoke(
            Print,
            () =>
            {
                Console.WriteLine($"Выполняется задача {Task.CurrentId}");
                Thread.Sleep(3000);
            },
            () => Square(5)
        );

        void Print()
        {
            Console.WriteLine($"Выполняется задача {Task.CurrentId}");
            Thread.Sleep(3000);
        }
        // вычисляем квадрат числа
        void Square(int n)
        {
            Console.WriteLine($"Выполняется задача {Task.CurrentId}");
            Thread.Sleep(3000);
            Console.WriteLine($"Результат {n * n}");
        }

        //7-ое задание
        Console.WriteLine("7-ое задание. Работа с BlockingCollection");

        BlockingCollection<int>[] Proiz = new BlockingCollection<int>[5];
        Proiz[0] = new BlockingCollection<int>(7);
        Proiz[1] = new BlockingCollection<int>(7);
        Proiz[2] = new BlockingCollection<int>(7);
        Proiz[3] = new BlockingCollection<int>(7);
        Proiz[4] = new BlockingCollection<int>(7);

        //Добавляем товары от разных поставщиков
        for(int i = 0; i < Proiz.Length; i++)
        {
            for(int j = 0; j < 7; j++)
            {
                Proiz[i].Add(j);
            }
        }

        Console.WriteLine("Все товары от всех поставщиков: ");
        for (int i = 0; i < Proiz.Length; i++)
        {
            await Console.Out.WriteLineAsync($"Все товары от поставщика номер {i+1}: ");
            for (int j = 0; j < 7; j++)
            {
                await Console.Out.WriteLineAsync($"Товар {j}");
            }
        }

        //Завершаем поставление товаров
        for (int i = 0; i < Proiz.Length; i++)
            Proiz[i].CompleteAdding();

      
        for(int i = 0;i < Proiz.Length; i++)
        {
            for(int j = 0; j < 7; j++)
            {
                Proiz[i].TryTake(out j);
            }
        }

        Console.WriteLine("Все товары от всех поставщиков после сбора: ");
        for (int i = 0; i < Proiz.Length; i++)
        {
            await Console.Out.WriteLineAsync($"Все товары от поставщика номер {i + 1}: ");
            for (int j = 0; j < Proiz[i].Count; j++)
            {
                await Console.Out.WriteLineAsync($"Товар {j}");
            }
        }

        //8-ое задание
        Console.WriteLine("8-ое задание. Рабоьта с async и await");
        await EvenAsync(N);

    }
    //Асинхронно переделываем метод Even
    public static async Task EvenAsync(int N)
    {
        Console.WriteLine("Начало асинхронного метода EvenAsync ");

        await Task.Run(() => Even(N));

        Console.WriteLine("Конец Асинхронного метода EvenAsync");
         
    }
    static int[] BubbleSort_Parallel(int[] array)
    {
        var len = array.Length;
        bool swapped;

        for (int i = 0; i < len; i++)
        {
            swapped = false;

            // Четные итерации: параллельно обрабатываем пары (0, 1), (2, 3), ...
            if (i % 2 == 0)
            {
                Parallel.For(0, len / 2, j =>
                {
                    int index = 2 * j;
                    if (index + 1 < len && array[index] > array[index + 1])
                    {
                        Swap(ref array[index], ref array[index + 1]);
                        swapped = true;
                    }
                });
            }
            // Нечетные итерации: параллельно обрабатываем пары (1, 2), (3, 4), ...
            else
            {
                Parallel.For(0, (len - 1) / 2, j =>
                {
                    int index = 2 * j + 1;
                    if (index + 1 < len && array[index] > array[index + 1])
                    {
                        Swap(ref array[index], ref array[index + 1]);
                        swapped = true;
                    }
                });
            }

            // Если за проход не было обменов, массив отсортирован
            if (!swapped) break;
        }

        return array;
    }

    static void Swap(ref int e1, ref int e2)
    {
        var temp = e1;
        e1 = e2;
        e2 = temp;
    }

    static int[] BubbleSort_for(int[] array)
    {
        var len = array.Length;
        for (var i = 1; i < len; i++)
        {
            for (var j = 0; j < len - i; j++)
            {
                if (array[j] > array[j + 1])
                {
                    Swap(ref array[j], ref array[j + 1]);
                }
            }
        }

        return array;
    }
    static public void InfarmationOfTask(Task task)
    {
        Console.WriteLine("Информация о задаче: ");
        Console.WriteLine($"Индефикатор задачи: {task.Id}");
        Console.WriteLine($"Статус выполнения: {task.AsyncState}");
    }

    static public void Erastofen(CancellationToken token)
    {
        var list = new List<int>();

        Console.Write("Введите до какого числа выводить простые числа: ");
        int N = Convert.ToInt32(Console.ReadLine());

        if (token.IsCancellationRequested)
        {
            Console.WriteLine("Задача отменена по времени.");
            return;
        }

        for (int s = 0; s < N; s++) 
        {
            list.Add(s);
        }

        list[1] = 0;
        list[2] = 0;

        int j = N;

        for (int i = 2; i < N; i++)
        {
            if (list[i] != 0)
                j = 2 * i;
            while (j < N)
            {
                list[j] = 0;
                j += i;
            }
        }

        list = list.Where(x => x != 0 && x != 4).ToList();

        Console.WriteLine($"Простые числа от 0 до {N} : ");
        foreach (var item in  list)
        {
            Console.WriteLine(item);
        }
    }


    //3 простых метода к 3-ему заданию
    public static List<int> Even(int N)
    {
        Console.WriteLine("Метод Even: ");
        List<int> list = new List<int>();

        for(int i = 0; i < N; i++)
        {
            if(i % 2 == 0)
            {
                Console.WriteLine(i);
                list.Add(i);
            }
                
        }
        return list;
    }
    public static List<int> High10(List<int> list)
    {
        Console.WriteLine("Метод High10: ");
        list = list.Where(x => x > 10).ToList();   

        foreach(var item in list)
        {
            Console.WriteLine(item);
        }
        return list;

    }

    public static List<int> Low20(List<int> list)
    {
        Console.WriteLine("Метод Low20: ");
        list = list.Where(x => x < 20).ToList(); 

        foreach(var item in list)
        {
            Console.WriteLine(item);
        }
        return list;

    }
}