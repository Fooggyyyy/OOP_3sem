using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OOP_3sem_laba10
{
    class MyList<T> : IEnumerable<T> where T : Student
    {
        private T[] _items;
        private int _count;

        public MyList()
        {
            _items = new T[4]; 
            _count = 0;
        }

        public int Count => _count;

        public void Add(T item)
        {
            if (_count == _items.Length)
            {
                Array.Resize(ref _items, _items.Length * 2);
            }

            _items = _items.Take(_count).Concat(new[] { item }).ToArray();
            _count++; 
        }

        public bool Remove(T item)
        {
            var index = Array.IndexOf(_items, item, 0, _count); 

            if (index >= 0) 
            {
                for (int i = index; i < _count - 1; i++) 
                {
                    _items[i] = _items[i + 1];
                }
                _items[--_count] = default; 
                return true; 
            }
            return false; 
        }


        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                    throw new ArgumentOutOfRangeException(nameof(index), "Индекс выходит за границы.");
                return _items[index];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _count; i++)
            {
                yield return _items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    class Student
    {
        private string name;
        public string Name => name;
        private string surname;
        private string dadname;

        public Student(string name, string surname, string dadname)
        {
            this.name = name;
            this.surname = surname;
            this.dadname = dadname;

        }

        public override string ToString()
        {
            return $"Student: {name} {surname} {dadname}";
        }

    }

    class student
    {
        private string _name;
        private string _surname;
        private int _group;
        private string _fakultat;
        private string _specialty;
        private int _year;

        public string Name { get { return _name;} }
        public string Surname{ get { return _surname; } }
        public int Group { get { return _group; } }
        public string Fakultat { get { return _fakultat; } }
        public string Specialty { get { return _specialty; } }

        public int Year { get { return _year; } } 

        public student(string name, string surname, int group, string fakultat, string specialty, int year)
        {
            _name = name;
            _surname = surname;
            _group = group;
            _fakultat = fakultat;
            _specialty = specialty;
            _year = year;
        }

        public override string ToString()
        {
            return $"{Surname} {Name}, Группа: {_group}, Факультет: {_fakultat}, Специальность: {_specialty}, Возраст: {_year}";
        }
    }

    class S
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int CourseId { get; set; } 

        public override string ToString()
        {
            return $"{Surname} {Name} (ID: {Id}, Course ID: {CourseId})";
        }
    }

    class C
    {
        public int Id { get; set; }
        public string CourseName { get; set; }

        public override string ToString()
        {
            return $"{Id}: {CourseName}";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string[] Month = {"January", "February", "March", "April",
                "May", "June", "July", "August",
                "September", "October", "November", "December"};

            Console.WriteLine("Запрос 1: ");
            Console.Write("Введите до какого номера вывести месяца: ");
            int number_query1 = Convert.ToInt32(Console.ReadLine());
            var query1 = Month.Take(number_query1);

            foreach (var item in query1) { Console.WriteLine(item); }

            Console.WriteLine("\nЗапрос 2: ");
            var query2 = Month.Select((word, index) => new { word, index })
            .Where(x => (x.index >= 0 && x.index <= 1) || (x.index >= 5 && x.index <= 7) || x.index == 11)
            .Select(x => x.word);

            foreach (var item in query2) { Console.WriteLine(item); }

            Console.WriteLine("\nЗапрос 3: ");
            var query3 = Month.OrderBy(month => month);
            foreach (var item in query3) { Console.WriteLine(item); }

            Console.WriteLine("\nЗапрос 4: ");
            var query4 = Month.Where(word => word.Length >= 4)
                .Where(word => word.Contains("u"));


            foreach (var item in query4) { Console.WriteLine(item); }

            //List<T> 
            MyList<Student> studentList = new MyList<Student>();

            studentList.Add(new Student("Иван", "Иванов", "Иванович"));
            studentList.Add(new Student("Петр", "Петров", "Петрович"));
            studentList.Add(new Student("Сергей", "Сергеев", "Сергеевич"));
            studentList.Add(new Student("Александр", "Александров", "Александрович"));
            studentList.Add(new Student("Дмитрий", "Дмитриев", "Дмитриевич"));
            studentList.Add(new Student("Елена", "Еленина", "Еленовна"));
            studentList.Add(new Student("Мария", "Мариевна", "Мариевна"));
            studentList.Add(new Student("Анна", "Аннева", "Анновна"));
            studentList.Add(new Student("Анастасия", "Анастасиева", "Анастасиевна"));
            studentList.Add(new Student("Максим", "Максимов", "Максимович"));
            var StudentRemove = new Student("Он", "Для", "Удаления");

            studentList.Add(StudentRemove);
            studentList.Remove(StudentRemove);

            Console.WriteLine("Список студентов:");
            for (int i = 0; i < studentList.Count; i++)
            {
                Console.WriteLine(studentList[i]);
            }

            //Задание 3
            List<student> students = new List<student>
            {
                // Три студента одной специальности
                new student("Иван", "Иванов", 101, "Факультет информационных технологий", "Программирование", 15),
                new student("Петр", "Петров", 102, "Факультет информационных технологий", "Программирование", 17),
                new student("Иван", "Сидоров", 102, "Факультет информационных технологий", "Программирование", 19),

                // Три студента другой специальности и факультета
                new student("Анна", "Бондарь", 101, "Факультет экономики", "Бухгалтерский учет", 14),
                new student("Мария", "Марьева", 101, "Факультет экономики", "Бухгалтерский учет", 14),
                new student("Олег", "Олегов", 101, "Факультет экономики", "Бухгалтерский учет", 18)
            };

            Console.WriteLine("\nЗапрос для студентов 1: ");
            string spec = "Программирование";
            string fakul = "Факультет информационных технологий";
            var student_query1 = students
            .Where(s => s.Specialty == spec)
            .OrderBy(s => s.Surname);

            foreach (var student in student_query1) { Console.WriteLine(student); }

            Console.WriteLine("\nЗапрос для студентов 2: ");
            var student_query2 = students
            .Where(s => s.Specialty == spec)
            .Where(s => s.Fakultat == fakul);
            foreach (var student in student_query2) { Console.WriteLine(student); }

            Console.WriteLine("\nЗапрос для студентов 3: ");
            var student_query3 = students.
                Where(s => s.Year == students.Min(y => y.Year));
            foreach (var student in student_query3) {  Console.WriteLine(student); }

            Console.WriteLine("\nЗапрос для студентов 4: ");
            int group = 101;
            var student_query4 = students
            .Where(s => s.Group == group)
            .OrderBy(s => s.Surname);
            foreach (var student in student_query4) { Console.WriteLine(student); }

            Console.WriteLine("\nЗапрос для студентов 5: ");
            var student_query5 = students.Where(s => s.Name == "Иван").Take(1);
            foreach (var student in student_query5) {  Console.WriteLine(student); }

            //Задание 4
            Console.WriteLine("\nЗадание 4: ");
            var Final = students.Where(s => s.Year == students.Min(y => y.Year) || students.Any(k => k.Group > 0))
                .OrderBy(s => s.Surname)
                .Take(1);

            foreach(var item in Final) { Console.WriteLine(item);}

            //Задание 5
            Console.WriteLine("\nЗадание 5: ");
            List<S> students1 = new List<S>
            {
                new S { Id = 1, Name = "Иван", Surname = "Иванов", CourseId = 1 },
                new S{ Id = 2, Name = "Петр", Surname = "Петров", CourseId = 2 },
                new S { Id = 3, Name = "Сидор", Surname = "Сидоров", CourseId = 1 },
                new S { Id = 4, Name = "Анна", Surname = "Анна", CourseId = 3 }
            };

            List<C> courses = new List<C>
            {
                new C { Id = 1, CourseName = "Программирование" },
                new C { Id = 2, CourseName = "Математика" },
                new C { Id = 3, CourseName = "Физика" }
            };

            var studentCourses = students1.Join(courses,
                student => student.CourseId,
                course => course.Id,
                (student, course) => new
                {
                    FullName = $"{student.Surname} {student.Name}",
                    CourseName = course.CourseName
                });

            foreach (var sc in studentCourses)
            {
                Console.WriteLine($"{sc.FullName} записан на курс: {sc.CourseName}");
            }

            
        }
    }
}
