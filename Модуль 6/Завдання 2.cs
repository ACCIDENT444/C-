using System;

// Базовий клас Device
class Device
{
    public string Name { get; set; }
    public string Brand { get; set; }
    public int Power { get; set; } // у ватах
    
    // Конструктор
    public Device(string name, string brand, int power)
    {
        Name = name;
        Brand = brand;
        Power = power;
    }
    
    // Віртуальні методи (будуть перевизначені у похідних класах)
    public virtual void Sound()
    {
        Console.WriteLine("Пристрій видає звук");
    }
    
    public virtual void Show()
    {
        Console.WriteLine($"Назва пристрою: {Name}");
    }
    
    public virtual void Desc()
    {
        Console.WriteLine($"Опис: {Name}, бренд: {Brand}, потужність: {Power}W");
    }
}

// Похідний клас Kettle
class Kettle : Device
{
    public double Capacity { get; set; } // об'єм у літрах
    
    public Kettle(string name, string brand, int power, double capacity) 
        : base(name, brand, power)
    {
        Capacity = capacity;
    }
    
    public override void Sound()
    {
        Console.WriteLine("Свист чайника: Фіііііі!");
    }
    
    public override void Show()
    {
        Console.WriteLine($"Чайник: {Name}");
    }
    
    public override void Desc()
    {
        base.Desc();
        Console.WriteLine($"Об'єм: {Capacity} л");
    }
}

// Похідний клас Microwave
class Microwave : Device
{
    public bool HasGrill { get; set; }
    
    public Microwave(string name, string brand, int power, bool hasGrill) 
        : base(name, brand, power)
    {
        HasGrill = hasGrill;
    }
    
    public override void Sound()
    {
        Console.WriteLine("Мікрохвильовка: Біп-біп!");
    }
    
    public override void Show()
    {
        Console.WriteLine($"Мікрохвильовка: {Name}");
    }
    
    public override void Desc()
    {
        base.Desc();
        Console.WriteLine($"Є гриль: {(HasGrill ? "Так" : "Ні")}");
    }
}

// Похідний клас Car
class Car : Device
{
    public int MaxSpeed { get; set; }
    
    public Car(string name, string brand, int power, int maxSpeed) 
        : base(name, brand, power * 1000) // переводимо к.с. у вати
    {
        MaxSpeed = maxSpeed;
    }
    
    public override void Sound()
    {
        Console.WriteLine("Автомобіль: Врум-врум!");
    }
    
    public override void Show()
    {
        Console.WriteLine($"Автомобіль: {Name}");
    }
    
    public override void Desc()
    {
        Console.WriteLine($"Опис: {Name}, бренд: {Brand}");
        Console.WriteLine($"Макс. швидкість: {MaxSpeed} км/год");
    }
}

// Похідний клас Steamboat
class Steamboat : Device
{
    public int PassengerCapacity { get; set; }
    
    public Steamboat(string name, string brand, int power, int passengers) 
        : base(name, brand, power)
    {
        PassengerCapacity = passengers;
    }
    
    public override void Sound()
    {
        Console.WriteLine("Пароплав: Ту-тууу!");
    }
    
    public override void Show()
    {
        Console.WriteLine($"Пароплав: {Name}");
    }
    
    public override void Desc()
    {
        base.Desc();
        Console.WriteLine($"Вміщує пасажирів: {PassengerCapacity}");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== РІЗНІ ПРИСТРОЇ ===\n");
        
        // Створюємо різні пристрої
        Device[] devices = new Device[4];
        
        devices[0] = new Kettle("Електричний чайник", "Philips", 2000, 1.7);
        devices[1] = new Microwave("Мікрохвильовка", "Samsung", 800, true);
        devices[2] = new Car("Tesla Model 3", "Tesla", 300, 220); // 300 к.с.
        devices[3] = new Steamboat("Титанік", "Harland & Wolff", 50000, 2435);
        
        // Демонструємо всі пристрої
        foreach (Device device in devices)
        {
            Console.WriteLine("====================");
            device.Show();
            device.Desc();
            Console.Write("Звук: ");
            device.Sound();
            Console.WriteLine();
        }
    }
}