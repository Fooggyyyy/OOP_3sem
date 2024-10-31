using System;
using System.Collections.Generic;
using System.IO;
using OOP_3sem_Laba7;

// Интерфейс для лабораторной работы 5
interface ILaba5<T>
{
    void Add(T item);
    void Remove(T item);
    void Get(T item);
}


// Интерфейс IDrive
interface IDrive
{
    double Drive(int weight, int height);
}

// Абстрактный класс Sentient_Being
abstract class Sentient_Being
{
    protected int weight { get; set; }
    protected int height { get; set; }
    protected int age { get; set; }
}

sealed class Human : Sentient_Being, IDrive
{
    private string Name { get; set; }
    private string Lastname { get; set; }
    private string Surname { get; set; }

    public Human()
    {
        Name = "Неизвестно";
        Lastname = "Неизвестно";
        Surname = "Неизвестно";
        weight = 0;
        height = 0;
        age = 0;
    }

    public Human(string name, string lastname, string surname, int weight, int height, int age)
    {
        this.Name = name;
        this.Lastname = lastname;
        this.Surname = surname;
        this.weight = weight;
        this.height = height;
        this.age = age;
    }

    public override string ToString()
    {
        return $"{Name} {Lastname} {Surname}, Возраст: {age}, Вес: {weight} кг, Рост: {height} см";
    }

    double IDrive.Drive(int weight, int height)
    {
        return (height * 10) / weight;
    }
}

class Program
{
    static void Main()
    {
        Set<Human> humans = new Set<Human>();

        // Добавление людей
        humans.Add(new Human("Иван", "Иванов", "Иванович", 70, 175, 30));
        humans.Add(new Human("Петр", "Петров", "Петрович", 80, 180, 35));

        // Сохранение в файл
        humans.SaveToTextFile("C:/Users/user/source/repos/OOP_3sem_Laba7/humans.txt");

        // Загрузка из файла
        Set<Human> newHumans = new Set<Human>();
        newHumans.LoadFromTextFile(line =>
        {
            var parts = line.Split(',');
            return new Human(parts[0], parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]));
        }, "C:/Users/user/source/repos/OOP_3sem_Laba7/books.txt");

        Console.WriteLine("Загруженные люди:");
        newHumans.PrintItems();

        
    }
}
