using System;
using System.Collections.Generic;

// Інтерфейс 1
interface ICalc
{
    int Less(int valueToCompare);
    int Greater(int valueToCompare);
}

// Інтерфейс 2
interface IOutput2
{
    void ShowEven();
    void ShowOdd();
}

// Інтерфейс 3
interface ICalc2
{
    int CountDistinct();
    int EqualToValue(int valueToCompare);
}

// Клас Array, який імплементує всі інтерфейси
class MyArray : ICalc, IOutput2, ICalc2
{
    private int[] array;
    
    // Конструктор
    public MyArray(int[] arr)
    {
        array = arr;
    }
    
    // Метод для виведення всього масиву
    public void PrintArray()
    {
        Console.Write("Масив: [ ");
        foreach (int num in array)
        {
            Console.Write(num + " ");
        }
        Console.WriteLine("]");
    }
    
    // === Імплементація ICalc ===
    public int Less(int valueToCompare)
    {
        int count = 0;
        foreach (int num in array)
        {
            if (num < valueToCompare)
                count++;
        }
        return count;
    }
    
    public int Greater(int valueToCompare)
    {
        int count = 0;
        foreach (int num in array)
        {
            if (num > valueToCompare)
                count++;
        }
        return count;
    }
    
    // === Імплементація IOutput2 ===
    public void ShowEven()
    {
        Console.Write("Парні числа: ");
        foreach (int num in array)
        {
            if (num % 2 == 0)
                Console.Write(num + " ");
        }
        Console.WriteLine();
    }
    
    public void ShowOdd()
    {
        Console.Write("Непарні числа: ");
        foreach (int num in array)
        {
            if (num % 2 != 0)
                Console.Write(num + " ");
        }
        Console.WriteLine();
    }
    
    // === Імплементація ICalc2 ===
    public int CountDistinct()
    {
        // Використовуємо список для зберігання унікальних значень
        List<int> unique = new List<int>();
        
        foreach (int num in array)
        {
            if (!unique.Contains(num))
                unique.Add(num);
        }
        
        return unique.Count;
    }
    
    public int EqualToValue(int valueToCompare)
    {
        int count = 0;
        foreach (int num in array)
        {
            if (num == valueToCompare)
                count++;
        }
        return count;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== ТЕСТУВАННЯ КЛАСУ ARRAY З ІНТЕРФЕЙСАМИ ===\n");
        
        // Створюємо масив для тестування
        int[] testArray = { 5, 2, 8, 2, 5, 7, 3, 8, 1, 4 };
        MyArray myArray = new MyArray(testArray);
        
        // Показуємо початковий масив
        myArray.PrintArray();
        Console.WriteLine();
        
        // === Тестування ICalc ===
        Console.WriteLine("=== ІНТЕРФЕЙС ICalc ===");
        int compareValue = 5;
        Console.WriteLine($"Порівняння з числом {compareValue}:");
        Console.WriteLine($"Менших за {compareValue}: {myArray.Less(compareValue)}");
        Console.WriteLine($"Більших за {compareValue}: {myArray.Greater(compareValue)}");
        Console.WriteLine();
        
        // === Тестування IOutput2 ===
        Console.WriteLine("=== ІНТЕРФЕЙС IOutput2 ===");
        myArray.ShowEven();
        myArray.ShowOdd();
        Console.WriteLine();
        
        // === Тестування ICalc2 ===
        Console.WriteLine("=== ІНТЕРФЕЙС ICalc2 ===");
        Console.WriteLine($"Унікальних значень: {myArray.CountDistinct()}");
        
        int valueToFind = 2;
        Console.WriteLine($"Значень рівних {valueToFind}: {myArray.EqualToValue(valueToFind)}");
        
        valueToFind = 8;
        Console.WriteLine($"Значень рівних {valueToFind}: {myArray.EqualToValue(valueToFind)}");
        
        valueToFind = 10;
        Console.WriteLine($"Значень рівних {valueToFind}: {myArray.EqualToValue(valueToFind)}");
        
        // === Додатковий тест з іншим масивом ===
        Console.WriteLine("\n=== ДОДАТКОВИЙ ТЕСТ ===");
        int[] anotherArray = { 1, 1, 1, 2, 2, 3 };
        MyArray array2 = new MyArray(anotherArray);
        
        array2.PrintArray();
        Console.WriteLine($"Унікальних значень: {array2.CountDistinct()}");
        Console.WriteLine($"Значень рівних 1: {array2.EqualToValue(1)}");
        
        // === Тест поліморфізму з інтерфейсами ===
        Console.WriteLine("\n=== ПОЛІМОРФІЗМ З ІНТЕРФЕЙСАМИ ===");
        
        // Можна створити змінну типу інтерфейсу
        ICalc calcInterface = myArray;
        Console.WriteLine($"Через ICalc: менших за 5 = {calcInterface.Less(5)}");
        
        IOutput2 outputInterface = myArray;
        Console.Write("Через IOutput2 парні: ");
        outputInterface.ShowEven();
    }
}