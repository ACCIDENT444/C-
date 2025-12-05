using System;

namespace Module2_Assignment2
{
    class Program
    {
        static void Main(string[] args)
        {
            // --- Оголошення та заповнення масиву 5x5 ---
            int[,] array = new int[5, 5];
            Random rand = new Random();
            int min = 100, max = -100;
            int minRow = 0, minCol = 0, maxRow = 0, maxCol = 0;

            Console.WriteLine("Масив 5x5:");

            // Заповнення масиву та одночасний пошук індексів min/max
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = rand.Next(-100, 101); // 101, бо верхня межа exclusive
                    Console.Write(array[i, j] + "\t");

                    // Пошук мінімального елемента та його позиції
                    if (array[i, j] < min)
                    {
                        min = array[i, j];
                        minRow = i;
                        minCol = j;
                    }
                    // Пошук максимального елемента та його позиції
                    if (array[i, j] > max)
                    {
                        max = array[i, j];
                        maxRow = i;
                        maxCol = j;
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine($"\nМінімальний елемент: {min} [({minRow},{minCol})]");
            Console.WriteLine($"Максимальний елемент: {max} [({maxRow},{maxCol})]");

            // --- Обчислення суми між min та max ---
            int sumBetween = 0;
            bool isSumming = false;

            // Проходимо всі елементи масиву в порядку рядків
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    int currentIndex = i * array.GetLength(1) + j; // Лінійний індекс
                    int minIndex = minRow * array.GetLength(1) + minCol;
                    int maxIndex = maxRow * array.GetLength(1) + maxCol;

                    // Визначаємо початкову та кінцеву точку (щоб не залежати від порядку)
                    int startIndex = Math.Min(minIndex, maxIndex);
                    int endIndex = Math.Max(minIndex, maxIndex);

                    // Якщо поточний індекс знаходиться між start та end (не включаючи самі елементи)
                    if (currentIndex > startIndex && currentIndex < endIndex)
                    {
                        sumBetween += array[i, j];
                        isSumming = true; // Флаг, що ми знайшли хоча б один елемент між
                    }
                }
            }

            // --- Виведення результату ---
            if (minIndex == maxIndex || Math.Abs(maxIndex - minIndex) == 1)
            {
                Console.WriteLine("\nМіж мінімальним та максимальним елементами немає інших елементів.");
                Console.WriteLine("Сума елементів між ними: 0");
            }
            else
            {
                Console.WriteLine($"\nСума елементів, розташованих між мінімальним та максимальним: {sumBetween}");
            }
        }
    }
}