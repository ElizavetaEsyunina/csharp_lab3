using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Laba_3
{
    //структура для 5 задания
    [Serializable]
    public struct Toy
    {
        public string name { get; set; }
        public double price { get; set; }
        public int min_age { get; set; }
        public int max_age { get; set; }

        public Toy(string name, double price, int min_age, int max_age)
        {
            this.name = name;
            this.price = price;
            this.min_age = min_age;
            this.max_age = max_age;
        }
    }
    // задания 4 - 8
    internal class Files
    {
        //задание 4
        //статический метод для заполнения бинарного файла случайными числами
        public static void FillBinaryFile(string FilePath, int count)
        {
            Random random = new Random();
            using (BinaryWriter writer = new BinaryWriter(File.Open(FilePath, FileMode.Create)))
            {
                for (int i = 0; i < count; i++)
                {
                    int new_data = random.Next(1, 51);
                    writer.Write(new_data);
                }
            }
        }
        //статический метод для извлечения данных из бинарного файла в xml файл
        public static void Extraction(string BinFilePath, string xmlFilePath, int k)
        {
            List <int> input_data = new List <int>();
            List <int> output_data = new List <int> ();
            using (BinaryReader reader = new BinaryReader(File.Open(BinFilePath, FileMode.Open)))
            {
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    int value = reader.ReadInt32();
                    input_data.Add (value);
                    if (value % k != 0)
                        output_data.Add (value);
                }
            }
            XmlSerializer serializer = new XmlSerializer(typeof(List<int>));
            using (TextWriter writer = new StreamWriter(xmlFilePath))
            {
                serializer.Serialize(writer, output_data);
            }

            //вывод исходных данных на экран
            Console.WriteLine("Числа, записанные в бинарный файл:");
            foreach (int number in input_data)
            {
                Console.Write(number + " ");
            }
            Console.WriteLine("\n");

            //вывод выходных данных на экран
            Console.WriteLine("Числа, записанные в xml-файл:");
            foreach (int number in output_data)
            {
                Console.Write(number + " ");
            }
            Console.WriteLine();
        }

        //задание 5
        public static void SaveToysToXML(string filePath)
        {
            List<Toy> toys = new List<Toy>
        {
            new Toy("Кукла", 1500, 3, 12),
            new Toy("Мяч", 500, 2, 8),
            new Toy("Мягкая игрушка", 2000.5, 1, 9),
            new Toy("Слайм", 300, 7, 12),
            new Toy("Пластилин", 200, 6, 12),
            new Toy("Конструктор", 3000, 3, 10),
            new Toy("Пирамидка", 500, 3, 5),
            new Toy("Кубик рубика", 1000, 8, 13),
            new Toy("Машинка", 2000, 2, 12)
        };

            XmlSerializer serializer = new XmlSerializer(typeof(List<Toy>));
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(stream, toys);
            }
        }

        public static List<Toy> FindToy(string filePath, int age)
        {
            List<Toy> toysForAge = new List<Toy>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Toy>));
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                List<Toy> toys = (List<Toy>)serializer.Deserialize(stream);

                foreach (var toy in toys)
                {
                    if (toy.min_age <= age && toy.max_age >= age && toy.name != "Мяч")
                    {
                        toysForAge.Add(toy);
                    }
                }
            }
            return toysForAge;
        }

        //задание 6
        //заполнение файла случайными числами
        public static void FillTxtFile(string FilePath, int count)
        {
            Random random = new Random();
            using (StreamWriter writer = new StreamWriter(File.Open(FilePath, FileMode.Create)))
            {
                for (int i = 0; i < count; i++)
                {
                    int new_data = random.Next(1, 51);
                    writer.WriteLine(new_data);
                }
            }
        }
        //нахождение суммы макс и мин элемента
        public static int CalculateSum(string filePath)
        {
            List<int> digits = new List<int>();
            using (StreamReader reader = new StreamReader(File.Open(filePath, FileMode.Open)))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    int value = int.Parse(line);
                    digits.Add(value);
                    line = reader.ReadLine();
                }
            }
            if (digits.Count == 0)
            {
                throw new Exception("Файл не содержит чисел. Для подсчета суммы нужно хотя бы два");
            }
            if (digits.Count == 1)
            {
                throw new Exception("Файл содержит одно число. Для подсчета суммы нужно хотя бы два");
            }
            Console.WriteLine("Содержимое файла:");
            foreach (int value in digits)
            {
                Console.WriteLine(value);
            }
            Console.WriteLine("Минимальный элемент: " + digits.Min());
            Console.WriteLine("Максимальный элемент: " + digits.Max());
            return digits.Min() + digits.Max();
        }

        //задание 7
        //заполнение файла случайными числами по несколько в строке
        public static void WriteTxtFile(string filePath, int k, int count)
        {
            Random random = new Random();
            using (StreamWriter writer = new StreamWriter(File.Open(filePath, FileMode.Create)))
            {
                for (int i = 0; i < k; i++)
                {
                    int new_data = random.Next(1, 51);
                    writer.Write(new_data + " ");
                    if ((i + 1) % count == 0)
                        writer.WriteLine();
                }
            }
        }

        public static void ReadTxtFile(string filePath)
        {
            List<int> digits = new List<int>();
            using (StreamReader reader = new StreamReader(File.Open(filePath, FileMode.Open)))
            {
                string line = reader.ReadLine(); // считываем строку чисел из файла
                while (line != null)
                {
                    string[] nums = line.Split (new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); // разбиваем числа в строке
                    foreach (string num in nums)
                    {
                        int value = int.Parse(num);
                        digits.Add(value);
                    }
                    line = reader.ReadLine();
                }
            }

            Console.WriteLine("Содержимое файла:");
            foreach (int value in digits)
            {
                Console.Write(value + " ");
            }
            Console.WriteLine();
        }

        //сумма четных элементов
        public static void SumEvenNumbers(string filePath)
        {
            List<int> digits = new List<int>();
            using (StreamReader reader = new StreamReader(File.Open(filePath, FileMode.Open)))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    string[] nums = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string num in nums)
                    {
                        int value = int.Parse(num);
                        digits.Add(value);
                    }
                    line = reader.ReadLine();
                }
            }

            List<int> even_numbers = new List<int>();
            int sum = 0;
            foreach (int value in digits)
            {
                if (value % 2 == 0)
                {
                    even_numbers.Add(value);
                    sum += value;
                }
            }

            Console.WriteLine("Четные элементы:");
            foreach (int value in even_numbers)
            {
                Console.WriteLine(value);
            }

            if (even_numbers.Count == 0)
            {
                throw new Exception("В файле нет четных элементов");
            }

            Console.WriteLine("Сумма четных элементов = " + sum);
        }

        //задание 8
        public static void FillTextFile(string filePath)
        {
            string[] lines = {
            "Привет, мир!",
            "Hello, World",
            "Это текстовый файл",
            "Сегодня 13 ноября.",
            "Tomorrow is November 14th",
            "Winter is coming",
            "Боже поможи...",
            "когда там уже каникулы =(",
            "",
            "***",
            "$89",
            "2024",
            "meow"
        };

            File.WriteAllLines(filePath, lines);
        }

        public static void CreateNewFile(string input_filePath, string output_filePath)
        {
            if (!File.Exists(input_filePath))
            {
                Console.WriteLine($"Файл {input_filePath} не найден.");
            }
            string[] lines = File.ReadAllLines(input_filePath);
            string[] firstSymbols = new string[lines.Length];

            // Получаем первый символ каждой строки
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Length > 0)
                    firstSymbols[i] = lines[i][0].ToString();
                else firstSymbols[i] = ""; // Пустая строка
            }

            // Записываем первые символы в выходной файл
            File.WriteAllLines(output_filePath, firstSymbols);
        }

        public static void ReadFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Файл {filePath} не найден.");
                return;
            }

            // Читаем все строки из файла и выводим их на экран
            string[] lines = File.ReadAllLines(filePath);
            Console.WriteLine("Содержимое файла:");
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
