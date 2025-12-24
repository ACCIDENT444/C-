using System;
using System.Collections;
using System.Collections.Generic;

namespace Module9
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Завдання 1: Generic метод Swap ===\n");
            
            // Тестування Swap для різних типів
            int a = 10, b = 20;
            Console.WriteLine($"До Swap: a = {a}, b = {b}");
            Swap(ref a, ref b);
            Console.WriteLine($"Після Swap: a = {a}, b = {b}");
            
            string str1 = "Hello", str2 = "World";
            Console.WriteLine($"\nДо Swap: str1 = {str1}, str2 = {str2}");
            Swap(ref str1, ref str2);
            Console.WriteLine($"Після Swap: str1 = {str1}, str2 = {str2}");
            
            double d1 = 3.14, d2 = 2.71;
            Console.WriteLine($"\nДо Swap: d1 = {d1}, d2 = {d2}");
            Swap(ref d1, ref d2);
            Console.WriteLine($"Після Swap: d1 = {d1}, d2 = {d2}");
            
            Console.WriteLine("\n=== Завдання 2: Generic клас PriorityQueue ===\n");
            
            // Тестування PriorityQueue
            PriorityQueue<string, int> priorityQueue = new PriorityQueue<string, int>();
            
            // Додавання елементів з різними пріоритетами
            priorityQueue.Enqueue("Низький пріоритет", 3);
            priorityQueue.Enqueue("Високий пріоритет", 1);
            priorityQueue.Enqueue("Середній пріоритет", 2);
            priorityQueue.Enqueue("Найвищий пріоритет", 0);
            
            Console.WriteLine($"Кількість елементів у черзі: {priorityQueue.Count}");
            Console.WriteLine($"Черга пуста? {priorityQueue.IsEmpty}");
            
            Console.WriteLine("\nЕлементи в порядку пріоритету:");
            while (!priorityQueue.IsEmpty)
            {
                var item = priorityQueue.Dequeue();
                Console.WriteLine($"{item.Value} (пріоритет: {item.Priority})");
            }
            
            Console.WriteLine($"\nПісля вилучення всіх елементів:");
            Console.WriteLine($"Кількість елементів: {priorityQueue.Count}");
            Console.WriteLine($"Черга пуста? {priorityQueue.IsEmpty}");
            
            // Тест з Clear
            priorityQueue.Enqueue("Тест 1", 1);
            priorityQueue.Enqueue("Тест 2", 2);
            Console.WriteLine($"\nКількість перед очищенням: {priorityQueue.Count}");
            priorityQueue.Clear();
            Console.WriteLine($"Кількість після очищення: {priorityQueue.Count}");
            
            Console.WriteLine("\n=== Завдання 3: Generic клас CircularQueue ===\n");
            
            // Тестування CircularQueue з різною ємністю
            CircularQueue<int> circularQueue = new CircularQueue<int>(5);
            
            Console.WriteLine($"Ємність черги: {circularQueue.Capacity}");
            Console.WriteLine($"Черга пуста? {circularQueue.IsEmpty}");
            Console.WriteLine($"Черга повна? {circularQueue.IsFull}");
            
            // Додаємо елементи
            for (int i = 1; i <= 5; i++)
            {
                circularQueue.Enqueue(i * 10);
                Console.WriteLine($"Додано: {i * 10}, Кількість: {circularQueue.Count}");
            }
            
            Console.WriteLine($"\nЧерга повна? {circularQueue.IsFull}");
            
            // Перевищення ємності (повинно викликати помилку)
            try
            {
                circularQueue.Enqueue(60);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Помилка при додаванні: {ex.Message}");
            }
            
            // Вилучаємо елементи
            Console.WriteLine("\nВилучення елементів:");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Вилучено: {circularQueue.Dequeue()}, Залишилось: {circularQueue.Count}");
            }
            
            // Додаємо нові елементи в звільнену пам'ять
            Console.WriteLine("\nДодавання нових елементів:");
            circularQueue.Enqueue(100);
            circularQueue.Enqueue(200);
            Console.WriteLine($"Додано 100 та 200, Кількість: {circularQueue.Count}");
            
            // Використання Peek
            Console.WriteLine($"\nНаступний елемент (Peek): {circularQueue.Peek()}");
            
            // Перегляд всіх елементів
            Console.WriteLine("\nВсі елементи у черзі:");
            foreach (var item in circularQueue)
            {
                Console.Write($"{item} ");
            }
            
            // Тест з очищенням
            Console.WriteLine($"\n\nКількість перед очищенням: {circularQueue.Count}");
            circularQueue.Clear();
            Console.WriteLine($"Кількість після очищення: {circularQueue.Count}");
            Console.WriteLine($"Черга пуста? {circularQueue.IsEmpty}");
        }
        
        // Завдання 1: Generic метод Swap
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }
    
    // Завдання 2: Generic клас PriorityQueue
    public class PriorityQueue<TValue, TPriority> where TPriority : IComparable<TPriority>
    {
        private class QueueItem
        {
            public TValue Value { get; }
            public TPriority Priority { get; }
            
            public QueueItem(TValue value, TPriority priority)
            {
                Value = value;
                Priority = priority;
            }
        }
        
        private List<QueueItem> items = new List<QueueItem>();
        
        public int Count => items.Count;
        public bool IsEmpty => Count == 0;
        
        public void Enqueue(TValue value, TPriority priority)
        {
            var newItem = new QueueItem(value, priority);
            items.Add(newItem);
            
            // Сортування за пріоритетом (найвищий пріоритет - перший)
            items.Sort((x, y) => x.Priority.CompareTo(y.Priority));
        }
        
        public QueueItem Dequeue()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Черга порожня");
            
            var item = items[0];
            items.RemoveAt(0);
            return item;
        }
        
        public QueueItem Peek()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Черга порожня");
            
            return items[0];
        }
        
        public void Clear()
        {
            items.Clear();
        }
    }
    
    // Завдання 3: Generic клас CircularQueue
    public class CircularQueue<T> : IEnumerable<T>
    {
        private T[] items;
        private int front;
        private int rear;
        private int count;
        
        public CircularQueue(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentException("Ємність повинна бути більше 0", nameof(capacity));
            
            items = new T[capacity];
            front = 0;
            rear = -1;
            count = 0;
        }
        
        public int Capacity => items.Length;
        public int Count => count;
        public bool IsEmpty => count == 0;
        public bool IsFull => count == Capacity;
        
        public void Enqueue(T item)
        {
            if (IsFull)
                throw new InvalidOperationException("Черга повна");
            
            rear = (rear + 1) % Capacity;
            items[rear] = item;
            count++;
        }
        
        public T Dequeue()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Черга порожня");
            
            T item = items[front];
            items[front] = default(T);
            front = (front + 1) % Capacity;
            count--;
            
            return item;
        }
        
        public T Peek()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Черга порожня");
            
            return items[front];
        }
        
        public void Clear()
        {
            Array.Clear(items, 0, items.Length);
            front = 0;
            rear = -1;
            count = 0;
        }
        
        // Реалізація інтерфейсу IEnumerable для підтримки foreach
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < count; i++)
            {
                int index = (front + i) % Capacity;
                yield return items[index];
            }
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}