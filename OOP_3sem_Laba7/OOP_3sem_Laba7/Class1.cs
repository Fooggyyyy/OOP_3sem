using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_3sem_Laba7
{

    // Класс Set с обобщениями
    public class Set<T> : ILaba5<T> where T : class
    {
        private List<T> _items = new List<T>();

        public void Add(T item)
        {
            _items.Add(item);
            Console.WriteLine($"{item} добавлен.");
        }

        public void Remove(T item)
        {
            _items.Remove(item);
            Console.WriteLine($"{item} удален.");
        }

        public void Get(T item)
        {
            if (_items.Contains(item))
            {
                Console.WriteLine($"{item} найден.");
            }
            else
            {
                Console.WriteLine($"{item} не найден.");
            }
        }

        public void SaveToTextFile(string filePath = "C:/Users/user/source/repos/OOP_3sem_Laba7/humans.txt")
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var item in _items)
                {
                    writer.WriteLine(item.ToString());
                }
            }
            Console.WriteLine($"Данные сохранены в файл {filePath}");
        }

        public void LoadFromTextFile(Func<string, T> parseFunc, string filePath = "C:/Users/user/source/repos/OOP_3sem_Laba7/books.txt")
        {
            _items.Clear();
            StreamReader reader = null; // Инициализация переменной reader

            try
            {
                reader = new StreamReader(filePath);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    T item = parseFunc(line);
                    _items.Add(item);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Общая ошибка: {ex.Message}");
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close(); // Закрываем поток, если он был открыт
                }
            }
            Console.WriteLine($"Данные загружены из файла {filePath}");
        }
        public void PrintItems()
        {
            foreach (var item in _items)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
