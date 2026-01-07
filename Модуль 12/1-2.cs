using System;
using System.Collections.Generic;
using System.Linq;

// Користувацький тип "Фірма"
public class Company
{
    public string Name { get; set; }
    public DateTime FoundingDate { get; set; }
    public string BusinessProfile { get; set; }
    public string DirectorFullName { get; set; }
    public int EmployeeCount { get; set; }
    public string Address { get; set; }
}

class Program
{
    static void Main()
    {
        // Створення масиву фірм
        List<Company> companies = new List<Company>
        {
            new Company
            {
                Name = "FoodExpress Ltd",
                FoundingDate = new DateTime(2020, 5, 15),
                BusinessProfile = "Marketing",
                DirectorFullName = "John White",
                EmployeeCount = 150,
                Address = "London, UK"
            },
            new Company
            {
                Name = "TechSolutions Inc",
                FoundingDate = new DateTime(2022, 3, 10),
                BusinessProfile = "IT",
                DirectorFullName = "Sarah Black",
                EmployeeCount = 80,
                Address = "Kyiv, Ukraine"
            },
            new Company
            {
                Name = "White Marketing Group",
                FoundingDate = new DateTime(2019, 11, 25),
                BusinessProfile = "Marketing",
                DirectorFullName = "Michael White",
                EmployeeCount = 250,
                Address = "London, UK"
            },
            new Company
            {
                Name = "HealthyFood Corp",
                FoundingDate = DateTime.Now.AddDays(-123), // Рівно 123 дні тому
                BusinessProfile = "Food Industry",
                DirectorFullName = "Robert Green",
                EmployeeCount = 350,
                Address = "Berlin, Germany"
            },
            new Company
            {
                Name = "Black & White IT",
                FoundingDate = new DateTime(2021, 8, 5),
                BusinessProfile = "IT",
                DirectorFullName = "James Black",
                EmployeeCount = 120,
                Address = "Paris, France"
            }
        };

        Console.WriteLine("=== ЗАПИТИ LINQ (Синтаксис запитів) ===\n");

        // 1. Отримати інформацію про всі фірми
        var allCompanies = from c in companies
                           select c;
        Console.WriteLine("1. Всі фірми:");
        foreach (var c in allCompanies)
            Console.WriteLine($"- {c.Name}, {c.Address}");

        // 2. Фірми зі словом "Food" у назві
        var foodCompanies = from c in companies
                            where c.Name.Contains("Food")
                            select c;
        Console.WriteLine("\n2. Фірми зі словом 'Food' у назві:");
        foreach (var c in foodCompanies)
            Console.WriteLine($"- {c.Name}");

        // 3. Фірми у галузі маркетингу
        var marketingCompanies = from c in companies
                                 where c.BusinessProfile == "Marketing"
                                 select c;
        Console.WriteLine("\n3. Фірми у галузі маркетингу:");
        foreach (var c in marketingCompanies)
            Console.WriteLine($"- {c.Name}");

        // 4. Фірми у галузі маркетингу або IT
        var marketingOrIT = from c in companies
                            where c.BusinessProfile == "Marketing" || c.BusinessProfile == "IT"
                            select c;
        Console.WriteLine("\n4. Фірми у галузі маркетингу або IT:");
        foreach (var c in marketingOrIT)
            Console.WriteLine($"- {c.Name} ({c.BusinessProfile})");

        // 5. Фірми з кількістю співробітників > 100
        var over100Employees = from c in companies
                               where c.EmployeeCount > 100
                               select c;
        Console.WriteLine("\n5. Фірми з >100 співробітниками:");
        foreach (var c in over100Employees)
            Console.WriteLine($"- {c.Name}: {c.EmployeeCount} співроб.");

        // 6. Фірми з кількістю співробітників 100-300
        var between100and300 = from c in companies
                               where c.EmployeeCount >= 100 && c.EmployeeCount <= 300
                               select c;
        Console.WriteLine("\n6. Фірми з 100-300 співробітників:");
        foreach (var c in between100and300)
            Console.WriteLine($"- {c.Name}: {c.EmployeeCount} співроб.");

        // 7. Фірми в Лондоні
        var londonCompanies = from c in companies
                              where c.Address.Contains("London")
                              select c;
        Console.WriteLine("\n7. Фірми в Лондоні:");
        foreach (var c in londonCompanies)
            Console.WriteLine($"- {c.Name}");

        // 8. Фірми з прізвищем директора White
        var whiteDirector = from c in companies
                            where c.DirectorFullName.Contains("White")
                            select c;
        Console.WriteLine("\n8. Фірми з директором White:");
        foreach (var c in whiteDirector)
            Console.WriteLine($"- {c.Name}: {c.DirectorFullName}");

        // 9. Фірми, засновані більше 2 років тому
        var twoYearsAgo = DateTime.Now.AddYears(-2);
        var olderThan2Years = from c in companies
                              where c.FoundingDate < twoYearsAgo
                              select c;
        Console.WriteLine("\n9. Фірми, засновані більше 2 років тому:");
        foreach (var c in olderThan2Years)
            Console.WriteLine($"- {c.Name}: {c.FoundingDate:yyyy-MM-dd}");

        // 10. Фірми, засновані рівно 123 дні тому
        var exactly123Days = from c in companies
                             where (DateTime.Now - c.FoundingDate).Days == 123
                             select c;
        Console.WriteLine("\n10. Фірми, засновані 123 дні тому:");
        foreach (var c in exactly123Days)
            Console.WriteLine($"- {c.Name}: {c.FoundingDate:yyyy-MM-dd}");

        // 11. Фірми: директор Black і назва містить White
        var blackWhite = from c in companies
                         where c.DirectorFullName.Contains("Black") && c.Name.Contains("White")
                         select c;
        Console.WriteLine("\n11. Фірми: директор Black і назва містить White:");
        foreach (var c in blackWhite)
            Console.WriteLine($"- {c.Name}, директор: {c.DirectorFullName}");

        // =================================================================
        // ЗАВДАННЯ 2: Ті самі запити через методи розширень
        // =================================================================

        Console.WriteLine("\n\n=== ЗАПИТИ LINQ (Методи розширень) ===\n");

        // 1. Всі фірми
        var allCompaniesExt = companies.Select(c => c);
        Console.WriteLine("1. Всі фірми:");
        foreach (var c in allCompaniesExt)
            Console.WriteLine($"- {c.Name}, {c.Address}");

        // 2. Фірми зі словом "Food" у назві
        var foodCompaniesExt = companies.Where(c => c.Name.Contains("Food"));
        Console.WriteLine("\n2. Фірми зі словом 'Food' у назві:");
        foreach (var c in foodCompaniesExt)
            Console.WriteLine($"- {c.Name}");

        // 3. Фірми у галузі маркетингу
        var marketingCompaniesExt = companies.Where(c => c.BusinessProfile == "Marketing");
        Console.WriteLine("\n3. Фірми у галузі маркетингу:");
        foreach (var c in marketingCompaniesExt)
            Console.WriteLine($"- {c.Name}");

        // 4. Фірми у галузі маркетингу або IT
        var marketingOrITExt = companies.Where(c => c.BusinessProfile == "Marketing" || c.BusinessProfile == "IT");
        Console.WriteLine("\n4. Фірми у галузі маркетингу або IT:");
        foreach (var c in marketingOrITExt)
            Console.WriteLine($"- {c.Name} ({c.BusinessProfile})");

        // 5. Фірми з кількістю співробітників > 100
        var over100EmployeesExt = companies.Where(c => c.EmployeeCount > 100);
        Console.WriteLine("\n5. Фірми з >100 співробітниками:");
        foreach (var c in over100EmployeesExt)
            Console.WriteLine($"- {c.Name}: {c.EmployeeCount} співроб.");

        // 6. Фірми з кількістю співробітників 100-300
        var between100and300Ext = companies.Where(c => c.EmployeeCount >= 100 && c.EmployeeCount <= 300);
        Console.WriteLine("\n6. Фірми з 100-300 співробітників:");
        foreach (var c in between100and300Ext)
            Console.WriteLine($"- {c.Name}: {c.EmployeeCount} співроб.");

        // 7. Фірми в Лондоні
        var londonCompaniesExt = companies.Where(c => c.Address.Contains("London"));
        Console.WriteLine("\n7. Фірми в Лондоні:");
        foreach (var c in londonCompaniesExt)
            Console.WriteLine($"- {c.Name}");

        // 8. Фірми з прізвищем директора White
        var whiteDirectorExt = companies.Where(c => c.DirectorFullName.Contains("White"));
        Console.WriteLine("\n8. Фірми з директором White:");
        foreach (var c in whiteDirectorExt)
            Console.WriteLine($"- {c.Name}: {c.DirectorFullName}");

        // 9. Фірми, засновані більше 2 років тому
        var olderThan2YearsExt = companies.Where(c => c.FoundingDate < twoYearsAgo);
        Console.WriteLine("\n9. Фірми, засновані більше 2 років тому:");
        foreach (var c in olderThan2YearsExt)
            Console.WriteLine($"- {c.Name}: {c.FoundingDate:yyyy-MM-dd}");

        // 10. Фірми, засновані рівно 123 дні тому
        var exactly123DaysExt = companies.Where(c => (DateTime.Now - c.FoundingDate).Days == 123);
        Console.WriteLine("\n10. Фірми, засновані 123 дні тому:");
        foreach (var c in exactly123DaysExt)
            Console.WriteLine($"- {c.Name}: {c.FoundingDate:yyyy-MM-dd}");

        // 11. Фірми: директор Black і назва містить White
        var blackWhiteExt = companies.Where(c => c.DirectorFullName.Contains("Black") && c.Name.Contains("White"));
        Console.WriteLine("\n11. Фірми: директор Black і назва містить White:");
        foreach (var c in blackWhiteExt)
            Console.WriteLine($"- {c.Name}, директор: {c.DirectorFullName}");
    }
}