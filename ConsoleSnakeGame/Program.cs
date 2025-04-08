using System;
using System.Collections.Generic;
using System.Threading;

namespace CoolSnakeGame
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            
            //logik för när spelet körs
            
            //logik för när man förlorar
        }
    }

    class Game
    {
        //spelplan 
        private int width = 30;
        private int height = 30;
        private char[,] gameBoard; //2D arrayen
        
        private List<(int row, int col)> snake;
        private enum Direction { Up, Down, Left, Right }
        private Direction currentDirection;
        
        private (int row, int col) food;
        
        private bool gameOver;
        private int score;
        
        private int gameSpeed = 200;
        
        private const char EMPTY = ' ';
        private const char WALL = '#';
        private const char SNAKE_BODY = 'O';
        private const char SNAKE_HEAD = '@';
        private const char FOOD = '*';
    }
    
    public Game()
    {
        Initialize();
    }

    private void Initialize()
    {
        gameBoard = new char[height, width];

        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                // Om det är en kant, placera en vägg
                if (row == 0 || row == height - 1 || col == 0 || col == width - 1)
                {
                    gameBoard[row, col] = WALL;
                }
                else
                {
                    gameBoard[row, col] = EMPTY;
                }
            }
        }

        // Placera ormen i mitten av spelplanen
        snake = new List<(int row, int col)>();
        int startRow = height / 2;
        int startCol = width / 2;

        // Skapa ormen med 3 delar
        snake.Add((startRow, startCol)); // Huvud
        snake.Add((startRow, startCol - 1)); // Kropp
        snake.Add((startRow, startCol - 2)); // Svans

        // Sätt initial riktning
        currentDirection = Direction.Right;

        // Sätt spelet till aktivt
        gameOver = false;
        score = 0;

        // Generera första maten
        GenerateFood();
    }
}

private void GenerateFood()
{
    Random random = new Random();
    int foodRow, foodCol;
    
    do
    {
        // Generera slumpmässiga koordinater (undvik väggarna)
        foodRow = random.Next(1, height - 1);
        foodCol = random.Next(1, width - 1);
        
        // Kontrollera om positionen är tom (inte innehåller någon del av ormen)
    } while (snake.Contains((foodRow, foodCol)));
    
    // Placera maten på spelplanen med hjälp av random
    food = (foodRow, foodCol);
    gameBoard[foodRow, foodCol] = FOOD;
}

private void ProcessInput()
{
    // Kontrollera om det finns någon tangent att läsa 
    if (Console.KeyAvailable)
    {
        // Läs tangenten utan att visa den på skärmen
        var key = Console.ReadKey(true).Key;
        
        // Uppdatera riktningen baserat på tangenttryck
        // (Se till att ormen inte kan vända helt om)
        switch (key)
        {
            case ConsoleKey.UpArrow:
                if (currentDirection != Direction.Down)
                    currentDirection = Direction.Up;
                break;
            case ConsoleKey.DownArrow:
                if (currentDirection != Direction.Up)
                    currentDirection = Direction.Down;
                break;
            case ConsoleKey.LeftArrow:
                if (currentDirection != Direction.Right)
                    currentDirection = Direction.Left;
                break;
            case ConsoleKey.RightArrow:
                if (currentDirection != Direction.Right)
                    currentDirection = Direction.Right;
                break;
            case ConsoleKey.Escape:
                gameOver = true; // Avsluta spelet om Escape trycks
                break;
        }
    }
}