using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Laba_3
{
    internal class Matrix
    {
        private int[,] arr;

        //конструктор по умолчанию
        public Matrix(int n, int m, int null_matrix)
        {
            arr = new int[n, m];
        }

        //конструктор для заполнения массива данными с клавиатуры
        public Matrix(int n, int m)
        {
            arr = new int[n, m];
            FillArray();
        }

        //конструктор для заполнения массива случйными числами (шахматы)
        public Matrix(int n)
        {
            arr = new int[n, n];
            RandomMatrix();
        }

        //конструктор для заполнения матрицы случайными числами (справа треугольник из нулей)
        public Matrix(int n, bool Left_Triangle)
        {
            arr = new int[n, n];
            LeftTriangle();
        }

        //конструктор для заполения массива случайными числами
        public Matrix(int n, int m, bool random_matrix)
        {
            arr = new int[n, m];
            GetRandomData();
        }


        //задание 1
        //заполнение матрицы данными с клавиатуры
        public void FillArray()
        {
            Console.WriteLine("Введите элементы массива");
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write($"Элемент [{i},{j}]: ");
                    if (int.TryParse(Console.ReadLine(), out int input_data))
                        arr[i, j] = input_data;
                    else Console.WriteLine("Некорректный формат входных данных. Данная позиция инициализируется нулем");
                }
            }
        }

        //заполнение матрицы случайными числами (шахматная доска)
        public void RandomMatrix()
        {
            Random rnd = new Random();
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    //определяем на какой "клетке" находимся
                    //четная - черная => заполняем четными числами
                    if ((i + j) % 2 == 0)
                        arr[i, j] = rnd.Next(-100, 101) * 2;
                    //нечетная - белая => заполняем нечетными числами
                    else
                        arr[i, j] = rnd.Next(-100, 101) * 2 + 1;
                }
            }
        }

        //заполнение матрицы случайными числами (справа треугольник из нулей)
        public void LeftTriangle()
        {
            Random rnd = new Random();
            int n = arr.GetLength(0); // размерность матрицы
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    arr[i, j] = rnd.Next(-100, 101); // заполнение случайными числами
                }
                for (int j = i + 1; j < n; j++)
                {
                    arr[i, j] = 0; // заполнение нулями
                }
            }
        }

        //заполнение массива случайными числами
        public void GetRandomData()
        {
            Random rnd = new Random();
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    arr[i, j] = rnd.Next(-50, 51);
                }
            }
        }

        //задание 2
        public List<int> Count_Summa()
        {
            //создаем список для сохранения получившихся сумм
            List<int> res = new List<int>(arr.GetLength(0) - 1);

            for (int col = 0; col < arr.GetLength(1) - 1; col++) // Проходим по всем столбцам, кроме последнего
            {
                int sum = 0;
                // Суммируем элементы столбца, исключая последние col элементов
                for (int row = 0; row < arr.GetLength(0) - (col + 1); row++)
                    sum += arr[row, col];
                //добавляем полученную сумму в список
                res.Add(sum);
            }
            foreach (int summa in res)
                Console.Write(summa + " ");
            Console.WriteLine();
            return res;
        }

        //задание 3
        //(А+4*В)-Ст
        public static Matrix operator *(Matrix B, int digit)
        {
            Matrix newB = new Matrix(B.arr.GetLength(0), B.arr.GetLength(1), 1);
            for (int i = 0; i < B.arr.GetLength(0); i++)
            {
                for (int j = 0; j < B.arr.GetLength(1); j++)
                {
                    newB.arr[i, j] = B.arr[i, j] * digit;
                }
            }
            return newB;
        }
        public static Matrix operator +(Matrix A, Matrix B)
        {
            if (A.arr.GetLength(0) == B.arr.GetLength(0) && A.arr.GetLength(1) == B.arr.GetLength(1))
            {
                Matrix A_plus_B = new Matrix(A.arr.GetLength(0), A.arr.GetLength(1), 1);

                for (int i = 0; i < A.arr.GetLength(0); i++)
                {
                    for (int j = 0; j < A.arr.GetLength(1); j++)
                    {
                        A_plus_B.arr[i, j] = A.arr[i, j] + B.arr[i, j];
                    }
                }

                return A_plus_B;
            }
            else
                throw new Exception("Невозможно сложить матрицы разного размера");
        }
        public static Matrix operator -(Matrix A, Matrix B)
        {
            if (A.arr.GetLength(0) == B.arr.GetLength(0) && A.arr.GetLength(1) == B.arr.GetLength(1))
            {
                Matrix A_minus_B = new Matrix(A.arr.GetLength(0), A.arr.GetLength(1), 1);

                for (int i = 0; i < A.arr.GetLength(0); i++)
                {
                    for (int j = 0; j < A.arr.GetLength(1); j++)
                    {
                        A_minus_B.arr[i, j] = A.arr[i, j] - B.arr[i, j];
                    }
                }

                return A_minus_B;
            }
            else
                throw new Exception("Невозможно вычесть матрицы разного размера");
        }
         //метод для транспонирования матрицы
         public int[,] Transposition()
        {
            Matrix trans_matrix = new Matrix(arr.GetLength(1), arr.GetLength(0), 1);
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    trans_matrix.arr[j, i] = arr[i, j];
                }
            }
            this.arr = trans_matrix.arr;
            return arr;
        }

        //перегрузка ToString()
        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    result += arr[i, j].ToString().PadLeft(6);
                }
                result += "\n";
            }
            return result;
        }

        //вывод матрицы на экран
        public void Print()
        {
            Console.WriteLine("Содержимое массива:");
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write($"{arr[i, j],7} ");
                }
                Console.WriteLine();
            }
        }
    }
}
