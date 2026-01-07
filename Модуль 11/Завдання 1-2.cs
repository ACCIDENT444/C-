using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Module11
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Модуль 11: Взаємодія з файловою системою ===\n");
            
            while (true)
            {
                Console.WriteLine("Оберіть завдання:");
                Console.WriteLine("1. Генерація чисел та збереження у файли");
                Console.WriteLine("2. Пошук та заміна слів у файлі");
                Console.WriteLine("3. Вийти");
                Console.Write("\nВаш вибір: ");
                
                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        Task1();
                        break;
                    case "2":
                        Task2();
                        break;
                    case "3":
                        Console.WriteLine("\nДякую за використання програми!");
                        return;
                    default:
                        Console.WriteLine("\nНевірний вибір. Спробуйте ще раз.");
                        break;
                }
                
                Console.WriteLine("\nНатисніть будь-яку клавішу для продовження...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        
        static void Task1()
        {
            Console.Clear();
            Console.WriteLine("=== Завдання 1: Генерація та збереження чисел ===\n");
            
            Stopwatch stopwatch = Stopwatch.StartNew();
            Statistics stats = new Statistics();
            
            try
            {
                // Генерація 100 випадкових чисел
                Console.WriteLine("Генерація 100 випадкових чисел...");
                Random random = new Random();
                List<int> numbers = new List<int>();
                
                for (int i = 0; i < 100; i++)
                {
                    numbers.Add(random.Next(1, 1000));
                }
                
                stats.TotalNumbersGenerated = numbers.Count;
                Console.WriteLine($"Згенеровано {numbers.Count} чисел.");
                
                // Визначення шляхів до файлів
                string primesFilePath = "prime_numbers.txt";
                string fibonacciFilePath = "fibonacci_numbers.txt";
                string allNumbersFilePath = "all_numbers.txt";
                
                // Створення списків для чисел
                List<int> primeNumbers = new List<int>();
                List<int> fibonacciNumbers = new List<int>();
                
                // Перевірка кожного числа
                Console.WriteLine("\nАналіз чисел...");
                foreach (int number in numbers)
                {
                    if (IsPrime(number))
                    {
                        primeNumbers.Add(number);
                        stats.PrimeNumbersCount++;
                    }
                    
                    if (IsFibonacci(number))
                    {
                        fibonacciNumbers.Add(number);
                        stats.FibonacciNumbersCount++;
                    }
                }
                
                // Збереження всіх чисел у файл
                Console.WriteLine("Збереження всіх чисел у файл...");
                SaveNumbersToFile(allNumbersFilePath, numbers, "Всі згенеровані числа");
                stats.AllNumbersFileSize = new FileInfo(allNumbersFilePath).Length;
                
                // Збереження простих чисел у файл
                Console.WriteLine("Збереження простих чисел у файл...");
                SaveNumbersToFile(primesFilePath, primeNumbers, "Прості числа");
                stats.PrimeNumbersFileSize = new FileInfo(primesFilePath).Length;
                
                // Збереження чисел Фібоначчі у файл
                Console.WriteLine("Збереження чисел Фібоначчі у файл...");
                SaveNumbersToFile(fibonacciFilePath, fibonacciNumbers, "Числа Фібоначчі");
                stats.FibonacciNumbersFileSize = new FileInfo(fibonacciFilePath).Length;
                
                stopwatch.Stop();
                stats.ExecutionTime = stopwatch.Elapsed;
                
                // Відображення статистики
                Console.WriteLine("\n" + new string('=', 50));
                Console.WriteLine("СТАТИСТИКА РОБОТИ ДОДАТКА");
                Console.WriteLine(new string('=', 50));
                
                DisplayStatistics(stats);
                
                // Відображення згенерованих чисел
                Console.WriteLine("\n" + new string('-', 50));
                Console.WriteLine("ДОДАТКОВА ІНФОРМАЦІЯ");
                Console.WriteLine(new string('-', 50));
                
                Console.WriteLine($"\nУсі згенеровані числа ({numbers.Count}):");
                Console.WriteLine(string.Join(", ", numbers));
                
                Console.WriteLine($"\nПрості числа ({primeNumbers.Count}):");
                if (primeNumbers.Count > 0)
                    Console.WriteLine(string.Join(", ", primeNumbers));
                else
                    Console.WriteLine("Прості числа не знайдені");
                
                Console.WriteLine($"\nЧисла Фібоначчі ({fibonacciNumbers.Count}):");
                if (fibonacciNumbers.Count > 0)
                    Console.WriteLine(string.Join(", ", fibonacciNumbers));
                else
                    Console.WriteLine("Числа Фібоначчі не знайдені");
                
                // Перетин множин (числа, які є і простими, і числами Фібоначчі)
                List<int> intersection = new List<int>();
                foreach (int num in primeNumbers)
                {
                    if (fibonacciNumbers.Contains(num))
                    {
                        intersection.Add(num);
                    }
                }
                
                if (intersection.Count > 0)
                {
                    Console.WriteLine($"\nЧисла, які є і простими, і числами Фібоначчі ({intersection.Count}):");
                    Console.WriteLine(string.Join(", ", intersection));
                }
                
                // Інформація про файли
                Console.WriteLine("\n" + new string('-', 50));
                Console.WriteLine("ІНФОРМАЦІЯ ПРО ФАЙЛИ");
                Console.WriteLine(new string('-', 50));
                
                DisplayFileInfo(allNumbersFilePath, "Усі числа");
                DisplayFileInfo(primesFilePath, "Прості числа");
                DisplayFileInfo(fibonacciFilePath, "Числа Фібоначчі");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nПОМИЛКА: {ex.Message}");
                Console.WriteLine($"Деталі: {ex.StackTrace}");
            }
        }
        
        static void Task2()
        {
            Console.Clear();
            Console.WriteLine("=== Завдання 2: Пошук та заміна слів у файлі ===\n");
            
            Stopwatch stopwatch = Stopwatch.StartNew();
            SearchReplaceStats stats = new SearchReplaceStats();
            
            try
            {
                // Введення шляху до файлу
                Console.Write("Введіть шлях до файлу (або натисніть Enter для використання тестового файлу): ");
                string filePath = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(filePath))
                {
                    // Створення тестового файлу
                    filePath = "test_text.txt";
                    CreateTestFile(filePath);
                    Console.WriteLine($"\nСтворено тестовий файл: {filePath}");
                }
                
                // Перевірка існування файлу
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"\nПОМИЛКА: Файл '{filePath}' не знайдений.");
                    return;
                }
                
                stats.FilePath = filePath;
                stats.OriginalFileSize = new FileInfo(filePath).Length;
                
                // Введення слова для пошуку
                Console.Write("\nВведіть слово для пошуку: ");
                string searchWord = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(searchWord))
                {
                    Console.WriteLine("Слово для пошуку не може бути порожнім.");
                    return;
                }
                
                // Введення слова для заміни
                Console.Write("Введіть слово для заміни: ");
                string replaceWord = Console.ReadLine();
                
                if (replaceWord == null)
                {
                    replaceWord = "";
                }
                
                // Зчитування вмісту файлу
                Console.WriteLine("\nЗчитування файлу...");
                string originalContent;
                
                using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
                {
                    originalContent = reader.ReadToEnd();
                }
                
                stats.TotalCharacters = originalContent.Length;
                stats.TotalLines = originalContent.Split('\n').Length;
                
                // Пошук та заміна
                Console.WriteLine("Виконання пошуку та заміни...");
                
                int occurrences = 0;
                int currentIndex = 0;
                StringBuilder resultContent = new StringBuilder();
                
                while (currentIndex < originalContent.Length)
                {
                    int foundIndex = originalContent.IndexOf(searchWord, currentIndex, StringComparison.OrdinalIgnoreCase);
                    
                    if (foundIndex == -1)
                    {
                        // Додаємо решту тексту
                        resultContent.Append(originalContent.Substring(currentIndex));
                        break;
                    }
                    
                    // Додаємо текст до знайденого слова
                    resultContent.Append(originalContent.Substring(currentIndex, foundIndex - currentIndex));
                    
                    // Додаємо слово для заміни
                    resultContent.Append(replaceWord);
                    
                    occurrences++;
                    currentIndex = foundIndex + searchWord.Length;
                }
                
                stats.ReplacementsCount = occurrences;
                
                if (occurrences == 0)
                {
                    Console.WriteLine($"\nСлово '{searchWord}' не знайдено у файлі.");
                }
                else
                {
                    // Збереження результату
                    string backupFilePath = filePath + ".backup";
                    string resultFilePath = filePath + ".modified.txt";
                    
                    // Створення резервної копії
                    File.Copy(filePath, backupFilePath, true);
                    Console.WriteLine($"Створено резервну копію: {backupFilePath}");
                    
                    // Збереження зміненого файлу
                    using (StreamWriter writer = new StreamWriter(resultFilePath, false, Encoding.UTF8))
                    {
                        writer.Write(resultContent.ToString());
                    }
                    
                    // Оновлення оригінального файлу (опціонально)
                    Console.Write($"\nОновити оригінальний файл '{filePath}'? (y/n): ");
                    string updateOriginal = Console.ReadLine().ToLower();
                    
                    if (updateOriginal == "y" || updateOriginal == "так")
                    {
                        File.WriteAllText(filePath, resultContent.ToString(), Encoding.UTF8);
                        Console.WriteLine($"Оригінальний файл оновлено.");
                        stats.ModifiedFilePath = filePath;
                    }
                    else
                    {
                        Console.WriteLine($"Збережено новий файл: {resultFilePath}");
                        stats.ModifiedFilePath = resultFilePath;
                    }
                    
                    stats.ModifiedFileSize = new FileInfo(stats.ModifiedFilePath).Length;
                }
                
                stopwatch.Stop();
                stats.ExecutionTime = stopwatch.Elapsed;
                
                // Відображення статистики
                Console.WriteLine("\n" + new string('=', 50));
                Console.WriteLine("СТАТИСТИКА ПОШУКУ ТА ЗАМІНИ");
                Console.WriteLine(new string('=', 50));
                
                DisplaySearchReplaceStatistics(stats, searchWord, replaceWord);
                
                // Показати приклад змін
                if (occurrences > 0)
                {
                    Console.WriteLine("\n" + new string('-', 50));
                    Console.WriteLine("ПРИКЛАД ЗМІН");
                    Console.WriteLine(new string('-', 50));
                    
                    // Знаходимо перше входження для демонстрації
                    int firstOccurrence = originalContent.IndexOf(searchWord, StringComparison.OrdinalIgnoreCase);
                    if (firstOccurrence >= 0)
                    {
                        int start = Math.Max(0, firstOccurrence - 30);
                        int length = Math.Min(originalContent.Length - start, searchWord.Length + 60);
                        
                        string context = originalContent.Substring(start, length);
                        string modifiedContext = context.Replace(searchWord, replaceWord, StringComparison.OrdinalIgnoreCase);
                        
                        Console.WriteLine("\nОригінальний текст (фрагмент):");
                        Console.WriteLine($"...{context}...");
                        
                        Console.WriteLine("\nЗмінений текст (фрагмент):");
                        Console.WriteLine($"...{modifiedContext}...");
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nПОМИЛКА: {ex.Message}");
                Console.WriteLine($"Деталі: {ex.StackTrace}");
            }
        }
        
        // Метод для перевірки чи число є простим
        static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;
            
            int boundary = (int)Math.Floor(Math.Sqrt(number));
            
            for (int i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0) return false;
            }
            
            return true;
        }
        
        // Метод для перевірки чи число є числом Фібоначчі
        static bool IsFibonacci(int number)
        {
            if (number < 0) return false;
            
            // Число є числом Фібоначчі, якщо 5*n*n + 4 або 5*n*n - 4 є повним квадратом
            return IsPerfectSquare(5 * number * number + 4) || 
                   IsPerfectSquare(5 * number * number - 4);
        }
        
        static bool IsPerfectSquare(int x)
        {
            int s = (int)Math.Sqrt(x);
            return s * s == x;
        }
        
        // Метод для збереження чисел у файл
        static void SaveNumbersToFile(string filePath, List<int> numbers, string header)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                writer.WriteLine($"// {header}");
                writer.WriteLine($"// Кількість: {numbers.Count}");
                writer.WriteLine($"// Дата створення: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
                writer.WriteLine(new string('-', 40));
                
                if (numbers.Count == 0)
                {
                    writer.WriteLine("Числа не знайдені.");
                }
                else
                {
                    for (int i = 0; i < numbers.Count; i++)
                    {
                        writer.Write($"{numbers[i],4}");
                        if ((i + 1) % 10 == 0 || i == numbers.Count - 1)
                            writer.WriteLine();
                        else
                            writer.Write(", ");
                    }
                }
                
                writer.WriteLine(new string('-', 40));
                writer.WriteLine($"// Сума чисел: {CalculateSum(numbers)}");
                writer.WriteLine($"// Середнє значення: {CalculateAverage(numbers):F2}");
            }
        }
        
        static long CalculateSum(List<int> numbers)
        {
            long sum = 0;
            foreach (int num in numbers)
                sum += num;
            return sum;
        }
        
        static double CalculateAverage(List<int> numbers)
        {
            if (numbers.Count == 0) return 0;
            return CalculateSum(numbers) / (double)numbers.Count;
        }
        
        // Метод для створення тестового файлу
        static void CreateTestFile(string filePath)
        {
            string[] lines = {
                "Це тестовий файл для перевірки роботи програми пошуку та заміни.",
                "Тут містяться різні слова для тестування функціоналу.",
                "Слово 'тест' зустрічається кілька разів у цьому тексті.",
                "Також тут є слово Тест з великої літери і слово ТЕСТ у верхньому регістрі.",
                "Програма повинна знайти всі варіанти слова 'тест' незалежно від регістру.",
                "Це важливо для коректної роботи функції пошуку та заміни.",
                "Після заміни слово 'тест' буде замінене на вказане користувачем слово.",
                "Давайте протестуємо роботу програми на цьому прикладі тексту."
            };
            
            File.WriteAllLines(filePath, lines, Encoding.UTF8);
        }
        
        // Методи для відображення статистики
        static void DisplayStatistics(Statistics stats)
        {
            Console.WriteLine($"Загальна кількість згенерованих чисел: {stats.TotalNumbersGenerated}");
            Console.WriteLine($"Знайдено простих чисел: {stats.PrimeNumbersCount}");
            Console.WriteLine($"Знайдено чисел Фібоначчі: {stats.FibonacciNumbersCount}");
            
            double primePercentage = (double)stats.PrimeNumbersCount / stats.TotalNumbersGenerated * 100;
            double fibPercentage = (double)stats.FibonacciNumbersCount / stats.TotalNumbersGenerated * 100;
            
            Console.WriteLine($"\nВідсоток простих чисел: {primePercentage:F2}%");
            Console.WriteLine($"Відсоток чисел Фібоначчі: {fibPercentage:F2}%");
            
            Console.WriteLine($"\nРозмір файлу з усіма числами: {FormatFileSize(stats.AllNumbersFileSize)}");
            Console.WriteLine($"Розмір файлу з простими числами: {FormatFileSize(stats.PrimeNumbersFileSize)}");
            Console.WriteLine($"Розмір файлу з числами Фібоначчі: {FormatFileSize(stats.FibonacciNumbersFileSize)}");
            
            Console.WriteLine($"\nЧас виконання: {stats.ExecutionTime.TotalMilliseconds:F2} мс");
            Console.WriteLine($"Дата виконання: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
        }
        
        static void DisplaySearchReplaceStatistics(SearchReplaceStats stats, string searchWord, string replaceWord)
        {
            Console.WriteLine($"Файл: {stats.FilePath}");
            Console.WriteLine($"Слово для пошуку: '{searchWord}'");
            Console.WriteLine($"Слово для заміни: '{replaceWord}'");
            
            Console.WriteLine($"\nЗагальна кількість символів у файлі: {stats.TotalCharacters}");
            Console.WriteLine($"Кількість рядків у файлі: {stats.TotalLines}");
            
            if (stats.ReplacementsCount == 0)
            {
                Console.WriteLine($"\nЗамін не виконано. Слово '{searchWord}' не знайдено.");
            }
            else
            {
                Console.WriteLine($"\nКількість замін: {stats.ReplacementsCount}");
                Console.WriteLine($"Розмір оригінального файлу: {FormatFileSize(stats.OriginalFileSize)}");
                
                if (stats.ModifiedFileSize > 0)
                {
                    Console.WriteLine($"Розмір зміненого файлу: {FormatFileSize(stats.ModifiedFileSize)}");
                    
                    long sizeDifference = stats.ModifiedFileSize - stats.OriginalFileSize;
                    Console.WriteLine($"Різниця у розмірі: {(sizeDifference >= 0 ? "+" : "")}{FormatFileSize(sizeDifference)}");
                }
            }
            
            Console.WriteLine($"\nЧас виконання: {stats.ExecutionTime.TotalMilliseconds:F2} мс");
            Console.WriteLine($"Дата виконання: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
        }
        
        static void DisplayFileInfo(string filePath, string description)
        {
            if (File.Exists(filePath))
            {
                FileInfo fileInfo = new FileInfo(filePath);
                string content = File.ReadAllText(filePath);
                int lineCount = content.Split('\n').Length;
                
                Console.WriteLine($"\n{description}:");
                Console.WriteLine($"  Файл: {filePath}");
                Console.WriteLine($"  Розмір: {FormatFileSize(fileInfo.Length)}");
                Console.WriteLine($"  Рядків: {lineCount}");
                Console.WriteLine($"  Створено: {fileInfo.CreationTime:dd.MM.yyyy HH:mm}");
                Console.WriteLine($"  Остання зміна: {fileInfo.LastWriteTime:dd.MM.yyyy HH:mm}");
            }
        }
        
        static string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            double len = bytes;
            int order = 0;
            
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }
            
            return $"{len:F2} {sizes[order]}";
        }
        
        // Класи для статистики
        class Statistics
        {
            public int TotalNumbersGenerated { get; set; }
            public int PrimeNumbersCount { get; set; }
            public int FibonacciNumbersCount { get; set; }
            public long AllNumbersFileSize { get; set; }
            public long PrimeNumbersFileSize { get; set; }
            public long FibonacciNumbersFileSize { get; set; }
            public TimeSpan ExecutionTime { get; set; }
        }
        
        class SearchReplaceStats
        {
            public string FilePath { get; set; }
            public string ModifiedFilePath { get; set; }
            public int TotalCharacters { get; set; }
            public int TotalLines { get; set; }
            public int ReplacementsCount { get; set; }
            public long OriginalFileSize { get; set; }
            public long ModifiedFileSize { get; set; }
            public TimeSpan ExecutionTime { get; set; }
        }
    }
}