using System;
using System.Collections.Generic;
using System.Threading;

namespace Module10
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Завдання 1: Клас 'П'єса' з деструктором ===\n");
            TestPlayClass();
            
            Console.WriteLine("\n=== Завдання 2: Клас 'Магазин' з IDisposable ===\n");
            TestShopClass();
            
            Console.WriteLine("\nПрограма завершена. Натисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
        
        static void TestPlayClass()
        {
            // Створення об'єктів класу Play
            Console.WriteLine("Створення об'єктів класу Play:");
            
            Play play1 = new Play("Ромео і Джульєтта", "Вільям Шекспір", "Трагедія", 1597);
            Play play2 = new Play("Гамлет", "Вільям Шекспір", "Трагедія", 1601);
            Play play3 = new Play("Лісова пісня", "Леся Українка", "Драма-феєрія", 1911);
            Play play4 = new Play("За двома зайцями", "Михайло Старицький", "Комедія", 1883);
            
            // Виведення інформації про п'єси
            Console.WriteLine("\nІнформація про створені п'єси:");
            Console.WriteLine(play1.GetInfo());
            Console.WriteLine(play2.GetInfo());
            Console.WriteLine(play3.GetInfo());
            Console.WriteLine(play4.GetInfo());
            
            // Тестування властивостей
            Console.WriteLine("\nТестування властивостей:");
            Console.WriteLine($"Назва п'єси 1: {play1.Title}");
            Console.WriteLine($"Автор п'єси 2: {play2.Author}");
            Console.WriteLine($"Жанр п'єси 3: {play3.Genre}");
            Console.WriteLine($"Рік п'єси 4: {play4.Year}");
            
            // Зміна властивостей
            Console.WriteLine("\nЗміна властивостей п'єси:");
            play1.Title = "Ромео та Джульєтта";
            play1.Year = 1595;
            Console.WriteLine($"Оновлена інформація: {play1.GetInfo()}");
            
            // Валідація даних
            Console.WriteLine("\nТестування валідації даних:");
            try
            {
                play1.Year = 3000; // Некоректний рік
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка валідації: {ex.Message}");
            }
            
            // Перевірка роботи деструктора
            Console.WriteLine("\nПеревірка роботи деструктора:");
            
            // Створення і швидке видалення об'єктів
            for (int i = 0; i < 3; i++)
            {
                Play tempPlay = new Play($"Тестова п'єса {i+1}", "Тестовий автор", "Тест", 2000 + i);
                Console.WriteLine($"Створено тимчасову п'єсу: {tempPlay.Title}");
                // Об'єкт буде доступний для збирання сміття після виходу з області видимості
            }
            
            // Примусовий виклик збирання сміття для демонстрації деструктора
            Console.WriteLine("\nПримусовий виклик GC.Collect():");
            play1 = null; // Втрачаємо посилання на об'єкт
            play2 = null;
            
            GC.Collect(); // Запускаємо збирання сміття
            GC.WaitForPendingFinalizers(); // Чекаємо завершення деструкторів
            
            Console.WriteLine("GC.Collect() викликано. Деструктори можуть спрацювати.");
            
            // Демонстрація, що об'єкти ще існують
            Console.WriteLine($"\nП'єса 3 ще доступна: {play3?.Title ?? "null"}");
            Console.WriteLine($"П'єса 4 ще доступна: {play4?.Title ?? "null"}");
            
            // Створення багатьох об'єктів для демонстрації роботи GC
            Console.WriteLine("\nСтворення багатьох об'єктів для демонстрації роботи GC:");
            List<Play> manyPlays = new List<Play>();
            
            for (int i = 0; i < 10000; i++)
            {
                manyPlays.Add(new Play($"П'єса {i}", $"Автор {i}", "Жанр", 1900 + (i % 100)));
            }
            
            Console.WriteLine($"Створено {manyPlays.Count} об'єктів");
            manyPlays.Clear(); // Видаляємо посилання на всі об'єкти
            
            // Ще один виклик GC для демонстрації
            GC.Collect();
            GC.WaitForPendingFinalizers();
            
            Console.WriteLine("Після видалення 10000 об'єктів.");
        }
        
        static void TestShopClass()
        {
            // Демонстрація використання using
            Console.WriteLine("Демонстрація використання using statement:");
            
            using (Shop shop1 = new Shop("Сільпо", "вул. Хрещатик, 10", ShopType.Grocery))
            {
                Console.WriteLine($"Магазин у блоці using: {shop1.Name}");
                Console.WriteLine(shop1.GetInfo());
                
                // Додавання товарів
                shop1.AddProduct("Хліб", 25.50m);
                shop1.AddProduct("Молоко", 45.00m);
                shop1.AddProduct("Сир", 120.75m);
                
                Console.WriteLine("\nТовари в магазині:");
                shop1.DisplayProducts();
                
                Console.WriteLine($"\nЗагальна вартість товарів: {shop1.CalculateTotalValue():C}");
            } // Dispose викликається автоматично
            
            Console.WriteLine("\nБлок using завершено. Dispose() викликано автоматично.");
            
            // Демонстрація ручного виклику Dispose
            Console.WriteLine("\nДемонстрація ручного виклику Dispose():");
            
            Shop shop2 = null;
            try
            {
                shop2 = new Shop("Епіцентр", "вул. Б. Хмельницького, 37/1", ShopType.Household);
                Console.WriteLine($"Створено магазин: {shop2.Name}");
                Console.WriteLine(shop2.GetInfo());
                
                shop2.AddProduct("Дриль", 2500.00m);
                shop2.AddProduct("Фарба", 450.50m);
                shop2.AddProduct("Інструменти", 1200.75m);
                
                Console.WriteLine("\nТовари в магазині:");
                shop2.DisplayProducts();
            }
            finally
            {
                // Гарантоване вивільнення ресурсів
                if (shop2 != null)
                {
                    shop2.Dispose();
                    Console.WriteLine("Dispose() викликано вручну в блоці finally");
                }
            }
            
            // Демонстрація без using і без ручного Dispose (неправильний підхід)
            Console.WriteLine("\nДемонстрація без використання Dispose (неправильний підхід):");
            
            Shop shop3 = new Shop("Intertop", "ТРЦ Ocean Plaza", ShopType.Shoes);
            Console.WriteLine($"Створено магазин: {shop3.Name}");
            // Не викликаємо Dispose - ресурси можуть не вивільнитися вчасно
            
            // Демонстрація повторного виклику Dispose
            Console.WriteLine("\nДемонстрація повторного виклику Dispose():");
            
            using (Shop shop4 = new Shop("Reserved", "вул. Велика Васильківська, 72", ShopType.Clothing))
            {
                Console.WriteLine($"Магазин створений: {shop4.Name}");
                
                // Можливість повторного виклику Dispose
                shop4.Dispose(); // Перший виклик
                Console.WriteLine("Dispose викликано вручну всередині using");
                
                // Перевірка, чи об'єкт ще можна використовувати
                Console.WriteLine($"Чи магазин все ще відкритий? {!shop4.IsDisposed}");
                
                // Спроба використання після Dispose
                try
                {
                    shop4.AddProduct("Футболка", 500.00m);
                }
                catch (ObjectDisposedException ex)
                {
                    Console.WriteLine($"Помилка при використанні після Dispose: {ex.Message}");
                }
            } // Другий виклик Dispose не призведе до помилки
            
            // Демонстрація виклику Dispose через деструктор
            Console.WriteLine("\nДемонстрація виклику Dispose через деструктор:");
            
            CreateAndForgetShop();
            
            // Примусовий виклик GC для демонстрації деструктора
            GC.Collect();
            GC.WaitForPendingFinalizers();
            
            Console.WriteLine("GC викликано. Деструктор Shop має викликати Dispose.");
        }
        
        static void CreateAndForgetShop()
        {
            // Створюємо об'єкт і відразу втрачаємо посилання на нього
            Shop tempShop = new Shop("Тимчасовий магазин", "Тимчасова адреса", ShopType.Grocery);
            Console.WriteLine($"Створено тимчасовий магазин: {tempShop.Name}");
            // Об'єкт стане доступним для GC після виходу з методу
        }
    }
    
    // Завдання 1: Клас "П'єса"
    public class Play
    {
        // Поля
        private string title;
        private string author;
        private string genre;
        private int year;
        
        // Властивості
        public string Title
        {
            get => title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Назва не може бути порожньою");
                title = value;
            }
        }
        
        public string Author
        {
            get => author;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Автор не може бути порожнім");
                author = value;
            }
        }
        
        public string Genre
        {
            get => genre;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Жанр не може бути порожнім");
                genre = value;
            }
        }
        
        public int Year
        {
            get => year;
            set
            {
                if (value < 1500 || value > DateTime.Now.Year)
                    throw new ArgumentException($"Рік має бути між 1500 і {DateTime.Now.Year}");
                year = value;
            }
        }
        
        // Конструктори
        public Play(string title, string author, string genre, int year)
        {
            Title = title;
            Author = author;
            Genre = genre;
            Year = year;
            Console.WriteLine($"Конструктор Play: створено п'єсу '{title}'");
        }
        
        // Методи
        public string GetInfo()
        {
            return $"'{Title}' - {Author}, {Genre}, {Year} рік";
        }
        
        public bool IsClassic()
        {
            return year < 1900;
        }
        
        public int GetAge()
        {
            return DateTime.Now.Year - year;
        }
        
        // Деструктор (фіналізатор)
        ~Play()
        {
            Console.WriteLine($"Деструктор Play: п'єса '{title}' видаляється з пам'яті");
            // Тут можна звільнити некеровані ресурси, якщо вони є
            
            // Додаткова логіка для демонстрації
            Thread.Sleep(100); // Невелика затримка для демонстрації
        }
    }
    
    // Завдання 2: Enum для типів магазинів
    public enum ShopType
    {
        Grocery,    // Продовольчий
        Household,  // Господарський
        Clothing,   // Одяг
        Shoes       // Взуття
    }
    
    // Завдання 2: Клас "Магазин" з IDisposable
    public class Shop : IDisposable
    {
        // Поля
        private string name;
        private string address;
        private ShopType type;
        private List<Product> products;
        private bool disposed = false;
        
        // Властивості
        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Назва магазину не може бути порожньою");
                name = value;
            }
        }
        
        public string Address
        {
            get => address;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Адреса не може бути порожньою");
                address = value;
            }
        }
        
        public ShopType Type
        {
            get => type;
            set => type = value;
        }
        
        public bool IsDisposed => disposed;
        
        // Допоміжний клас для товарів
        private class Product
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            
            public Product(string name, decimal price)
            {
                Name = name;
                Price = price;
            }
        }
        
        // Конструктор
        public Shop(string name, string address, ShopType type)
        {
            Name = name;
            Address = address;
            Type = type;
            products = new List<Product>();
            Console.WriteLine($"Конструктор Shop: створено магазин '{name}'");
        }
        
        // Методи
        public string GetInfo()
        {
            string typeStr = GetShopTypeString();
            return $"Магазин '{Name}' ({typeStr})\nАдреса: {Address}";
        }
        
        private string GetShopTypeString()
        {
            return type switch
            {
                ShopType.Grocery => "Продовольчий",
                ShopType.Household => "Господарський",
                ShopType.Clothing => "Одяг",
                ShopType.Shoes => "Взуття",
                _ => "Невідомий"
            };
        }
        
        public void AddProduct(string productName, decimal price)
        {
            if (disposed)
                throw new ObjectDisposedException(nameof(Shop), "Магазин вже закритий");
                
            if (string.IsNullOrWhiteSpace(productName))
                throw new ArgumentException("Назва товару не може бути порожньою");
                
            if (price <= 0)
                throw new ArgumentException("Ціна має бути додатньою");
                
            products.Add(new Product(productName, price));
        }
        
        public void DisplayProducts()
        {
            if (disposed)
                throw new ObjectDisposedException(nameof(Shop), "Магазин вже закритий");
                
            if (products.Count == 0)
            {
                Console.WriteLine("Товарів немає");
                return;
            }
            
            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {products[i].Name} - {products[i].Price:C}");
            }
        }
        
        public decimal CalculateTotalValue()
        {
            if (disposed)
                throw new ObjectDisposedException(nameof(Shop), "Магазин вже закритий");
                
            decimal total = 0;
            foreach (var product in products)
            {
                total += product.Price;
            }
            return total;
        }
        
        // Реалізація IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Звільнення керованих ресурсів
                    Console.WriteLine($"Dispose: звільнення керованих ресурсів магазину '{name}'");
                    
                    if (products != null)
                    {
                        products.Clear();
                        products = null;
                        Console.WriteLine("Список товарів очищено");
                    }
                }
                
                // Звільнення некерованих ресурсів (якщо є)
                Console.WriteLine($"Dispose: звільнення некерованих ресурсів магазину '{name}'");
                
                // Додаткова логіка для демонстрації
                Thread.Sleep(100); // Невелика затримка для демонстрації
                
                disposed = true;
            }
        }
        
        // Деструктор (фіналізатор) - резервний механізм
        ~Shop()
        {
            Console.WriteLine($"Деструктор Shop: магазин '{name}' видаляється з пам'яті");
            Dispose(false);
        }
    }
}