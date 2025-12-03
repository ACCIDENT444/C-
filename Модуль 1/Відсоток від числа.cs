using System;

namespace Module1_Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введіть два числа:");
            
            // Зчитуємо перше число
            Console.Write("1. Значення: ");
            string input1 = Console.ReadLine();
            
            // Зчитуємо друге число
            Console.Write("2. Відсоток: ");
            string input2 = Console.ReadLine();
            
            // Перевіряємо, чи обидва введення є числами
            if (double.TryParse(input1, out double value) && double.TryParse(input2, out double percent))
            {
                // Обчислюємо відсоток
                double result = value * (percent / 100);
                
                // Виводимо результат
                Console.WriteLine($"{percent}% від {value} = {result}");
            }
            else
            {
                Console.WriteLine("Помилка: введено некоректні числа.");
            }
            
            Console.ReadKey();
        }
    }
}