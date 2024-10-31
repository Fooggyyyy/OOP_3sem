using System;
using System.Collections.Generic;

public class Set
{
    private List<int> elements;

    public Set()
    {
        elements = new List<int>();
    }

    public Set(List<int> elements)
    {
        this.elements = elements;
    }

    public int this[int index]
    {
        get => elements[index];
        set => elements[index] = value;
    }

    public int Count => elements.Count;

    public void Add(int element)
    {
        if (!elements.Contains(element))
        {
            elements.Add(element);
        }
    }

    public void Remove(int element)
    {
        elements.Remove(element);
    }

    public static Set operator -(Set set, int element)
    {
        set.Remove(element);
        return set;
    }

    public static Set operator +(Set set, int element)
    {
        set.Add(element);
        return set;
    }
    
    public static Set operator +(Set set1, Set set2)
    {
        Set result = new Set(new List<int>(set1.elements));
        foreach (var elem in set2.elements)
        {
            result.Add(elem);
        }
        return result;
    }
    public static Set operator *(Set set1, Set set2)
    {
        Set result = new Set();
        foreach (var elem in set1.elements)
        {
            if (set2.elements.Contains(elem))
            {
                result.Add(elem);
            }
        }
        return result;
    }

    public static explicit operator int(Set set)
    {
        return set.Count;
    }

    public static bool operator false(Set set)
    {
        return set.Count < 3 || set.Count > 10;
    }

    public static bool operator true(Set set)
    {
        return set.Count >= 3 && set.Count <= 10;
    }

    public void RemoveDuplicates()
    {
        elements = new List<int>(new HashSet<int>(elements));
    }

    public override string ToString()
    {
        return string.Join(", ", elements);
    }

    public Production ProductionInfo { get; set; }

    public class Developer
    {
        public string FullName { get; set; }
        public int Id { get; set; }
        public string Department { get; set; }

        public Developer(string fullName, int id, string department)
        {
            FullName = fullName;
            Id = id;
            Department = department;
        }

        public override string ToString()
        {
            return $"{FullName} (ID: {Id}), Department: {Department}";
        }
    }

    public static class StatisticOperation
    {
        public static int Sum(Set set)
        {
            int sum = 0;
            for (int i = 0; i < set.Count; i++)
            {
                sum += set[i];
            }
            return sum;
        }

        public static int Difference(Set set)
        {
            int max = int.MinValue;
            int min = int.MaxValue;

            for (int i = 0; i < set.Count; i++)
            {
                if (set[i] > max) max = set[i];
                if (set[i] < min) min = set[i];
            }
            return max - min;
        }

        public static int CountElements(Set set)
        {
            return set.Count;
        }
    }

    public Developer DeveloperInfo { get; set; }
}

public class Production
{
    public int Id { get; set; }
    public string OrganizationName { get; set; }

    public Production(int id, string organizationName)
    {
        Id = id;
        OrganizationName = organizationName;
    }

    public override string ToString()
    {
        return $"{OrganizationName} (ID: {Id})";
    }
}

public static class SetExtensions
{
    public static int CharCount(this string str, char ch)
    {
        int count = 0;
        foreach (var c in str)
        {
            if (c == ch) count++;
        }
        return count;
    }

    public static Set RemoveminusElements(this Set set)
    {
       for(int i = 0;i < set.Count;i++)
        {
            if (set[i] < 0)
            {
                set.Remove(set[i]);
            }
        }
            
                
       return set;
    }
}

class Program
{
    static void Main()
    {
        Set set1 = new Set(new List<int> { -1, -2, 2, 3, 4, 4, 4 });
        Set set2 = new Set(new List<int> { 3, 4, 5, 6 });
        set1.RemoveDuplicates();

       
        set1 = set1 + 7;
        Console.WriteLine($"Set 1 после добавления 7: {set1}");

        set1 = set1 - -1;
        Console.WriteLine($"Set 1 после убирания -1: {set1}");

        set1.ProductionInfo = new Production(1001, "TechCorp");
        Console.WriteLine($"Производство: {set1.ProductionInfo}");

        set1.DeveloperInfo = new Set.Developer("John Doe", 101, "Development");
        Console.WriteLine($"Разработчик: {set1.DeveloperInfo}");

        Set unionSet = set1 + set2;
        Console.WriteLine($"Объединение Set 1 и Set 2: {unionSet}");

        Set intersectionSet = set1 * set2;
        Console.WriteLine($"Пересечение Set 1 и Set 2: {intersectionSet}");

        int setSize = (int)set1;
        Console.WriteLine($"Мощность Set 1: {setSize}");

        if (set1)
        {
            Console.WriteLine("Множество Set 1 имеет допустимый размер.");
        }
        else
        {
            Console.WriteLine("Множество Set 1 имеет недопустимый размер.");
        }

        Console.WriteLine($"Сумма элементов Set 1: {Set.StatisticOperation.Sum(set1)}");
        Console.WriteLine($"Разница между максимальным и минимальным элементами Set 1: {Set.StatisticOperation.Difference(set1)}");
        Console.WriteLine($"Количество элементов в Set 1: {Set.StatisticOperation.CountElements(set1)}");

        string CheckString = "Hello World!!!";
        Console.WriteLine("Кол во l в Hello world " + CheckString.CharCount('l'));
        set1.RemoveminusElements();
        Console.WriteLine(set1);

      
    }
}
