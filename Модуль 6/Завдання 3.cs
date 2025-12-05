using System;

// Базовий клас Money
class Money
{
    // Властивості
    public int Dollars { get; set; }
    public int Cents { get; set; }
    
    // Конструктор
    public Money(int dollars, int cents)
    {
        Dollars = dollars;
        Cents = cents;
        Normalize(); // Приводимо до правильного формату
    }
    
    // Приведення до правильного формату (якщо центів > 99)
    private void Normalize()
    {
        if (Cents >= 100)
        {
            Dollars += Cents / 100;
            Cents = Cents % 100;
        }
    }
    
    // Метод для виведення суми
    public void Display()
    {
        Console.WriteLine($"{Dollars}.{Cents:00}");
    }
    
    // Метод для встановлення значень
    public void SetAmount(int dollars, int cents)
    {
        Dollars = dollars;
        Cents = cents;
        Normalize();
    }
    
    // Отримати суму у вигляді дробового числа
    public decimal GetAmount()
    {
        return Dollars + (Cents / 100m);
    }
}

// Похідний клас Product
class Product : Money
{
    // Властивості продукту
    public string Name { get; set; }
    
    // Конструктор
    public Product(string name, int dollars, int cents) : base(dollars, cents)
    {
        Name = name;
    }
    
    // Метод для зменшення ціни
    public void ReducePrice(int dollars, int cents)
    {
        // Перетворюємо все в центи для зручності
        int totalCurrentCents = (Dollars * 100) + Cents;
        int reduceCents = (dollars * 100) + cents;
        
        int newCents = totalCurrentCents - reduceCents;
        
        if (newCents < 0)
            newCents = 0;
        
        Dollars = newCents / 100;
        Cents = newCents % 100;
    }
    
    // Вивести інформацію про продукт
    public void DisplayProduct()
    {
        Console.WriteLine($"Продукт: {Name}");
        Console.Write("Ціна: ");
        Display();
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== РОБОТА З ГРОШИМА ===");
        
        // Створюємо об'єкт Money
        Money wallet = new Money(10, 150); // 10 доларів 150 центів = 11.50
        Console.Write("Сума в гаманці: ");
        wallet.Display();
        
        // Змінюємо суму
        wallet.SetAmount(20, 75);
        Console.Write("Нова сума: ");
        wallet.Display();
        
        Console.WriteLine("\n=== РОБОТА З ПРОДУКТАМИ ===");
        
        // Створюємо продукт
        Product apple = new Product("Яблуко", 2, 99);
        apple.DisplayProduct();
        
        // Зменшуємо ціну
        apple.ReducePrice(1, 50);
        Console.Write("Ціна після знижки: ");
        apple.Display();
        
        // Ще один продукт
        Product laptop = new Product("Ноутбук", 1000, 0);
        laptop.DisplayProduct();
        
        laptop.ReducePrice(150, 75);
        Console.Write("Ціна після знижки: ");
        laptop.Display();
    }
}