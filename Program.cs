using Laba_3;
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Выберите номер задания");
        short choise = short.Parse(Console.ReadLine());
        switch (choise)
        {
            case 1:
                Console.WriteLine("Лаба 3");
                Console.WriteLine("Задание 1");
                Tasks task1 = new Tasks(1);
                task1.Task1();
                break;
            case 2:
                Console.WriteLine("Задание 2");
                Tasks task2 = new Tasks(2);
                task2.Task2();
                break;
            case 3:
                Console.WriteLine("Задание 3");
                Tasks task3 = new Tasks(3);
                task3.Task3();
                break;
            case 4:
                Console.WriteLine("Задание 4");
                Tasks task4 = new Tasks(4);
                task4.Task4();
                break;
            case 5:
                Console.WriteLine("Задание 5");
                Tasks task5 = new Tasks(5);
                task5.Task5();
                break;
            case 6:
                Console.WriteLine("Задание 6");
                Tasks tasks = new Tasks(6);
                tasks.Task6();
                break;
            case 7:
                Console.WriteLine("Задание 7");
                Tasks task7 = new Tasks(7);
                task7.Task7();
                break;
            case 8:
                Console.WriteLine("Задание 8");
                Tasks task8 = new Tasks(8);
                task8.Task8();
                break;
        }
    }
}