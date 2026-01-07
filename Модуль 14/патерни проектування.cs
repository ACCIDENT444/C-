using System;
using System.Collections.Generic;
using System.Threading;

namespace SingletonAppConfig
{
  
    public sealed class AppConfig
    {
        // Приватне статичне поле для зберігання єдиного екземпляра
        private static AppConfig _instance;
        
        // Об'єкт для блокування потоку під час створення екземпляра
        private static readonly object _lock = new object();
        
        // Словник для зберігання налаштувань
        private readonly Dictionary<string, string> _settings;
        
       
        private AppConfig()
        {
            _settings = new Dictionary<string, string>();
            InitializeDefaultSettings();
        }
        
      
        public static AppConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new AppConfig();
                        }
                    }
                }
                return _instance;
            }
        }
        
      
        private void InitializeDefaultSettings()
        {
            _settings["AppName"] = "MyApplication";
            _settings["Version"] = "1.0.0";
            _settings["DefaultTheme"] = "Light";
            _settings["DefaultLanguage"] = "English";
            _settings["LogLevel"] = "Info";
            _settings["MaxConnections"] = "10";
        }
        
      
        public void SetSetting(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key), "Ключ налаштування не може бути null або порожнім");
            }
            
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "Значення налаштування не може бути null");
            }
            
            lock (_lock)
            {
                _settings[key] = value;
            }
        }
        
    
        public string GetSetting(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key), "Ключ налаштування не може бути null або порожнім");
            }
            
            lock (_lock)
            {
                return _settings.TryGetValue(key, out string value) ? value : null;
            }
        }
        
        
        public string GetSetting(string key, string defaultValue)
        {
            string value = GetSetting(key);
            return value ?? defaultValue;
        }
        
        
        public Dictionary<string, string> GetAllSettings()
        {
            lock (_lock)
            {
                return new Dictionary<string, string>(_settings);
            }
        }
        
       
        public bool RemoveSetting(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return false;
            }
            
            lock (_lock)
            {
                return _settings.Remove(key);
            }
        }
        
       
        public bool ContainsSetting(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return false;
            }
            
            lock (_lock)
            {
                return _settings.ContainsKey(key);
            }
        }
        
      
        public void ClearSettings()
        {
            lock (_lock)
            {
                _settings.Clear();
                InitializeDefaultSettings();
            }
        }
        
       
        public int SettingsCount
        {
            get
            {
                lock (_lock)
                {
                    return _settings.Count;
                }
            }
        }
        
      
        public override string ToString()
        {
            lock (_lock)
            {
                return $"AppConfig Settings (Count: {_settings.Count}): " + 
                       string.Join("; ", _settings);
            }
        }
    }
    
   
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ДЕМОНСТРАЦІЯ РОБОТИ КЛАСУ APPCONFIG ===\n");
            
            // Приклад 1: Базове використання
            Console.WriteLine("1. Базове використання:");
            Console.WriteLine("----------------------");
            
            // Отримання єдиного екземпляра
            AppConfig config = AppConfig.Instance;
            
            // Встановлення налаштувань
            config.SetSetting("Theme", "Dark");
            config.SetSetting("Language", "English");
            config.SetSetting("FontSize", "14");
            
            // Отримання налаштувань
            Console.WriteLine($"Theme: {config.GetSetting("Theme")}"); // Dark
            Console.WriteLine($"Language: {config.GetSetting("Language")}"); // English
            Console.WriteLine($"FontSize: {config.GetSetting("FontSize")}"); // 14
            
            // Приклад 2: Перевірка єдиного екземпляра
            Console.WriteLine("\n2. Перевірка Singleton:");
            Console.WriteLine("----------------------");
            
            // Отримання "іншого" екземпляра
            AppConfig config2 = AppConfig.Instance;
            
            // Перевірка, чи це той самий об'єкт
            bool isSameInstance = ReferenceEquals(config, config2);
            Console.WriteLine($"config і config2 - той самий екземпляр: {isSameInstance}");
            
            // Зміна налаштування через config2
            config2.SetSetting("Theme", "Blue");
            
            // Перевірка через config
            Console.WriteLine($"Theme через config: {config.GetSetting("Theme")}"); // Blue
            
            // Приклад 3: Обробка відсутніх налаштувань
            Console.WriteLine("\n3. Обробка відсутніх налаштувань:");
            Console.WriteLine("--------------------------------");
            
            string nonExistent = config.GetSetting("NonExistentKey");
            Console.WriteLine($"NonExistentKey: {(nonExistent ?? "null")}");
            
            // Зі значенням за замовчуванням
            string withDefault = config.GetSetting("NonExistentKey", "DefaultValue");
            Console.WriteLine($"NonExistentKey з defaultValue: {withDefault}");
            
            // Приклад 4: Додаткові методи
            Console.WriteLine("\n4. Додаткові методи:");
            Console.WriteLine("---------------------");
            
            // Перевірка існування
            Console.WriteLine($"Contains 'Theme': {config.ContainsSetting("Theme")}");
            Console.WriteLine($"Contains 'Invalid': {config.ContainsSetting("Invalid")}");
            
            // Кількість налаштувань
            Console.WriteLine($"Кількість налаштувань: {config.SettingsCount}");
            
            // Отримання всіх налаштувань
            var allSettings = config.GetAllSettings();
            Console.WriteLine("\nВсі налаштування:");
            foreach (var setting in allSettings)
            {
                Console.WriteLine($"  {setting.Key}: {setting.Value}");
            }
            
            // Приклад 5: Видалення та очищення
            Console.WriteLine("\n5. Видалення та очищення:");
            Console.WriteLine("------------------------");
            
            // Видалення налаштування
            bool removed = config.RemoveSetting("FontSize");
            Console.WriteLine($"FontSize видалено: {removed}");
            Console.WriteLine($"FontSize після видалення: {config.GetSetting("FontSize") ?? "null"}");
            
            // Очищення всіх налаштувань
            config.ClearSettings();
            Console.WriteLine($"\nПісля очищення:");
            Console.WriteLine($"Кількість налаштувань: {config.SettingsCount}");
            Console.WriteLine($"DefaultTheme: {config.GetSetting("DefaultTheme")}");
            
            // Приклад 6: Потокобезпечність
            Console.WriteLine("\n6. Тестування потокобезпечності:");
            Console.WriteLine("--------------------------------");
            
            TestThreadSafety();
            
            // Приклад 7: Обробка винятків
            Console.WriteLine("\n7. Обробка винятків:");
            Console.WriteLine("--------------------");
            
            try
            {
                config.SetSetting(null, "value");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Помилка при встановленні null ключа: {ex.Message}");
            }
            
            try
            {
                config.SetSetting("ValidKey", null);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Помилка при встановленні null значення: {ex.Message}");
            }
            
            Console.WriteLine("\n=== ДЕМОНСТРАЦІЯ ЗАВЕРШЕНА ===");
        }
        
        /// <summary>
        /// Тестування потокобезпечності Singleton
        /// </summary>
        static void TestThreadSafety()
        {
            const int threadCount = 10;
            var threads = new Thread[threadCount];
            var instances = new AppConfig[threadCount];
            
            // Створення та запуск потоків
            for (int i = 0; i < threadCount; i++)
            {
                int threadIndex = i;
                threads[i] = new Thread(() =>
                {
                    instances[threadIndex] = AppConfig.Instance;
                    instances[threadIndex].SetSetting($"ThreadKey_{threadIndex}", $"Value_{threadIndex}");
                });
            }
            
            // Запуск всіх потоків
            foreach (var thread in threads)
            {
                thread.Start();
            }
            
            // Очікування завершення всіх потоків
            foreach (var thread in threads)
            {
                thread.Join();
            }
            
            // Перевірка, чи всі посилання вказують на той самий об'єкт
            bool allSame = true;
            for (int i = 1; i < threadCount; i++)
            {
                if (!ReferenceEquals(instances[0], instances[i]))
                {
                    allSame = false;
                    break;
                }
            }
            
            Console.WriteLine($"Усі {threadCount} потоків отримали один і той самий екземпляр: {allSame}");
            
            // Перевірка налаштувань з різних потоків
            var testConfig = AppConfig.Instance;
            for (int i = 0; i < threadCount; i++)
            {
                string key = $"ThreadKey_{i}";
                string value = testConfig.GetSetting(key);
                Console.WriteLine($"{key}: {value}");
            }
        }
    }
    
  
    public sealed class AppConfigExtended : AppConfig
    {
      
    }
}
