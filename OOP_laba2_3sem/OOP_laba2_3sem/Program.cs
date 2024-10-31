using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_laba2_3sem
{
    partial class Student
    {
        public string UniversityGS => university;

        public string NameGS
        {
            get => name;
            set => name = value;
        }

        public string SurnameGS
        { get { return surname; } set { surname = value; } }

        public string DadnameGS
        { get { return dadname; } set { dadname = value; } }

        public string AdressGS
        { get { return adress; } set { adress = value; } }

        public string FakultatGS
        { get { return fakultat; } set { fakultat = value; } }

        public long PhoneNumberGS
        { get { return phoneNumber; } set { phoneNumber = value; } }

        public DateTime BirthdayDateGS
        { get { return birthdayDate; } set { birthdayDate = value; } }

        //____________________________________________________________________

        public int IdGS
        { get { return id; } }
        public int GroupGS
        { 
        get { return group; }

        set //Проверка на группу, ключевое слово value
        { 
            if(value < 1)
            {
                group = 1;
                return;
            }
                
            if(value > 10)
            {
                group = 10;
                return;
            }
            group = value;
        }
        } 
        public int CourseGS //Проверка на курс, ключевое слово value
        { 
        get { return course; }

        set 
        {
            if (value < 1)
            {
                course = 1;
                return;
            }

            if (value > 4)
            {
                course = 10;
                return;
            }
            course = value;
        } 
        } 

        //____________________________________________________________________

        static Student()
        {
            Console.WriteLine("Статический конструктор");
        }
        public Student()//Конструкток по умолчнаию
        {
            Console.WriteLine("Публичный конструктор по умолчанию");
            instanceCount++;
        }
        public Student(string name, string surname, string dadname)
        {
            this.name = name;
            this.surname = surname;
            this.dadname = dadname;
            instanceCount++;

        }
        public Student(string adress, string fakultat, int id, int course, int group, long phoneNumber, DateTime birthdayDate)
        {
            this.adress = adress;
            this.fakultat = fakultat;

            this.id = id * group * course;
            this.course = course;
            this.group = group;
            this.phoneNumber = phoneNumber;

            this.birthdayDate = birthdayDate;
            instanceCount++;
        }

        private Student(int id)//Сделан для того, что бы не можно было напрямую создать объект.Для базовых настроек при вызова с дочерних классов
        {
            
        }   

        //____________________________________________________________________

        public static int InstanceCount()
        {
            return instanceCount;
        }

        public static void GetInformation()
        {
            Console.WriteLine("Это класс Student, описывающий параметры, свойственные студенту БГТУ.");
        }

        public int HetHashID(out int id, ref int course, ref int group)
        {
            id = group * course;
            return id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Student other = (Student)obj;
            return id == other.id &&
                   name == other.name &&
                   surname == other.surname &&
                   dadname == other.dadname &&
                   adress == other.adress &&
                   fakultat == other.fakultat &&
                   course == other.course &&
                   group == other.group &&
                   phoneNumber == other.phoneNumber &&
                   birthdayDate == other.birthdayDate;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + id.GetHashCode();
            hash = hash * 31 + (name != null ? name.GetHashCode() : 0);
            hash = hash * 31 + (surname != null ? surname.GetHashCode() : 0);
            hash = hash * 31 + (dadname != null ? dadname.GetHashCode() : 0);
            hash = hash * 31 + (adress != null ? adress.GetHashCode() : 0);
            hash = hash * 31 + (fakultat != null ? fakultat.GetHashCode() : 0);
            hash = hash * 31 + course.GetHashCode();
            hash = hash * 31 + group.GetHashCode();
            hash = hash * 31 + phoneNumber.GetHashCode();
            hash = hash * 31 + birthdayDate.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            return $"Student: {name} {surname} {dadname}, University: {university}, " +
                   $"Course: {course}, Group: {group}, Fakultet: {fakultat}, " +
                   $"Phone: {phoneNumber}, Birthday: {birthdayDate.ToShortDateString()}";
        }

        public int GetAgeStudent()
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthdayDate.Year;

            if (birthdayDate > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Student studentttt = new Student();//Экземлпяр класса
            Student studentt = new Student("Гулецкий", "Прохор", "Олегович");
            // Вызов статического метода для получения информации о классе
            Student.GetInformation();

            // Создание первого объекта с использованием конструктора по умолчанию
            Student student1 = new Student();
            student1.NameGS = "John";
            student1.SurnameGS = "Doe";
            student1.DadnameGS = "Smith";
            student1.AdressGS = "123 Main St";
            student1.FakultatGS = "Engineering";
            student1.CourseGS = 2;
            student1.GroupGS = 3;
            student1.PhoneNumberGS = 1234567890L;
            student1.BirthdayDateGS = new DateTime(2000, 5, 15);

            Console.WriteLine($"Student 1: {student1.ToString()}");
            Console.WriteLine($"Student 1 Age: {student1.GetAgeStudent()}");

            // Создание второго объекта с использованием конструктора с параметрами
            Student student2 = new Student("456 Elm St", "Science", 1001, 3, 2, 9876543210L, new DateTime(1999, 8, 25));

            Console.WriteLine($"Student 2: {student2.ToString()}");
            Console.WriteLine($"Student 2 Age: {student2.GetAgeStudent()}");

            // Создание третьего объекта с использованием конструктора с именем, фамилией и отчеством
            Student student3 = new Student("Jane", "Doe", "Johnson");
            student3.AdressGS = "789 Oak St";
            student3.FakultatGS = "Mathematics";
            student3.CourseGS = 4;
            student3.GroupGS = 1;
            student3.PhoneNumberGS = 5551234567L;
            student3.BirthdayDateGS = new DateTime(1998, 12, 5);

            Console.WriteLine($"Student 3: {student3.ToString()}");
            Console.WriteLine($"Student 3 Age: {student3.GetAgeStudent()}");

            // Сравнение объектов student1 и student3
            bool areEqual = student1.Equals(student3);
            Console.WriteLine($"Are student1 and student3 equal? {areEqual}");

            // Вывод количества созданных объектов
            Console.WriteLine($"Total instances of Student: {Student.InstanceCount()}");

            // Проверка типа объекта
            Console.WriteLine($"Type of student1: {student1.GetType()}");

            // Вызов метода GetHashCode для student1
            Console.WriteLine($"Hash code of student1: {student1.GetHashCode()}");

            Console.WriteLine("Кол во объектов: " + Student.InstanceCount());

            var anonymousStudent = new
            {
                University = "BSTU",
                Name = "John",
                Surname = "Doe",
                Dadname = "Smith",
                Adress = "123 Main St",
                Fakultat = "Engineering",
                Id = 12345, 
                Course = 2,
                Group = 3,
                PhoneNumber = 1234567890L,
                BirthdayDate = new DateTime(2000, 5, 15)
            };

            Console.WriteLine($"University: {anonymousStudent.University}");
            Console.WriteLine($"Name: {anonymousStudent.Name} {anonymousStudent.Surname} {anonymousStudent.Dadname}");
            Console.WriteLine($"Adress: {anonymousStudent.Adress}");
            Console.WriteLine($"Fakultat: {anonymousStudent.Fakultat}");
            Console.WriteLine($"ID: {anonymousStudent.Id}");
            Console.WriteLine($"Course: {anonymousStudent.Course}, Group: {anonymousStudent.Group}");
            Console.WriteLine($"Phone Number: {anonymousStudent.PhoneNumber}");
            Console.WriteLine($"Birthday Date: {anonymousStudent.BirthdayDate.ToShortDateString()}");

            Student[] students = new Student[]
       {
            new Student("123 Main St", "ФИТ", 1, 2, 9, 1234567890L, new DateTime(2000, 5, 15)),
            new Student("456 Elm St", "ФИТ", 2, 3, 9, 9876543210L, new DateTime(1999, 8, 25)),
            new Student("789 Oak St", "ФИТ", 3, 4, 9, 5551234567L, new DateTime(1998, 12, 5)),
            new Student("321 Pine St", "ХТИТ", 4, 2, 8, 5559876543L, new DateTime(2001, 3, 10)),
            new Student("654 Cedar St", "ХТИТ", 5, 1, 8, 5551239876L, new DateTime(1997, 11, 21)),
       };

            // Ввод факультета для фильтрации студентов
            Console.Write("Введите название факультета для получения списка студентов: ");
            string fakultatInput = Console.ReadLine();

            // Вывод списка студентов по заданному факультету
            Console.WriteLine($"\nСписок студентов факультета '{fakultatInput}':");
            bool foundFakultat = false;
            foreach (Student student in students)
            {
                if (student.FakultatGS.Equals(fakultatInput, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(student.ToString());
                    foundFakultat = true;
                }
            }
            if (!foundFakultat)
            {
                Console.WriteLine("Студенты на этот факультет не найдены.");
            }

            // Ввод группы для фильтрации студентов
            Console.Write("Введите номер группы для получения списка студентов: ");
            int groupInput;
            while (!int.TryParse(Console.ReadLine(), out groupInput))
            {
                Console.Write("Введите корректный номер группы: ");
            }

            // Вывод списка студентов по заданной группе
            Console.WriteLine($"\nСписок студентов группы {groupInput}:");
            bool foundGroup = false;
            foreach (Student student in students)
            {
                if (student.GroupGS == groupInput)
                {
                    Console.WriteLine(student.ToString());
                    foundGroup = true;
                }
            }
            if (!foundGroup)
            {
                Console.WriteLine("Студенты в этой группе не найдены.");
            }

            
        }
    }
}


