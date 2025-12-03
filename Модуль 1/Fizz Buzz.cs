using System;

namespace Module1_Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введіть число від 1 до 100: ");
            
            // Зчитуємо введення користувача
            string input = Console.ReadLine();
            
            // Перевіряємо, чи введено число
            if (int.TryParse(input, out int number))
            {
                // Перевіряємо діапазон
                if (number >= 1 && number <= 100)
                {
                    // Перевіряємо умови Fizz Buzz
                    if (number % 3 == 0 && number % 5 == 0)
                    {
                        Console.WriteLine("Fizz Buzz");
                    }
                    else if (number % 3 == 0)
                    {
                        Console.WriteLine("Fizz");
                    }
                    else if (number % 5 == 0)
                    {
                        Console.WriteLine("Buzz");
                    }
                    else
                    {
                        Console.WriteLine(number);
                    }
                }
                else
                {
                    Console.WriteLine("Помилка: число повинно бути в діапазоні від 1 до 100.");
                }
            }
            else
            {
                Console.WriteLine("Помилка: введено некоректне число.");
            }
            
            Console.ReadKey();
        }
    }
}