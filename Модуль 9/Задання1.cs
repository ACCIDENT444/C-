using System;
using System.Collections.Generic;

namespace EmployeePasswordManager
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeePasswordManager manager = new EmployeePasswordManager();
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Система менеджменту співробітників і паролів ===\n");
                Console.WriteLine("1. Додати нового співробітника");
                Console.WriteLine("2. Видалити співробітника");
                Console.WriteLine("3. Змінити логін та пароль співробітника");
                Console.WriteLine("4. Отримати пароль співробітника");
                Console.WriteLine("5. Показати всіх співробітників");
                Console.WriteLine("6. Пошук співробітника");
                Console.WriteLine("7. Статистика");
                Console.WriteLine("8. Вихід");
                Console.Write("\nОберіть опцію: ");
                
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            AddEmployee(manager);
                            break;
                        case 2:
                            RemoveEmployee(manager);
                            break;
                        case 3:
                            UpdateEmployee(manager);
                            break;
                        case 4:
                            GetEmployeePassword(manager);
                            break;
                        case 5:
                            ShowAllEmployees(manager);
                            break;
                        case 6:
                            SearchEmployee(manager);
                            break;
                        case 7:
                            ShowStatistics(manager);
                            break;
                        case 8:
                            Console.WriteLine("\nДякую за використання програми!");
                            return;
                        default:
                            Console.WriteLine("\nНевірний вибір. Спробуйте ще раз.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\nБудь ласка, введіть число.");
                }
                
                Console.WriteLine("\nНатисніть будь-яку клавішу для продовження...");
                Console.ReadKey();
            }
        }
        
        static void AddEmployee(EmployeePasswordManager manager)
        {
            Console.WriteLine("\n--- Додавання нового співробітника ---");
            
            Console.Write("Введіть логін: ");
            string login = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(login))
            {
                Console.WriteLine("Логін не може бути порожнім.");
                return;
            }
            
            if (manager.EmployeeExists(login))
            {
                Console.WriteLine($"Співробітник з логіном '{login}' вже існує.");
                return;
            }
            
            Console.Write("Введіть пароль: ");
            string password = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Пароль не може бути порожнім.");
                return;
            }
            
            if (manager.AddEmployee(login, password))
            {
                Console.WriteLine($"Співробітник '{login}' успішно доданий.");
            }
            else
            {
                Console.WriteLine("Не вдалося додати співробітника.");
            }
        }
        
        static void RemoveEmployee(EmployeePasswordManager manager)
        {
            Console.WriteLine("\n--- Видалення співробітника ---");
            
            Console.Write("Введіть логін співробітника для видалення: ");
            string login = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(login))
            {
                Console.WriteLine("Логін не може бути порожнім.");
                return;
            }
            
            if (!manager.EmployeeExists(login))
            {
                Console.WriteLine($"Співробітник з логіном '{login}' не знайдений.");
                return;
            }
            
            Console.Write($"Ви впевнені, що хочете видалити співробітника '{login}'? (y/n): ");
            string confirmation = Console.ReadLine().ToLower();
            
            if (confirmation == "y" || confirmation == "так")
            {
                if (manager.RemoveEmployee(login))
                {
                    Console.WriteLine($"Співробітник '{login}' успішно видалений.");
                }
                else
                {
                    Console.WriteLine("Не вдалося видалити співробітника.");
                }
            }
            else
            {
                Console.WriteLine("Видалення скасовано.");
            }
        }
        
        static void UpdateEmployee(EmployeePasswordManager manager)
        {
            Console.WriteLine("\n--- Зміна даних співробітника ---");
            
            Console.Write("Введіть поточний логін співробітника: ");
            string oldLogin = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(oldLogin))
            {
                Console.WriteLine("Логін не може бути порожнім.");
                return;
            }
            
            if (!manager.EmployeeExists(oldLogin))
            {
                Console.WriteLine($"Співробітник з логіном '{oldLogin}' не знайдений.");
                return;
            }
            
            Console.Write("Введіть новий логін (залишіть порожнім, щоб не змінювати): ");
            string newLogin = Console.ReadLine();
            
            Console.Write("Введіть новий пароль (залишіть порожнім, щоб не змінювати): ");
            string newPassword = Console.ReadLine();
            
            bool updateLogin = !string.IsNullOrWhiteSpace(newLogin);
            bool updatePassword = !string.IsNullOrWhiteSpace(newPassword);
            
            if (!updateLogin && !updatePassword)
            {
                Console.WriteLine("Немає даних для оновлення.");
                return;
            }
            
            if (updateLogin && manager.EmployeeExists(newLogin) && newLogin != oldLogin)
            {
                Console.WriteLine($"Співробітник з логіном '{newLogin}' вже існує.");
                return;
            }
            
            if (manager.UpdateEmployee(oldLogin, newLogin, newPassword))
            {
                Console.WriteLine("Дані співробітника успішно оновлені.");
                if (updateLogin)
                {
                    Console.WriteLine($"Новий логін: {newLogin}");
                }
                if (updatePassword)
                {
                    Console.WriteLine("Пароль оновлено.");
                }
            }
            else
            {
                Console.WriteLine("Не вдалося оновити дані співробітника.");
            }
        }
        
        static void GetEmployeePassword(EmployeePasswordManager manager)
        {
            Console.WriteLine("\n--- Отримання пароля співробітника ---");
            
            Console.Write("Введіть логін співробітника: ");
            string login = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(login))
            {
                Console.WriteLine("Логін не може бути порожнім.");
                return;
            }
            
            string password = manager.GetPassword(login);
            
            if (password != null)
            {
                Console.WriteLine($"Пароль співробітника '{login}': {password}");
                
                Console.Write("\nПоказати пароль у замаскованому вигляді? (y/n): ");
                string showMasked = Console.ReadLine().ToLower();
                
                if (showMasked == "y" || showMasked == "так")
                {
                    Console.WriteLine($"Замаскований пароль: {new string('*', password.Length)}");
                }
            }
            else
            {
                Console.WriteLine($"Співробітник з логіном '{login}' не знайдений.");
            }
        }
        
        static void ShowAllEmployees(EmployeePasswordManager manager)
        {
            Console.WriteLine("\n--- Список всіх співробітників ---");
            
            var employees = manager.GetAllEmployees();
            
            if (employees.Count == 0)
            {
                Console.WriteLine("Немає зареєстрованих співробітників.");
                return;
            }
            
            int counter = 1;
            foreach (var employee in employees)
            {
                Console.WriteLine($"{counter}. Логін: {employee.Login}, Пароль: {new string('*', employee.Password.Length)}");
                counter++;
            }
        }
        
        static void SearchEmployee(EmployeePasswordManager manager)
        {
            Console.WriteLine("\n--- Пошук співробітника ---");
            
            Console.Write("Введіть логін або його частину для пошуку: ");
            string searchTerm = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                Console.WriteLine("Пошуковий запит не може бути порожнім.");
                return;
            }
            
            var results = manager.SearchEmployees(searchTerm);
            
            if (results.Count == 0)
            {
                Console.WriteLine("Співробітників за запитом не знайдено.");
                return;
            }
            
            Console.WriteLine($"\nЗнайдено {results.Count} співробітників:");
            int counter = 1;
            foreach (var employee in results)
            {
                Console.WriteLine($"{counter}. Логін: {employee.Login}, Пароль: {new string('*', employee.Password.Length)}");
                counter++;
            }
        }
        
        static void ShowStatistics(EmployeePasswordManager manager)
        {
            Console.WriteLine("\n--- Статистика ---");
            
            var stats = manager.GetStatistics();
            
            Console.WriteLine($"Загальна кількість співробітників: {stats.TotalEmployees}");
            
            if (stats.TotalEmployees > 0)
            {
                Console.WriteLine($"\nДовжина паролів:");
                Console.WriteLine($"- Середня довжина: {stats.AveragePasswordLength:F1} символів");
                Console.WriteLine($"- Найкоротший пароль: {stats.MinPasswordLength} символів");
                Console.WriteLine($"- Найдовший пароль: {stats.MaxPasswordLength} символів");
                
                if (stats.WeakPasswords > 0)
                {
                    Console.WriteLine($"\nУвага! Знайдено {stats.WeakPasswords} слабких паролів (менше 6 символів)");
                }
                
                Console.WriteLine($"\nПерші 5 співробітників за алфавітом:");
                foreach (var login in stats.FirstFiveLogins)
                {
                    Console.WriteLine($"- {login}");
                }
            }
        }
    }
    
    // Клас для представлення співробітника
    public class Employee
    {
        public string Login { get; set; }
        public string Password { get; set; }
        
        public Employee(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
    
    // Клас для статистики
    public class Statistics
    {
        public int TotalEmployees { get; set; }
        public double AveragePasswordLength { get; set; }
        public int MinPasswordLength { get; set; }
        public int MaxPasswordLength { get; set; }
        public int WeakPasswords { get; set; }
        public List<string> FirstFiveLogins { get; set; }
    }
    
    // Основний клас для менеджменту співробітників
    public class EmployeePasswordManager
    {
        // Dictionary для зберігання логінів та паролів
        private Dictionary<string, string> employees = new Dictionary<string, string>();
        
        // Додавання нового співробітника
        public bool AddEmployee(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }
            
            if (employees.ContainsKey(login))
            {
                return false;
            }
            
            employees[login] = password;
            return true;
        }
        
        // Видалення співробітника
        public bool RemoveEmployee(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                return false;
            }
            
            return employees.Remove(login);
        }
        
        // Оновлення даних співробітника
        public bool UpdateEmployee(string oldLogin, string newLogin, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(oldLogin) || !employees.ContainsKey(oldLogin))
            {
                return false;
            }
            
            // Якщо вказаний новий логін і він відрізняється від старого
            if (!string.IsNullOrWhiteSpace(newLogin) && newLogin != oldLogin)
            {
                // Перевіряємо, чи не існує вже такий логін
                if (employees.ContainsKey(newLogin))
                {
                    return false;
                }
                
                // Отримуємо старий пароль
                string password = employees[oldLogin];
                
                // Видаляємо старий запис
                employees.Remove(oldLogin);
                
                // Додаємо новий запис
                if (!string.IsNullOrWhiteSpace(newPassword))
                {
                    password = newPassword;
                }
                
                employees[newLogin] = password;
                return true;
            }
            // Якщо лише змінюємо пароль
            else if (!string.IsNullOrWhiteSpace(newPassword))
            {
                employees[oldLogin] = newPassword;
                return true;
            }
            
            return false;
        }
        
        // Отримання пароля за логіном
        public string GetPassword(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                return null;
            }
            
            return employees.TryGetValue(login, out string password) ? password : null;
        }
        
        // Перевірка існування співробітника
        public bool EmployeeExists(string login)
        {
            return !string.IsNullOrWhiteSpace(login) && employees.ContainsKey(login);
        }
        
        // Отримання всіх співробітників
        public List<Employee> GetAllEmployees()
        {
            var result = new List<Employee>();
            
            foreach (var kvp in employees)
            {
                result.Add(new Employee(kvp.Key, kvp.Value));
            }
            
            return result;
        }
        
        // Пошук співробітників
        public List<Employee> SearchEmployees(string searchTerm)
        {
            var result = new List<Employee>();
            
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return result;
            }
            
            foreach (var kvp in employees)
            {
                if (kvp.Key.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(new Employee(kvp.Key, kvp.Value));
                }
            }
            
            return result;
        }
        
        // Отримання статистики
        public Statistics GetStatistics()
        {
            var stats = new Statistics
            {
                TotalEmployees = employees.Count,
                FirstFiveLogins = new List<string>()
            };
            
            if (employees.Count == 0)
            {
                stats.AveragePasswordLength = 0;
                stats.MinPasswordLength = 0;
                stats.MaxPasswordLength = 0;
                stats.WeakPasswords = 0;
                return stats;
            }
            
            int totalLength = 0;
            int minLength = int.MaxValue;
            int maxLength = int.MinValue;
            int weakCount = 0;
            
            foreach (var password in employees.Values)
            {
                int length = password.Length;
                totalLength += length;
                
                if (length < minLength) minLength = length;
                if (length > maxLength) maxLength = length;
                
                if (length < 6) weakCount++;
            }
            
            stats.AveragePasswordLength = (double)totalLength / employees.Count;
            stats.MinPasswordLength = minLength;
            stats.MaxPasswordLength = maxLength;
            stats.WeakPasswords = weakCount;
            
            // Отримуємо перші 5 логінів за алфавітом
            var sortedLogins = new List<string>(employees.Keys);
            sortedLogins.Sort();
            
            for (int i = 0; i < Math.Min(5, sortedLogins.Count); i++)
            {
                stats.FirstFiveLogins.Add(sortedLogins[i]);
            }
            
            return stats;
        }
        
        // Метод для отримання всіх записів (для тестування)
        public Dictionary<string, string> GetAllEntries()
        {
            return new Dictionary<string, string>(employees);
        }
        
        // Кількість співробітників
        public int Count => employees.Count;
    }
}