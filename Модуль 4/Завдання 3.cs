using System;
using System.Collections.Generic;

namespace MorseCodeTranslator
{
    class Program
    {
        // Словник для азбуки Морзе
        static Dictionary<char, string> morseCode = new Dictionary<char, string>()
        {
            {'A', ".-"}, {'B', "-..."}, {'C', "-.-."}, {'D', "-.."}, {'E', "."},
            {'F', "..-."}, {'G', "--."}, {'H', "...."}, {'I', ".."}, {'J', ".---"},
            {'K', "-.-"}, {'L', ".-.."}, {'M', "--"}, {'N', "-."}, {'O', "---"},
            {'P', ".--."}, {'Q', "--.-"}, {'R', ".-."}, {'S', "..."}, {'T', "-"},
            {'U', "..-"}, {'V', "...-"}, {'W', ".--"}, {'X', "-..-"}, {'Y', "-.--"},
            {'Z', "--.."}, {'0', "-----"}, {'1', ".----"}, {'2', "..---"}, {'3', "...--"},
            {'4', "....-"}, {'5', "....."}, {'6', "-...."}, {'7', "--..."}, {'8', "---.."},
            {'9', "----."}, {' ', "/"}, {'.', ".-.-.-"}, {',', "--..--"}, {'?', "..--.."}
        };
        
        static void Main()
        {
            Console.WriteLine("=== ПЕРЕКЛАДАЧ АЗБУКИ МОРЗЕ ===");
            
            while (true)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Перекласти текст в азбуку Морзе");
                Console.WriteLine("2. Вийти");
                Console.Write("Виберіть опцію: ");
                
                string choice = Console.ReadLine();
                
                if (choice == "1")
                {
                    TranslateToMorse();
                }
                else if (choice == "2")
                {
                    Console.WriteLine("До побачення!");
                    break;
                }
                else
                {
                    Console.WriteLine("Невірний вибір!");
                }
            }
        }
        
        static void TranslateToMorse()
        {
            Console.Write("\nВведіть текст для перекладу: ");
            string text = Console.ReadLine().ToUpper(); // Перетворюємо у великі літери
            
            Console.WriteLine("\nРезультат перекладу:");
            Console.WriteLine("======================");
            
            foreach (char character in text)
            {
                if (morseCode.ContainsKey(character))
                {
                    Console.Write(morseCode[character] + " ");
                }
                else
                {
                    Console.Write("? "); // Якщо символ не знайдено
                }
            }
            
            Console.WriteLine("\n======================");
            
            // Додатково показуємо розшифровку
            Console.WriteLine("\nРозшифровка:");
            foreach (char character in text)
            {
                if (character == ' ')
                {
                    Console.Write("[ПРОБІЛ] ");
                }
                else if (morseCode.ContainsKey(character))
                {
                    Console.Write($"{character}: {morseCode[character]}   ");
                }
            }
            Console.WriteLine();
        }
    }
}