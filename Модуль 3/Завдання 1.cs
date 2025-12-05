// Метод для малювання квадрата
static void DrawSquare(int sideLength, char symbol)
{
    for (int i = 0; i < sideLength; i++)
    {
        for (int j = 0; j < sideLength; j++)
        {
            Console.Write(symbol + " ");
        }
        Console.WriteLine();
    }
}

// Головна частина програми
Console.Write("Введіть довжину сторони квадрата: ");
int length = int.Parse(Console.ReadLine());

Console.Write("Введіть символ: ");
char symbol = Console.ReadLine()[0];

Console.WriteLine();
DrawSquare(length, symbol);