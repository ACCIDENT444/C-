// Метод для перевірки чи є число паліндромом
static bool IsPalindrome(int number)
{
    int original = number;   // Зберігаємо оригінальне число
    int reversed = 0;        // Тут буде зворотнє число
    
    while (number > 0)
    {
        int lastDigit = number % 10;      // Беремо останню цифру
        reversed = reversed * 10 + lastDigit; // Додаємо її до зворотнього числа
        number = number / 10;             // Видаляємо останню цифру
    }
    
    return original == reversed; // Порівнюємо оригінал із зворотнім
}

// Головна частина програми
Console.Write("Введіть число для перевірки: ");
int num = int.Parse(Console.ReadLine());

if (IsPalindrome(num))
{
    Console.WriteLine($"{num} - це паліндром!");
}
else
{
    Console.WriteLine($"{num} - це НЕ паліндром!");
}