using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary; 
using System.Runtime.Serialization.Formatters.Soap;
using System.Text.Json;
using System.Xml.Serialization;
using System.Xml;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace SerializationExample
{
    [Serializable] 
    public class Human : ISerializable
    {
        public string Name { get; set; }

        [NonSerialized]
        [XmlIgnore]
        public int Weight;

        [NonSerialized]
        [XmlIgnore]
        public int Height;
        public int Age { get; set; }

        public Human() { }

        public Human(string name, int weight, int height, int age)
        {
            Name = name;
            Weight = weight;
            Height = height;
            Age = age;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Age", Age);
        }

        protected Human(SerializationInfo info, StreamingContext context)
        {
            Name = info.GetString("Name");
            Age = info.GetInt32("Age");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Human human = new Human("John", 80, 180, 25);

            //___________________________________
            Console.WriteLine("Бинарный файл");
            IFormatter formatter_bin = new BinaryFormatter(); //Создание объекта

            //Сериализуем
            using (FileStream fs = new FileStream("C:\\Users\\user\\source\\repos\\OOP_3sem_laba13\\OOP_3sem_laba13\\bunserial.bin", FileMode.OpenOrCreate))
            {
                formatter_bin.Serialize(fs, human);

                Console.WriteLine("Объект сериализован");
            }

            //Десириализация
            using (FileStream fs = new FileStream("C:\\Users\\user\\source\\repos\\OOP_3sem_laba13\\OOP_3sem_laba13\\bunserial.bin", FileMode.OpenOrCreate, FileAccess.Read))
            {
                Human newPerson = (Human)formatter_bin.Deserialize(fs);

                Console.WriteLine("Объект десериализован");
                Console.WriteLine($"Имя: {newPerson.Name} --- Возраст: {newPerson.Age}");
            }

            //___________________________________
            Console.WriteLine("SOAP");
            IFormatter formatter_soap = new SoapFormatter(); //Создание объекта

            //Сериализуем
            using (FileStream fs = new FileStream("C:\\Users\\user\\source\\repos\\OOP_3sem_laba13\\OOP_3sem_laba13\\soapserialize.soap", FileMode.OpenOrCreate))
            {
                formatter_soap.Serialize(fs, human);

                Console.WriteLine("Объект сериализован");
            }

            //Десириализация
            using (FileStream fs = new FileStream("C:\\Users\\user\\source\\repos\\OOP_3sem_laba13\\OOP_3sem_laba13\\soapserialize.soap ", FileMode.OpenOrCreate, FileAccess.Read))
            {
                Human newPerson = (Human)formatter_soap.Deserialize(fs);

                Console.WriteLine("Объект десериализован");
                Console.WriteLine($"Имя: {newPerson.Name} --- Возраст: {newPerson.Age}");
            }

            //___________________________________
            Console.WriteLine("JSON:");

            string jsonString = JsonSerializer.Serialize(human);
            File.WriteAllText("C:\\Users\\user\\source\\repos\\OOP_3sem_laba13\\OOP_3sem_laba13\\jsonserialize.json", jsonString);
            Console.WriteLine("Объект сериализован");

            string jsonFromFile = File.ReadAllText("C:\\Users\\user\\source\\repos\\OOP_3sem_laba13\\OOP_3sem_laba13\\jsonserialize.json");
            var newPerson_json = JsonSerializer.Deserialize<Human>(jsonFromFile);
            Console.WriteLine("Объект десериализован");
            Console.WriteLine($"Имя: {newPerson_json.Name} --- Возраст: {newPerson_json.Age}");

            //___________________________________
            Console.WriteLine("XML");

            XmlSerializer formatter_xml = new XmlSerializer(typeof(Human)); //Создание объекта

            //Сериализуем
            using (FileStream fs = new FileStream("C:\\Users\\user\\source\\repos\\OOP_3sem_laba13\\OOP_3sem_laba13\\xmlserialize.xml", FileMode.OpenOrCreate))
            {
                formatter_xml.Serialize(fs, human);

                Console.WriteLine("Объект сериализован");
            }

            //Десириализация
            using (FileStream fs = new FileStream("C:\\Users\\user\\source\\repos\\OOP_3sem_laba13\\OOP_3sem_laba13\\xmlserialize.xml", FileMode.OpenOrCreate))
            {
                Human newPerson = (Human)formatter_xml.Deserialize(fs);

                Console.WriteLine("Объект десериализован");
                Console.WriteLine($"Имя: {newPerson.Name} --- Возраст: {newPerson.Age}");
            }

            Console.WriteLine("Задание с List: ");

            List<Human> SerList = new List<Human>();

            SerList.Add(new Human("Alice", 55, 165, 30));
            SerList.Add(new Human("Bob", 75, 180, 25));
            SerList.Add(new Human("Charlie", 68, 175, 40));
            SerList.Add(new Human("Diana", 60, 160, 35));
            SerList.Add(new Human("Eve", 50, 155, 28));

            string jsonString_list = JsonSerializer.Serialize(SerList);
            File.WriteAllText("C:\\Users\\user\\source\\repos\\OOP_3sem_laba13\\OOP_3sem_laba13\\json_list_serialize.json", jsonString_list);
            Console.WriteLine("Объект сериализован");

            var newList_json = JsonSerializer.Deserialize<List<Human>>(jsonString_list);
            Console.WriteLine("Объект десериализован");

            foreach (var item in newList_json)
            {
                Console.WriteLine($"Имя: {item.Name}, Возраст: {item.Age}");
            }

            Console.WriteLine("XPath:");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("C:\\Users\\user\\source\\repos\\OOP_3sem_laba13\\OOP_3sem_laba13\\xmlserialize.xml");

            XmlNode nameNode = xmlDoc.SelectSingleNode("/Human/Name");
            if (nameNode != null)
            {
                Console.WriteLine($"Имя: {nameNode.InnerText}");
            }

            XmlNode ageNode = xmlDoc.SelectSingleNode("/Human/Age[text() > 20]");
            if (ageNode != null)
            {
                Console.WriteLine($"Возраст (больше 20): {ageNode.InnerText}");
            }

            Console.WriteLine("LINQ to JSON:");

            JArray person_list = JArray.Parse(jsonString_list);
            JObject person = JObject.Parse(jsonString);

            string name_person = (string)person["Name"];
            int age_person = (int)person["Age"];

            Console.WriteLine($"Имя: {name_person}, Возраст: {age_person}");

            var person_list_age = person_list
                .Where(p => (int)p["Age"] > 34)
                .Select(p => new
                {
                    Name = (string)p["Name"],
                    Age = (int)p["Age"]
                });

            Console.WriteLine("Люди старше 34 лет:");
            foreach (var item in person_list_age)
            {
                Console.WriteLine($"Имя: {item.Name}, Возраст: {item.Age}");
            }


        }
    }
}
