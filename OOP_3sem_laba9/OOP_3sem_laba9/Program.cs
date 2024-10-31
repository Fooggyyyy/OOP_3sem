using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

interface IList
{
    void PrintValue();
}

public class MyDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>, IList
{
    private Dictionary<TKey, TValue> _internalDictionary;

    public MyDictionary()
    {
        _internalDictionary = new Dictionary<TKey, TValue>();
    }

    public int Count => _internalDictionary.Count;

    public void Add(TKey key, TValue value)
    {
        if (_internalDictionary.ContainsKey(key))
        {
            Console.WriteLine("Ключ уже существует в словаре"); 
        }

        _internalDictionary.Add(key, value);
    }

    public TValue Get(TKey key)
    {
       return _internalDictionary[key];
    }

    public bool Remove(TKey key)
    {
        return _internalDictionary.Remove(key);
    }

    public bool ContainsKey(TKey key)
    {
        return _internalDictionary.ContainsKey(key);
    }

    public TValue this[TKey key]
    {
        get
        {
            return Get(key);
        }
        set
        {
            _internalDictionary[key] = value;
        }
    }

    public void Clear()
    {
        _internalDictionary.Clear();
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return _internalDictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void PrintValue()
    {
        foreach (TValue value in _internalDictionary.Values)
        {
            Console.WriteLine(value);
        }
    }
}

interface IOrderedDictionary
{
    void sort();
}
public class ConcurrentBag<T> : IEnumerable<T>, IOrderedDictionary
{
    private T[] _items;     
    private int _count;       

    public ConcurrentBag()
    {
        _items = new T[4];    
        _count = 0;           
    }

    public int Count => _count;

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
                throw new ArgumentOutOfRangeException(nameof(index), "Индекс выходит за границы.");
            return _items[index];
        }
        set
        {
            if (index < 0 || index >= _count)
                throw new ArgumentOutOfRangeException(nameof(index), "Индекс выходит за границы.");
            _items[index] = value;
        }
    }

    public void Add(T item)
    {
        if (_count == _items.Length)
        { 
            Array.Resize(ref _items, _items.Length * 2);
        }
        _items[_count] = item;
        _count++;
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= _count)
            throw new ArgumentOutOfRangeException(nameof(index), "Индекс выходит за границы.");

        for (int i = index; i < _count - 1; i++)
        {
            _items[i] = _items[i + 1];  
        }
        _items[_count - 1] = default(T); 
        _count--;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < _count; i++)
        {
            yield return _items[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void sort()
    {
        _items.OrderBy(pair => pair).ToList();
    }
}



namespace OOP_3sem_laba9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyDictionary<string, int> myDict = new MyDictionary<string, int>();

            myDict.Add("Apple", 1);
            myDict.Add("Banana", 2);
            myDict.Add("Cherry", 3);

            Console.WriteLine("Значение для ключа 'Banana': " + myDict.Get("Banana"));

            Console.WriteLine("Все значения в словаре:");
            myDict.PrintValue();

            myDict.Remove("Cherry");
            Console.WriteLine("После удаления 'Cherry':");
            myDict.PrintValue();

            Console.WriteLine("Содержит ли ключ 'Apple'? " + myDict.ContainsKey("Apple"));
            Console.WriteLine("Содержит ли ключ 'Cherry'? " + myDict.ContainsKey("Cherry")); // Ожидается false

            myDict.Clear();
            Console.WriteLine("Количество элементов после очистки: " + myDict.Count);

            ObservableCollection<ConcurrentBag<string>> bags = new ObservableCollection<ConcurrentBag<string>>();

            bags.CollectionChanged += Bags_CollectionChanged;

            var bag1 = new ConcurrentBag<string>();
            bag1.Add("Item 1");
            bags.Add(bag1);

            var bag2 = new ConcurrentBag<string>();
            bag2.Add("Item 2");
            bags.Add(bag2);

          
            bags.Remove(bag1); 

            Console.WriteLine("Текущие ConcurrentBag в коллекции:");
            foreach (var bag in bags)
            {
                Console.WriteLine($"Bag содержит {bag.Count} элементов.");
            }
        }

        private static void Bags_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (ConcurrentBag<string> newBag in e.NewItems)
                {
                    Console.WriteLine($"Добавлен новый ConcurrentBag с {newBag.Count} элементами.");
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (ConcurrentBag<string> oldBag in e.OldItems)
                {
                    Console.WriteLine($"Удален ConcurrentBag с {oldBag.Count} элементами.");
                }
            }
        }
    }
}

/*
5. Отличия коллекций в пространстве имен System.Collections.Concurrent
Коллекции в System.Collections.Concurrent предназначены для безопасной работы в многопоточной среде. 
Они обеспечивают автоматическую синхронизацию, что позволяет избежать конфликтов при одновременном доступе
из нескольких потоков. Например, ConcurrentDictionary<TKey, TValue> обеспечивает безопасный доступ при добавлении, удалении и изменении элементов.

8. Охарактеризуйте интерфейсы IEnumerator и IEnumerable
IEnumerable: Предоставляет метод для получения перечислителя (GetEnumerator()), который позволяет перебрать коллекцию.
IEnumerator: Предоставляет методы для итерации по коллекции: MoveNext(), Reset(), и свойство Current, которое возвращает текущий элемент.
IEnumerator позволяет управлять итерацией по коллекции.

В среде .NET Framework поддерживаются пять типов коллекций: необобщенные, наблюдаемые, с поразрядной организацией, обобщенные и параллельные.
*/
