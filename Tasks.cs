using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Laba_3.Files;

namespace Laba_3
{
    internal class Tasks
    {
        private int task_num;

        public Tasks(int task_num)
        {
            this.task_num = task_num;
        }

        //задание 1
        public void Task1()
        {
            Console.WriteLine("Выберите что тестировать:\n" +
                              "1 - заполнение матрицы с клавиатуры\n" +
                              "2 - заполнение матрицы рандомными числами (шахматная доска)\n" +
                              "3 - заполнение матрицы рандомными числами (справа треугольник из нулей)");
            bool continue_running = true;
            while (continue_running)
            {
                Console.WriteLine("\nВыберите вариант 1-3, или нажмите 0, чтобы завершить работу программы");
                int num = int.Parse(Console.ReadLine());
                switch (num)
                {
                    case 1:
                        Console.WriteLine("Заполнение матрицы размерностью n*m данными с клавиатуры");
                        Console.WriteLine("Введите количество строк и столбцов в матрице");
                        if (int.TryParse(Console.ReadLine(), out int n) && int.TryParse(Console.ReadLine(), out int m))
                        {
                            Matrix arr_WithUserData = new Matrix(Math.Abs(n), Math.Abs(m));
                            arr_WithUserData.Print();
                        }
                        else Console.WriteLine("Некорректный формат входных данных");
                        break;
                    case 2:
                        Console.WriteLine("Заполнение матрицы размерностью n*n случайными числами");
                        Console.WriteLine("Введите размерность матрицы");
                        if (int.TryParse(Console.ReadLine(), out int n2))
                        {
                            Matrix random_matrix = new Matrix(Math.Abs(n2));
                            random_matrix.Print();
                        }
                        else Console.WriteLine("Некорректный формат входных данных");
                        break;
                    case 3:
                        Console.WriteLine("Заполнение матрицы размерностью n*n случайными числами (справа треугольник из нулей)");
                        Console.WriteLine("Введите размерность матрицы");
                        if (int.TryParse(Console.ReadLine(), out int n3))
                        {
                            Matrix left_triangle_matrix = new Matrix(Math.Abs(n3), true);
                            left_triangle_matrix.Print();
                        }
                        else Console.WriteLine("Некорректный формат входных данных");
                        break;
                    case 0:
                        continue_running = false;
                        break;
                    default: Console.WriteLine("Неверный выбор номера, попробуйте еще раз.");
                        break;
                }
            }
            Console.WriteLine("Работа программы завершена");
        }

        //задание 2
        public void Task2()
        {
            Console.WriteLine("Введите количество строк и столбцов в матрице");
            if (int.TryParse(Console.ReadLine(), out int n) && int.TryParse(Console.ReadLine(), out int m))
            {
                Matrix t2_matrix = new Matrix(Math.Abs(n), Math.Abs(m), true);
                t2_matrix.Print();
                Console.WriteLine("Список полученных сумм:");
                List<int> res = t2_matrix.Count_Summa();
                Console.WriteLine("Максимальная сумма: " + res.Max());
            }
        }

        //задание 3
        public void Task3()
        {
            Console.WriteLine("Подсчет значения матричного выражения (А+4*В)-Ст ");
            Console.WriteLine("Введите количество строк и столбцов в матрицах A И B");
            int n = int.Parse(Console.ReadLine());
            int m = int.Parse(Console.ReadLine());
            Matrix A = new Matrix(Math.Abs(n), Math.Abs(m), true);
            Matrix B = new Matrix(Math.Abs(n), Math.Abs(m), true);
            Console.WriteLine("Исходные матрицы A и B имеют следующее наполнение:");
            A.Print();
            B.Print();
            Console.WriteLine("Матрица B * 4 = \n" + (B * 4));
            Console.WriteLine("Введите размерность матрицы C");
            int n_C = int.Parse(Console.ReadLine());
            int m_C = int.Parse(Console.ReadLine());
            Matrix C = new Matrix(Math.Abs(n_C), Math.Abs(m_C), true);
            Console.WriteLine("Исходная матрица C имеет следующее наполнение:");
            C.Print();
            C.Transposition();
            Console.WriteLine("Транспонированная матрица C:");
            C.Print();
            Console.WriteLine();
            try
            {
                Matrix AplusB = (A + (B * 4));
                Matrix res = AplusB - C;
                Console.WriteLine("Значение матричного выражения:\n" + res);
            }
            catch (Exception error)
            {
                Console.WriteLine (error.Message);
            }
        }

        //задание 4
        public void Task4()
        {
            string binaryFilePath = "Task4.bin";
            string xmlFilePath = "output.xml";
            Console.WriteLine("Введите количество чисел в файле и целочисленное значение k");
            if (int.TryParse(Console.ReadLine(), out int nums) && int.TryParse(Console.ReadLine(), out int k))
            {
                Files.FillBinaryFile(binaryFilePath, Math.Abs(nums));
                Files.Extraction(binaryFilePath, xmlFilePath, k);
                Console.WriteLine("Данные успешно записаны в xml-файл");
            }
            else Console.WriteLine("Некорректные входные данные");
        }

        //задание 5
        public void Task5()
        {
            string xmlFilePath = "toys.xml";
            Files.SaveToysToXML(xmlFilePath);
            Console.WriteLine("Данные успешно записаны в xml-файл");

            List<Toy> ToysFor3 = Files.FindToy(xmlFilePath, 3);

            // Выводим подходящие игрушки 
            if (ToysFor3.Count > 0)
            {
                Console.WriteLine("Подходящие игрушки для ребенка трех лет (кроме мяча):");
                foreach (var toy in ToysFor3)
                {
                    Console.WriteLine($"Название: {toy.name}\nЦена: {toy.price}\nВозраст: от {toy.min_age} до {toy.max_age} лет");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Нет подходящих игрушек для ребенка трех лет.");
            }
        }

        //задание 6
        public void Task6()
        {
            string filePath = "Task6.txt";
            Console.WriteLine("Сколькими числами заполнить файл?");
            if (int.TryParse(Console.ReadLine(), out int count))
            {
                try
                {
                    Files.FillTxtFile(filePath, Math.Abs(count));
                    Console.WriteLine("Данные успешно записаны в txt файл");
                    Console.WriteLine("Сумма макс и мин элементов = " + Files.CalculateSum(filePath));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else Console.WriteLine("Некорректный формат входных данных");
        }

        //задание 7
        public void Task7()
        {
            string FilePath = "Task7.txt";
            Console.WriteLine("Сколькими числами заполнить файл?");
            if (int.TryParse(Console.ReadLine(), out int k))
            {
                Console.WriteLine("Сколько чисел должно быть записано в каждой строке файла?");
                if (int.TryParse(Console.ReadLine(), out int count))
                {
                    try
                    {
                        Files.WriteTxtFile(FilePath, Math.Abs(k), Math.Abs(count));
                        Console.WriteLine("Данные успешно записаны в файл");
                        Files.ReadTxtFile(FilePath);
                        Files.SumEvenNumbers(FilePath);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            else Console.WriteLine("Некорректный формат входных данных");
        }

        //задание 8
        public void Task8()
        {
            string input_file = "Task8.txt";
            string output_file = "output_Task8.txt";
            Files.FillTextFile(input_file);
            Files.ReadFile(input_file);
            Files.CreateNewFile(input_file, output_file);
            Console.WriteLine();
            Files.ReadFile(output_file);
        }
    }
}
