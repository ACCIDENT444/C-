using System;

class Shop
{
    // Властивості
    public string Name { get; set; }
    public string Address { get; set; }
    public double Area { get; set; } // Нова властивість - площа
    
    // Конструктор
    public Shop(string name, string address, double area)
    {
        Name = name;
        Address = address;
        Area = area;
    }
    
    // Виведення інформації
    public void PrintInfo()
    {
        Console.WriteLine($"Магазин: {Name}");
        Console.WriteLine($"Адреса: {Address}");
        Console.WriteLine($"Площа: {Area} кв.м");
        Console.WriteLine();
    }
    
    // Перевантаження оператора + (збільшення площі)
    public static Shop operator +(Shop shop, double amount)
    {
        shop.Area += amount;
        return shop;
    }
    
    // Перевантаження оператора - (зменшення площі)
    public static Shop operator -(Shop shop, double amount)
    {
        shop.Area -= amount;
        if (shop.Area < 0) shop.Area = 0;
        return shop;
    }
    
    // Перевантаження == (рівність площ)
    public static bool operator ==(Shop s1, Shop s2)
    {
        return s1.Area == s2.Area;
    }
    
    // Перевантаження != (нерівність площ)
    public static bool operator !=(Shop s1, Shop s2)
    {
        return s1.Area != s2.Area;
    }
    
    // Перевантаження > (більша площа)
    public static bool operator >(Shop s1, Shop s2)
    {
        return s1.Area > s2.Area;
    }
    
    // Перевантаження < (менша площа)
    public static bool operator <(Shop s1, Shop s2)
    {
        return s1.Area < s2.Area;
    }
    
    // Перевизначення методу Equals
    public override bool Equals(object obj)
    {
        if (obj is Shop otherShop)
        {
            return this.Area == otherShop.Area;
        }
        return false;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== ПРИКЛАД РОБОТИ З МАГАЗИНАМИ ===");
        
        // Створюємо магазини
        Shop shop1 = new Shop("Продукти", "вул. Центральна, 10", 150.5);
        Shop shop2 = new Shop("Одяг", "вул. Модна, 5", 200.0);
        
        Console.WriteLine("Початкові дані:");
        shop1.PrintInfo();
        shop2.PrintInfo();
        
        // Використання перевантажених операторів
        Console.WriteLine("Після змін площ:");
        shop1 = shop1 + 50.5; // Додаємо 50.5 кв.м
        shop2 = shop2 - 30.0; // Віднімаємо 30 кв.м
        
        shop1.PrintInfo();
        shop2.PrintInfo();
        
        // Перевірка операторів порівняння
        Console.WriteLine("Порівняння магазинів:");
        Console.WriteLine($"shop1 == shop2: {shop1 == shop2}");
        Console.WriteLine($"shop1 != shop2: {shop1 != shop2}");
        Console.WriteLine($"shop1 > shop2: {shop1 > shop2}");
        Console.WriteLine($"shop1 < shop2: {shop1 < shop2}");
        
        // Перевірка методу Equals
        Console.WriteLine($"shop1.Equals(shop2): {shop1.Equals(shop2)}");
    }
}