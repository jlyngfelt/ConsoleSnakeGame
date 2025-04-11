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
                gameOver = true; // Avsluta spelet om Escape trycks på
                break;
        }
    }
}

private void Update()
{
    // Beräkna nästa position för ormens huvud baserat på nuvarande riktning
    (int headRow, int headCol) = snake[0]; // Nuvarande huvudposition
    (int newRow, int newCol) = (0, 0);     // Nästa huvudposition
    
    // Beräkna ny position baserat på riktning
    switch (currentDirection)
    {
        case Direction.Up:
            newRow = headRow - 1;
            newCol = headCol;
            break;
        case Direction.Down:
            newRow = headRow + 1;
            newCol = headCol;
            break;
        case Direction.Left:
            newRow = headRow;
            newCol = headCol - 1;
            break;
        case Direction.Right:
            newRow = headRow;
            newCol = headCol + 1;
            break;
    }
    
    // Kontrollera kollisioner
    if (CheckCollision(newRow, newCol))
    {
        gameOver = true;
        return;
    }
    
    // Kontrollera om ormen äter mat
    bool ateFood = (newRow == food.row && newCol == food.col);
    
    // Lägg till nytt huvud
    snake.Insert(0, (newRow, newCol));
    
    // Om ormen inte åt mat, .....(ta bort svansen)
    if (!ateFood)
    {
        // Ta bort sista segmentet (svansen)
        (int tailRow, int tailCol) = snake[snake.Count - 1];
        gameBoard[tailRow, tailCol] = EMPTY; // Rensa svansens position på spelplanen
        snake.RemoveAt(snake.Count - 1);
    }
    else
    {
        // Om ormen åt mat
        score++; // Öka poängen
        GenerateFood(); // Generera ny mat
        
        // Öka hastigheten lite (minska väntetiden)
        if (gameSpeed > 50)
        {
            gameSpeed -= 5;
        }
    }
    
    // Uppdatera spelplanen med ormens nya position
    UpdateGameBoard();
}

private bool CheckCollision(int row, int col)
{
    // Kontrollera om kollision händer med väggar
    if (gameBoard[row, col] == WALL)
    {
        return true;
    }
    
    // Kontrollera kollision med ormens kropp (förutom svansen som kommer att försvinna??)
    for (int i = 0; i < snake.Count - 1; i++)
    {
        if (snake[i].row == row && snake[i].col == col)
        {
            return true;
        }
    }
    
    return false;
}

private void UpdateGameBoard()
{
    // Rensa ormen från spelplanen (håll kvar väggar och mat)
    for (int row = 0; row < height; row++)
    {
        for (int col = 0; col < width; col++)
        {
            if (gameBoard[row, col] != WALL && gameBoard[row, col] != FOOD)
            {
                gameBoard[row, col] = EMPTY;
            }
        }
    }
    
    // Rita ormens kropp
    for (int i = 1; i < snake.Count; i++)
    {
        (int row, int col) = snake[i];
        gameBoard[row, col] = SNAKE_BODY;
    }
    
    // Rita ormens huvud
    (int headRow, int headCol) = snake[0];
    gameBoard[headRow, headCol] = SNAKE_HEAD;
}



private void Render()
{
    // Placera markören i övre vänstra hörnet, är inte detta default?
    Console.SetCursorPosition(0, 0);
    
    // Gör en string för hela spelplanen (tom) 
    string gameDisplay = "";
    
    // Lägg till poänginformation
    gameDisplay += $"Poäng: {score}\n";
    
    // Rendera spelplanen
    for (int row = 0; row < height; row++)
    {
        for (int col = 0; col < width; col++)
        {
            gameDisplay += gameBoard[row, col];
        }
        gameDisplay += "\n";
    }
    
    // Visa instruktioner
    gameDisplay += "Använd piltangenterna för att styra. ESC för att avsluta.";
    
    // Skriv ut hela spelplanen på en gång
    Console.Write(gameDisplay);
}
