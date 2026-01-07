using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SerializationDemo
{
    // ========== ЗАВДАННЯ 1: Робота з дробами ==========
    
    // Клас для представлення дробу
    public class Fraction
    {
        public int Numerator { get; set; }
        public int Denominator { get; set; }

        public Fraction() { } // Порожній конструктор для серіалізації

        public Fraction(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }
    }

    // Обробник дробів з серіалізацією в JSON
    public class FractionManager
    {
        private List<Fraction> fractions = new List<Fraction>();

        // Введення масиву дробів з клавіатури
        public void InputFractionsFromKeyboard()
        {
            Console.WriteLine("\n=== Введення дробів ===");
            Console.Write("Скільки дробів ви хочете ввести? ");
            int count;
            while (!int.TryParse(Console.ReadLine(), out count) || count <= 0)
            {
                Console.Write("Будь ласка, введіть додатне число: ");
            }

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\nДроб {i + 1}:");
                
                int numerator;
                Console.Write("Введіть чисельник: ");
                while (!int.TryParse(Console.ReadLine(), out numerator))
                {
                    Console.Write("Будь ласка, введіть ціле число: ");
                }

                int denominator;
                Console.Write("Введіть знаменник: ");
                while (!int.TryParse(Console.ReadLine(), out denominator) || denominator == 0)
                {
                    Console.Write("Знаменник не може бути 0. Введіть інше число: ");
                }

                fractions.Add(new Fraction(numerator, denominator));
            }

            Console.WriteLine($"\nУспішно додано {count} дробів!");
        }

        // Виведення дробів
        public void DisplayFractions()
        {
            Console.WriteLine("\n=== Список дробів ===");
            if (fractions.Count == 0)
            {
                Console.WriteLine("Список порожній.");
                return;
            }

            for (int i = 0; i < fractions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {fractions[i]}");
            }
        }

        // Серіалізація та збереження у файл
        public void SaveToFile(string filename = "fractions.json")
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true, // Для читабельності
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                string json = JsonSerializer.Serialize(fractions, options);
                File.WriteAllText(filename, json);
                Console.WriteLine($"\nДроби збережено у файл: {filename}");
                Console.WriteLine($"Розмір файлу: {new FileInfo(filename).Length} байт");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка збереження: {ex.Message}");
            }
        }

        // Завантаження та десеріалізація з файлу
        public void LoadFromFile(string filename = "fractions.json")
        {
            try
            {
                if (!File.Exists(filename))
                {
                    Console.WriteLine($"Файл {filename} не знайдено!");
                    return;
                }

                string json = File.ReadAllText(filename);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                fractions = JsonSerializer.Deserialize<List<Fraction>>(json, options);
                Console.WriteLine($"\nДроби завантажено з файлу: {filename}");
                Console.WriteLine($"Завантажено {fractions.Count} дробів");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка завантаження: {ex.Message}");
            }
        }

        // Очищення списку
        public void ClearFractions()
        {
            fractions.Clear();
            Console.WriteLine("\nСписок дробів очищено.");
        }
    }

    // ========== ЗАВДАННЯ 2: Робота з журналами ==========
    
    // Клас для представлення журналу
    public class Magazine
    {
        public string Title { get; set; }
        public string Publisher { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int PageCount { get; set; }

        public Magazine() { } // Порожній конструктор для серіалізації

        public Magazine(string title, string publisher, DateTime releaseDate, int pageCount)
        {
            Title = title;
            Publisher = publisher;
            ReleaseDate = releaseDate;
            PageCount = pageCount;
        }

        public override string ToString()
        {
            return $"Назва: {Title}\nВидавництво: {Publisher}\nДата: {ReleaseDate:dd.MM.yyyy}\nСторінок: {PageCount}";
        }
    }

    // Обробник журналів з серіалізацією в XML
    public class MagazineManager
    {
        private Magazine magazine;

        // Введення інформації про журнал
        public void InputMagazine()
        {
            Console.WriteLine("\n=== Введення інформації про журнал ===");
            
            Console.Write("Назва журналу: ");
            string title = Console.ReadLine();

            Console.Write("Назва видавництва: ");
            string publisher = Console.ReadLine();

            DateTime releaseDate;
            Console.Write("Дата випуску (рррр-мм-дд): ");
            while (!DateTime.TryParse(Console.ReadLine(), out releaseDate))
            {
                Console.Write("Будь ласка, введіть коректну дату (рррр-мм-дд): ");
            }

            int pageCount;
            Console.Write("Кількість сторінок: ");
            while (!int.TryParse(Console.ReadLine(), out pageCount) || pageCount <= 0)
            {
                Console.Write("Будь ласка, введіть додатне число: ");
            }

            magazine = new Magazine(title, publisher, releaseDate, pageCount);
            Console.WriteLine("\nІнформацію про журнал збережено!");
        }

        // Виведення інформації про журнал
        public void DisplayMagazine()
        {
            Console.WriteLine("\n=== Інформація про журнал ===");
            if (magazine == null)
            {
                Console.WriteLine("Журнал не завантажено.");
                return;
            }

            Console.WriteLine(magazine);
        }

        // Серіалізація та збереження у файл
        public void SaveToFile(string filename = "magazine.xml")
        {
            try
            {
                if (magazine == null)
                {
                    Console.WriteLine("\nНемає даних для збереження!");
                    return;
                }

                var serializer = new XmlSerializer(typeof(Magazine));
                using (var writer = new StreamWriter(filename))
                {
                    serializer.Serialize(writer, magazine);
                }

                Console.WriteLine($"\nЖурнал збережено у файл: {filename}");
                Console.WriteLine($"Розмір файлу: {new FileInfo(filename).Length} байт");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка збереження: {ex.Message}");
            }
        }

        // Завантаження та десеріалізація з файлу
        public void LoadFromFile(string filename = "magazine.xml")
        {
            try
            {
                if (!File.Exists(filename))
                {
                    Console.WriteLine($"Файл {filename} не знайдено!");
                    return;
                }

                var serializer = new XmlSerializer(typeof(Magazine));
                using (var reader = new StreamReader(filename))
                {
                    magazine = (Magazine)serializer.Deserialize(reader);
                }

                Console.WriteLine($"\nЖурнал завантажено з файлу: {filename}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка завантаження: {ex.Message}");
            }
        }
    }

    // ========== Головна програма ==========
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ПРОГРАМА СЕРІАЛІЗАЦІЇ ОБ'ЄКТІВ ===");
            Console.WriteLine("Розробник: [Ваше ім'я]");
            Console.WriteLine("Дата: " + DateTime.Now.ToString("dd.MM.yyyy"));
            Console.WriteLine("=====================================\n");

            // Ініціалізація менеджерів
            var fractionManager = new FractionManager();
            var magazineManager = new MagazineManager();

            // Головне меню
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n=== ГОЛОВНЕ МЕНЮ ===");
                Console.WriteLine("1. Робота з дробами (Завдання 1)");
                Console.WriteLine("2. Робота з журналами (Завдання 2)");
                Console.WriteLine("3. Вийти");
                Console.Write("Виберіть опцію: ");

                string choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1":
                        ShowFractionMenu(fractionManager);
                        break;
                    case "2":
                        ShowMagazineMenu(magazineManager);
                        break;
                    case "3":
                        exit = true;
                        Console.WriteLine("Дякуємо за використання програми!");
                        break;
                    default:
                        Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                        break;
                }
            }
        }

        static void ShowFractionMenu(FractionManager manager)
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n=== МЕНЮ РОБОТИ З ДРОБАМИ ===");
                Console.WriteLine("1. Ввести дроби з клавіатури");
                Console.WriteLine("2. Показати всі дроби");
                Console.WriteLine("3. Зберегти дроби у файл (JSON)");
                Console.WriteLine("4. Завантажити дроби з файлу");
                Console.WriteLine("5. Очистити список дробів");
                Console.WriteLine("6. Назад до головного меню");
                Console.Write("Виберіть опцію: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        manager.InputFractionsFromKeyboard();
                        break;
                    case "2":
                        manager.DisplayFractions();
                        break;
                    case "3":
                        Console.Write("Введіть ім'я файлу (натисніть Enter для fractions.json): ");
                        string saveFile = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(saveFile))
                            saveFile = "fractions.json";
                        manager.SaveToFile(saveFile);
                        break;
                    case "4":
                        Console.Write("Введіть ім'я файлу (натисніть Enter для fractions.json): ");
                        string loadFile = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(loadFile))
                            loadFile = "fractions.json";
                        manager.LoadFromFile(loadFile);
                        break;
                    case "5":
                        manager.ClearFractions();
                        break;
                    case "6":
                        back = true;
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Невірний вибір.");
                        break;
                }
            }
        }

        static void ShowMagazineMenu(MagazineManager manager)
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n=== МЕНЮ РОБОТИ З ЖУРНАЛАМИ ===");
                Console.WriteLine("1. Ввести інформацію про журнал");
                Console.WriteLine("2. Показати інформацію про журнал");
                Console.WriteLine("3. Зберегти журнал у файл (XML)");
                Console.WriteLine("4. Завантажити журнал з файлу");
                Console.WriteLine("5. Назад до головного меню");
                Console.Write("Виберіть опцію: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        manager.InputMagazine();
                        break;
                    case "2":
                        manager.DisplayMagazine();
                        break;
                    case "3":
                        Console.Write("Введіть ім'я файлу (натисніть Enter для magazine.xml): ");
                        string saveFile = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(saveFile))
                            saveFile = "magazine.xml";
                        manager.SaveToFile(saveFile);
                        break;
                    case "4":
                        Console.Write("Введіть ім'я файлу (натисніть Enter для magazine.xml): ");
                        string loadFile = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(loadFile))
                            loadFile = "magazine.xml";
                        manager.LoadFromFile(loadFile);
                        break;
                    case "5":
                        back = true;
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Невірний вибір.");
                        break;
                }
            }
        }
    }
}