using System;

namespace TicTacToeGame
{
    class Program
    {
        static char[,] board = new char[3, 3]; // Ігрове поле 3x3
        static Random random = new Random();
        
        static void Main()
        {
            Console.WriteLine("=== ГРА ХРЕСТИКИ-НОЛИКИ ===");
            Console.WriteLine("1. Грати з комп'ютером");
            Console.WriteLine("2. Грати з другом");
            Console.Write("Виберіть режим (1 або 2): ");
            
            int choice = int.Parse(Console.ReadLine());
            
            if (choice == 1)
            {
                PlayWithComputer();
            }
            else if (choice == 2)
            {
                PlayWithFriend();
            }
            else
            {
                Console.WriteLine("Невірний вибір!");
            }
        }
        
        // Гра з комп'ютером
        static void PlayWithComputer()
        {
            InitializeBoard();
            bool playerTurn = random.Next(2) == 0; // Випадково вибираємо хто перший
            
            if (playerTurn)
                Console.WriteLine("\nВи ходите першим (X)!");
            else
                Console.WriteLine("\nКомп'ютер ходить першим (O)!");
            
            while (true)
            {
                PrintBoard();
                
                if (playerTurn)
                {
                    PlayerMove('X');
                }
                else
                {
                    Console.WriteLine("\nХід комп'ютера...");
                    ComputerMove();
                }
                
                // Перевірка результатів
                if (CheckWin('X'))
                {
                    PrintBoard();
                    Console.WriteLine("\n🎉 ВИ ПЕРЕМОГЛИ! 🎉");
                    break;
                }
                else if (CheckWin('O'))
                {
                    PrintBoard();
                    Console.WriteLine("\n🤖 КОМП'ЮТЕР ПЕРЕМІГ!");
                    break;
                }
                else if (IsBoardFull())
                {
                    PrintBoard();
                    Console.WriteLine("\n🤝 НІЧИЯ!");
                    break;
                }
                
                playerTurn = !playerTurn; // Зміна гравця
            }
        }
        
        // Гра з другом
        static void PlayWithFriend()
        {
            InitializeBoard();
            bool firstPlayerTurn = random.Next(2) == 0;
            
            if (firstPlayerTurn)
                Console.WriteLine("\nГравець 1 (X) ходить першим!");
            else
                Console.WriteLine("\nГравець 2 (O) ходить першим!");
            
            char currentSymbol = firstPlayerTurn ? 'X' : 'O';
            
            while (true)
            {
                PrintBoard();
                Console.WriteLine($"\nХід гравця {(currentSymbol == 'X' ? "1 (X)" : "2 (O)")}");
                
                if (currentSymbol == 'X')
                    PlayerMove('X');
                else
                    PlayerMove('O');
                
                // Перевірка результатів
                if (CheckWin('X'))
                {
                    PrintBoard();
                    Console.WriteLine("\n🎉 ГРАВЕЦЬ 1 (X) ПЕРЕМІГ! 🎉");
                    break;
                }
                else if (CheckWin('O'))
                {
                    PrintBoard();
                    Console.WriteLine("\n🎉 ГРАВЕЦЬ 2 (O) ПЕРЕМІГ! 🎉");
                    break;
                }
                else if (IsBoardFull())
                {
                    PrintBoard();
                    Console.WriteLine("\n🤝 НІЧИЯ!");
                    break;
                }
                
                currentSymbol = (currentSymbol == 'X') ? 'O' : 'X'; // Зміна гравця
            }
        }
        
        // Ініціалізація поля
        static void InitializeBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = ' ';
                }
            }
        }
        
        // Виведення поля
        static void PrintBoard()
        {
            Console.WriteLine("\n  1 2 3");
            for (int i = 0; i < 3; i++)
            {
                Console.Write($"{i + 1} ");
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(board[i, j]);
                    if (j < 2) Console.Write("|");
                }
                Console.WriteLine();
                if (i < 2) Console.WriteLine("  -----");
            }
        }
        
        // Хід гравця
        static void PlayerMove(char symbol)
        {
            while (true)
            {
                Console.Write("Введіть рядок (1-3): ");
                int row = int.Parse(Console.ReadLine()) - 1;
                
                Console.Write("Введіть стовпець (1-3): ");
                int col = int.Parse(Console.ReadLine()) - 1;
                
                if (row >= 0 && row < 3 && col >= 0 && col < 3 && board[row, col] == ' ')
                {
                    board[row, col] = symbol;
                    break;
                }
                else
                {
                    Console.WriteLine("Невірний хід! Спробуйте ще раз.");
                }
            }
        }
        
        // Хід комп'ютера (простий)
        static void ComputerMove()
        {
            // Список вільних клітинок
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        System.Threading.Thread.Sleep(500); // Затримка для реалізму
                        board[i, j] = 'O';
                        return;
                    }
                }
            }
        }
        
        // Перевірка на перемогу
        static bool CheckWin(char symbol)
        {
            // Перевірка рядків
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == symbol && board[i, 1] == symbol && board[i, 2] == symbol)
                    return true;
            }
            
            // Перевірка стовпців
            for (int j = 0; j < 3; j++)
            {
                if (board[0, j] == symbol && board[1, j] == symbol && board[2, j] == symbol)
                    return true;
            }
            
            // Перевірка діагоналей
            if (board[0, 0] == symbol && board[1, 1] == symbol && board[2, 2] == symbol)
                return true;
            
            if (board[0, 2] == symbol && board[1, 1] == symbol && board[2, 0] == symbol)
                return true;
            
            return false;
        }
        
        // Перевірка на заповненість поля
        static bool IsBoardFull()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                        return false;
                }
            }
            return true;
        }
    }
}