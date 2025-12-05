using System;

namespace Module2_Assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            // --- Оголошення масивів ---
            int[] A = new int[5];
            double[,] B = new double[3, 4];
            Random rand = new Random();

            // --- Заповнення масиву A ---
            Console.WriteLine("Введіть 5 цілих чисел для масиву A:");
            for (int i = 0; i < A.Length; i++)
            {
                Console.Write($"A[{i}] = ");
                A[i] = Convert.ToInt32(Console.ReadLine());
            }

            // --- Заповнення масиву B випадковими числами ---
            for (int i = 0; i < B.GetLength(0); i++) // рядки
            {
                for (int j = 0; j < B.GetLength(1); j++) // стовпці
                {
                    // Генерація дробового числа від 0 до 10, округленого до 2 знаків
                    B[i, j] = Math.Round(rand.NextDouble() * 10, 2);
                }
            }

            // --- Виведення масиву A ---
            Console.WriteLine("\nМасив A:");
            foreach (int element in A)
            {
                Console.Write(element + "\t");
            }

            // --- Виведення масиву B ---
            Console.WriteLine("\n\nМасив B:");
            for (int i = 0; i < B.GetLength(0); i++)
            {
                for (int j = 0; j < B.GetLength(1); j++)
                {
                    Console.Write(B[i, j] + "\t");
                }
                Console.WriteLine();
            }

            // --- Пошук загальних характеристик (для конвертації в double) ---
            double maxElement = Convert.ToDouble(A[0]);
            double minElement = Convert.ToDouble(A[0]);
            double totalSum = 0;
            double totalProduct = 1; // Початкове значення для добутку
            double sumEvenA = 0;
            double sumOddColumnsB = 0;

            // Обробка масиву A для загальних характеристик
            for (int i = 0; i < A.Length; i++)
            {
                double currentA = Convert.ToDouble(A[i]);
                // Максимальний
                if (currentA > maxElement) maxElement = currentA;
                // Мінімальний
                if (currentA < minElement) minElement = currentA;
                // Загальна сума
                totalSum += currentA;
                // Загальний добуток
                totalProduct *= currentA;
                // Сума парних елементів A
                if (A[i] % 2 == 0) sumEvenA += currentA;
            }

            // Обробка масиву B для загальних характеристик
            for (int i = 0; i < B.GetLength(0); i++)
            {
                for (int j = 0; j < B.GetLength(1); j++)
                {
                    double currentB = B[i, j];
                    // Максимальний
                    if (currentB > maxElement) maxElement = currentB;
                    // Мінімальний
                    if (currentB < minElement) minElement = currentB;
                    // Загальна сума
                    totalSum += currentB;
                    // Загальний добуток
                    totalProduct *= currentB;
                    // Сума непарних стовпців (індексація з 0, тому стовпці 1, 3 -> індекси 0, 2)
                    if (j % 2 == 0) // j = 0 -> перший стовпець (непарний)
                    {
                        sumOddColumnsB += currentB;
                    }
                }
            }

            // --- Виведення результатів ---
            Console.WriteLine("\nРезультати обчислень:");
            Console.WriteLine($"Загальний максимальний елемент: {maxElement}");
            Console.WriteLine($"Загальний мінімальний елемент: {minElement}");
            Console.WriteLine($"Загальна сума всіх елементів: {totalSum:F2}");
            Console.WriteLine($"Загальний добуток усіх елементів: {totalProduct:E2}"); // E2 для великих/малих чисел
            Console.WriteLine($"Сума парних елементів масиву A: {sumEvenA}");
            Console.WriteLine($"Сума елементів непарних стовпців масиву B: {sumOddColumnsB:F2}");
        }
    }
}