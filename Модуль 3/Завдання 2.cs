// Метод для фільтрації масиву
static int[] FilterArray(int[] originalArray, int[] filterArray)
{
    // Тимчасовий список для зберігання результату
    List<int> result = new List<int>();
    
    // Перевіряємо кожен елемент оригінального масиву
    for (int i = 0; i < originalArray.Length; i++)
    {
        bool shouldAdd = true;
        
        // Перевіряємо, чи є цей елемент у масиві для фільтрації
        for (int j = 0; j < filterArray.Length; j++)
        {
            if (originalArray[i] == filterArray[j])
            {
                shouldAdd = false; // Якщо знайшли - не додаємо
                break;
            }
        }
        
        // Якщо елемента немає у фільтрі - додаємо до результату
        if (shouldAdd)
        {
            result.Add(originalArray[i]);
        }
    }
    
    // Конвертуємо список назад у масив
    return result.ToArray();
}

// Головна частина програми
int[] original = { 1, 2, 6, -1, 88, 7, 6 };
int[] filter = { 6, 88, 7 };

Console.Write("Оригінальний масив: ");
foreach (int num in original)
{
    Console.Write(num + " ");
}

Console.Write("\nМасив для фільтрації: ");
foreach (int num in filter)
{
    Console.Write(num + " ");
}

int[] filtered = FilterArray(original, filter);

Console.Write("\nРезультат після фільтрації: ");
foreach (int num in filtered)
{
    Console.Write(num + " ");
}