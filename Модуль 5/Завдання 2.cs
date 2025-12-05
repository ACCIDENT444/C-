using System;

class Journal
{
    // Властивості (замість полів)
    public string Name { get; set; }
    public int Year { get; set; }
    public int EmployeesCount { get; set; } // Нова властивість
    
    // Конструктор
    public Journal(string name, int year, int employees)
    {
        Name = name;
        Year = year;
        EmployeesCount = employees;
    }
    
    // Виведення інформації
    public void PrintInfo()
    {
        Console.WriteLine($"Журнал: {Name}");
        Console.WriteLine($"Рік заснування: {Year}");
        Console.WriteLine($"Кількість співробітників: {EmployeesCount}");
        Console.WriteLine();
    }
    
    // Перевантаження оператора + (збільшення співробітників)
    public static Journal operator +(Journal journal, int number)
    {
        journal.EmployeesCount += number;
        return journal;
    }
    
    // Перевантаження оператора - (зменшення співробітників)
    public static Journal operator -(Journal journal, int number)
    {
        journal.EmployeesCount -= number;
        if (journal.EmployeesCount < 0) journal.EmployeesCount = 0;
        return journal;
    }
    
    // Перевантаження == (перевірка на рівність кількості співробітників)
    public static bool operator ==(Journal j1, Journal j2)
    {
        return j1.EmployeesCount == j2.EmployeesCount;
    }
    
    // Перевантаження != (перевірка на нерівність)
    public static bool operator !=(Journal j1, Journal j2)
    {
        return j1.EmployeesCount != j2.EmployeesCount;
    }
    
    // Перевантаження > (більше співробітників)
    public static bool operator >(Journal j1, Journal j2)
    {
        return j1.EmployeesCount > j2.EmployeesCount;
    }
    
    // Перевантаження < (менше співробітників)
    public static bool operator <(Journal j1, Journal j2)
    {
        return j1.EmployeesCount < j2.EmployeesCount;
    }
    
    // Перевизначення методу Equals
    public override bool Equals(object obj)
    {
        if (obj is Journal otherJournal)
        {
            return this.EmployeesCount == otherJournal.EmployeesCount;
        }
        return false;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== ПРИКЛАД РОБОТИ З ЖУРНАЛАМИ ===");
        
        // Створюємо журнали
        Journal journal1 = new Journal("Наука", 1995, 15);
        Journal journal2 = new Journal("Техніка", 2000, 10);
        
        Console.WriteLine("Початкові дані:");
        journal1.PrintInfo();
        journal2.PrintInfo();
        
        // Використання перевантажених операторів
        Console.WriteLine("Після змін:");
        journal1 = journal1 + 5; // Додаємо 5 співробітників
        journal2 = journal2 - 3; // Віднімаємо 3 співробітники
        
        journal1.PrintInfo();
        journal2.PrintInfo();
        
        // Перевірка операторів порівняння
        Console.WriteLine("Порівняння журналів:");
        Console.WriteLine($"journal1 == journal2: {journal1 == journal2}");
        Console.WriteLine($"journal1 != journal2: {journal1 != journal2}");
        Console.WriteLine($"journal1 > journal2: {journal1 > journal2}");
        Console.WriteLine($"journal1 < journal2: {journal1 < journal2}");
        
        // Перевірка методу Equals
        Console.WriteLine($"journal1.Equals(journal2): {journal1.Equals(journal2)}");
    }
}