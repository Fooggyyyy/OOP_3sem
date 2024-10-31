using System;
using System.Reflection; 
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OOP_3sem_laba11
{
    static class Reflector
    {
        static public int first = 1;
        static public int second = 2;

        static public void ClearFile(string filePath = "C:\\Users\\user\\source\\repos\\OOP_3sem_laba11\\OOP_3sem_laba11\\Info.txt")
        {
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                writer.Write(""); 
            }
        }

        static void WriteToFile(string content, string filePath = "C:\\Users\\user\\source\\repos\\OOP_3sem_laba11\\OOP_3sem_laba11\\Info.txt")
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(content);
            }
        }

        static public void GetAssemblyInfo()
        {
            WriteToFile("Имя сборки: " + Assembly.GetCallingAssembly().GetName().Name);
        }

        static public void HasStaticConstructor(Type classType)
        {
            ConstructorInfo[] constructors = classType.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            bool hasStaticConstructor = constructors.Any(c => c.IsStatic);
            WriteToFile($"Есть ли статические конструкторы в классе {classType.Name}: {(hasStaticConstructor ? "Да" : "Нет")}");
        }

        static public void AllMethod_Field(Type classType)
        {
            MethodInfo[] methods = classType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            FieldInfo[] fields = classType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            foreach (MethodInfo method in methods)
            {
                WriteToFile("Метод: " + method.Name);
            }

            foreach (FieldInfo field in fields)
            {
                WriteToFile("Поле: " + field.Name);
            }
        }

        static public void InterfaceCount(Type classType)
        {
            var interfaces = classType.GetInterfaces();
            WriteToFile($"Класс {classType.Name} реализует {interfaces.Length} интерфейса(ов).");
        }

        public static object Invoke(object obj, string methodName, params object[] parameters)
        {
            Type type = obj.GetType();
            MethodInfo method = type.GetMethod(methodName);

            if (method == null)
            {
                throw new ArgumentException($"Метод {methodName} не найден в классе {type.Name}.");
            }

            return method.Invoke(obj, parameters);
        }

        public static T Create<T>()
        {
            return Activator.CreateInstance<T>();
        }

        public static object Create(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Reflector.ClearFile();

            Reflector.GetAssemblyInfo();
            Reflector.HasStaticConstructor(typeof(Reflector));
            Reflector.AllMethod_Field(typeof(Reflector));
            Reflector.InterfaceCount(typeof(Reflector));

            var myClassInstance = Reflector.Create<MyClass>(); 
            Console.WriteLine($"Создан экземпляр класса: {myClassInstance.GetType().Name}");

            string methodName = "MethodName"; 
            object[] parameters = new object[] {  };
            try
            {
                Reflector.Invoke(myClassInstance, methodName, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при вызове метода: {ex.Message}");
            }

            Console.WriteLine("Все данные записаны в файл!!!");
        }
    }

    public class MyClass 
    {
        public void MethodName() 
        {
            Console.WriteLine("Метод вызван!");
        }
    }
}
