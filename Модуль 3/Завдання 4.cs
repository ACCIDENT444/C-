// Клас Веб-сайт
class Website
{
    // Поля класу (приватні - доступ тільки через методи)
    private string name;
    private string url;
    private string description;
    private string ipAddress;
    
    // Метод для введення даних
    public void InputData()
    {
        Console.Write("Введіть назву сайту: ");
        name = Console.ReadLine();
        
        Console.Write("Введіть шлях до сайту (URL): ");
        url = Console.ReadLine();
        
        Console.Write("Введіть опис сайту: ");
        description = Console.ReadLine();
        
        Console.Write("Введіть IP-адресу сайту: ");
        ipAddress = Console.ReadLine();
    }
    
    // Метод для виведення даних
    public void OutputData()
    {
        Console.WriteLine("\n=== Інформація про сайт ===");
        Console.WriteLine($"Назва: {name}");
        Console.WriteLine($"URL: {url}");
        Console.WriteLine($"Опис: {description}");
        Console.WriteLine($"IP-адреса: {ipAddress}");
        Console.WriteLine("===========================\n");
    }
    
    // Методи для отримання даних з полів (геттери)
    public string GetName() { return name; }
    public string GetUrl() { return url; }
    public string GetDescription() { return description; }
    public string GetIpAddress() { return ipAddress; }
    
    // Методи для зміни даних в полях (сеттери)
    public void SetName(string newName) { name = newName; }
    public void SetUrl(string newUrl) { url = newUrl; }
    public void SetDescription(string newDescription) { description = newDescription; }
    public void SetIpAddress(string newIp) { ipAddress = newIp; }
}

// Головна частина програми
class Program
{
    static void Main()
    {
        // Створюємо об'єкт класу Website
        Website mySite = new Website();
        
        // Вводимо дані
        Console.WriteLine("=== Введення даних про сайт ===");
        mySite.InputData();
        
        // Виводимо дані
        mySite.OutputData();
        
        // Приклад роботи з методами доступу
        Console.WriteLine("=== Приклад роботи з методами ===");
        Console.WriteLine("Назва сайту: " + mySite.GetName());
        
        // Змінюємо назву
        mySite.SetName("Мій новий сайт");
        Console.WriteLine("Нова назва: " + mySite.GetName());
        
        // Виводимо оновлені дані
        mySite.OutputData();
    }
}