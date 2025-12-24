using System;

namespace Module8
{
    // Делегат для фільтрації масиву чисел
    public delegate bool NumberFilter(int number);
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Завдання 1: Фільтрація масиву ===");
            Task1Demo();
            
            Console.WriteLine("\n=== Завдання 2: Різноманітні методи ===");
            Task2Demo();
        }
        
        static void Task1Demo()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            Console.WriteLine("Початковий масив: " + string.Join(", ", numbers));
            
            // Створення делегатів
            NumberFilter evenFilter = IsEven;
            NumberFilter oddFilter = IsOdd;
            NumberFilter primeFilter = IsPrime;
            NumberFilter fibonacciFilter = IsFibonacci;
            
            // Фільтрація масиву
            Console.WriteLine("\nПарні числа: " + string.Join(", ", FilterArray(numbers, evenFilter)));
            Console.WriteLine("Непарні числа: " + string.Join(", ", FilterArray(numbers, oddFilter)));
            Console.WriteLine("Прості числа: " + string.Join(", ", FilterArray(numbers, primeFilter)));
            Console.WriteLine("Числа Фібоначчі: " + string.Join(", ", FilterArray(numbers, fibonacciFilter)));
        }
        
        // Метод фільтрації масиву
        static int[] FilterArray(int[] array, NumberFilter filter)
        {
            return Array.FindAll(array, n => filter(n));
        }
        
        // Метод перевірки на парність
        static bool IsEven(int number)
        {
            return number % 2 == 0;
        }
        
        // Метод перевірки на непарність
        static bool IsOdd(int number)
        {
            return number % 2 != 0;
        }
        
        // Метод перевірки на простоту
        static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;
            
            for (int i = 3; i * i <= number; i += 2)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
        
        // Метод перевірки на число Фібоначчі
        static bool IsFibonacci(int number)
        {
            if (number < 0) return false;
            
            // Перевірка, чи є число повним квадратом
            bool IsPerfectSquare(int x)
            {
                int s = (int)Math.Sqrt(x);
                return s * s == x;
            }
            
            // Число є числом Фібоначчі, якщо 5*n*n + 4 або 5*n*n - 4 є повним квадратом
            return IsPerfectSquare(5 * number * number + 4) || 
                   IsPerfectSquare(5 * number * number - 4);
        }
        
        static void Task2Demo()
        {
            // Використання Action для методів без повернення значення
            Action showCurrentTime = () => Console.WriteLine($"Поточний час: {DateTime.Now:HH:mm:ss}");
            Action showCurrentDate = () => Console.WriteLine($"Поточна дата: {DateTime.Now:dd.MM.yyyy}");
            Action showCurrentDayOfWeek = () => Console.WriteLine($"Поточний день тижня: {DateTime.Now.DayOfWeek}");
            
            // Виклик методів
            showCurrentTime();
            showCurrentDate();
            showCurrentDayOfWeek();
            
            // Використання Predicate для перевірки умов
            Predicate<double> isPositive = x => x > 0;
            Console.WriteLine($"\nЧи є 5.5 додатним числом? {isPositive(5.5)}");
            Console.WriteLine($"Чи є -2.3 додатним числом? {isPositive(-2.3)}");
            
            // Використання Func для методів з поверненням значення
            Func<double, double, double, double> triangleArea = (a, b, c) =>
            {
                // Формула Герона
                double p = (a + b + c) / 2;
                return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
            };
            
            Func<double, double, double> rectangleArea = (width, height) => width * height;
            
            // Розрахунок площ
            Console.WriteLine($"\nПлоща трикутника зі сторонами 3, 4, 5: {triangleArea(3, 4, 5):F2}");
            Console.WriteLine($"Площа прямокутника зі сторонами 4 і 6: {rectangleArea(4, 6):F2}");
            
            // Додатковий приклад з Predicate для перевірки трикутника
            Predicate<(double, double, double)> isValidTriangle = sides =>
            {
                var (a, b, c) = sides;
                return a + b > c && a + c > b && b + c > a;
            };
            
            var triangleSides = (3.0, 4.0, 5.0);
            Console.WriteLine($"\nЧи є трикутник зі сторонами {triangleSides} правильним? {isValidTriangle(triangleSides)}");
            
            // Комбінування делегатів
            Console.WriteLine("\n--- Комбінований виклик через Action ---");
            Action combinedActions = showCurrentTime;
            combinedActions += showCurrentDate;
            combinedActions += showCurrentDayOfWeek;
            combinedActions();
        }
    }
}