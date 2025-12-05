using System;

// Базовий клас MusicalInstrument
class MusicalInstrument
{
    public string Name { get; set; }
    public string Material { get; set; }
    public int Year { get; set; }
    
    // Конструктор
    public MusicalInstrument(string name, string material, int year)
    {
        Name = name;
        Material = material;
        Year = year;
    }
    
    // Віртуальні методи
    public virtual void Sound()
    {
        Console.WriteLine("Інструмент грає");
    }
    
    public virtual void Show()
    {
        Console.WriteLine($"Назва інструменту: {Name}");
    }
    
    public virtual void Desc()
    {
        Console.WriteLine($"Опис: {Name}, матеріал: {Material}, рік: {Year}");
    }
    
    public virtual void History()
    {
        Console.WriteLine($"Це класичний музичний інструмент");
    }
}

// Похідний клас Violin
class Violin : MusicalInstrument
{
    public int StringCount { get; set; }
    
    public Violin(string name, string material, int year, int strings) 
        : base(name, material, year)
    {
        StringCount = strings;
    }
    
    public override void Sound()
    {
        Console.WriteLine("Скрипка: Вііі-іі-і!");
    }
    
    public override void Show()
    {
        Console.WriteLine($"Скрипка: {Name}");
    }
    
    public override void Desc()
    {
        base.Desc();
        Console.WriteLine($"Кількість струн: {StringCount}");
    }
    
    public override void History()
    {
        Console.WriteLine("Скрипка з'явилася в Італії у XVI столітті");
    }
}

// Похідний клас Trombone
class Trombone : MusicalInstrument
{
    public string Type { get; set; } // теноровий, басовий
    
    public Trombone(string name, string material, int year, string type) 
        : base(name, material, year)
    {
        Type = type;
    }
    
    public override void Sound()
    {
        Console.WriteLine("Тромбон: Ту-ту-ту!");
    }
    
    public override void Show()
    {
        Console.WriteLine($"Тромбон: {Name}");
    }
    
    public override void Desc()
    {
        base.Desc();
        Console.WriteLine($"Тип: {Type}");
    }
    
    public override void History()
    {
        Console.WriteLine("Тромбон з'явився у XV столітті");
    }
}

// Похідний клас Ukulele
class Ukulele : MusicalInstrument
{
    public string Size { get; set; } // сопрано, концертна, тенор, баритон
    
    public Ukulele(string name, string material, int year, string size) 
        : base(name, material, year)
    {
        Size = size;
    }
    
    public override void Sound()
    {
        Console.WriteLine("Укулеле: Брень-брень!");
    }
    
    public override void Show()
    {
        Console.WriteLine($"Укулеле: {Name}");
    }
    
    public override void Desc()
    {
        base.Desc();
        Console.WriteLine($"Розмір: {Size}");
    }
    
    public override void History()
    {
        Console.WriteLine("Укулеле прийшло з Гавайських островів у XIX столітті");
    }
}

// Похідний клас Cello
class Cello : MusicalInstrument
{
    public bool HasSpike { get; set; }
    
    public Cello(string name, string material, int year, bool hasSpike) 
        : base(name, material, year)
    {
        HasSpike = hasSpike;
    }
    
    public override void Sound()
    {
        Console.WriteLine("Віолончель: Глухо-гу-гу!");
    }
    
    public override void Show()
    {
        Console.WriteLine($"Віолончель: {Name}");
    }
    
    public override void Desc()
    {
        base.Desc();
        Console.WriteLine($"Д шпиль: {(HasSpike ? "Так" : "Ні")}");
    }
    
    public override void History()
    {
        Console.WriteLine("Віолончель розвинулася з інструменту viola da gamba");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== МУЗИЧНІ ІНСТРУМЕНТИ ===\n");
        
        // Створюємо масив інструментів
        MusicalInstrument[] instruments = new MusicalInstrument[4];
        
        instruments[0] = new Violin("Страдіварі", "Дерево", 1715, 4);
        instruments[1] = new Trombone("Басовий тромбон", "Мідь", 1950, "Басовий");
        instruments[2] = new Ukulele("Гавайська укулеле", "Дерево", 2020, "Сопрано");
        instruments[3] = new Cello("Професійна віолончель", "Ялина та клен", 1890, true);
        
        // Демонструємо всі інструменти
        foreach (MusicalInstrument instrument in instruments)
        {
            Console.WriteLine("====================");
            instrument.Show();
            instrument.Desc();
            Console.Write("Звук: ");
            instrument.Sound();
            Console.Write("Історія: ");
            instrument.History();
            Console.WriteLine();
        }
        
        // Показуємо поліморфізм
        Console.WriteLine("\n=== ПОКАЗ ПОЛІМОРФІЗМУ ===");
        Console.WriteLine("Ось як звучать усі інструменти разом:");
        
        foreach (MusicalInstrument instrument in instruments)
        {
            instrument.Sound();
        }
    }
}