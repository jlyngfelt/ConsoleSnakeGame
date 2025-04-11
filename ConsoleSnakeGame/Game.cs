using System;
using System.Threading;

namespace CoolSnakeGame
{
    public class Game
    {
        private GameBoard gameBoard;
        private Snake snake;
        private FoodGenerator foodGenerator;
        
        private (int row, int col) food;
        private bool gameOver;
        private int score;
        private int gameSpeed = 200;

        public Game()
        {
            Initialize();
        }

        private void Initialize()
        {
            int width = 30;
            int height = 20;
            
            gameBoard = new GameBoard(width, height);
            snake = new Snake(height / 2, width / 2);
            foodGenerator = new FoodGenerator(height, width);
            
            gameOver = false;
            score = 0;
            
            GenerateFood();
        }

        private void GenerateFood()
        {
            food = foodGenerator.GenerateFood(snake.Body);
            gameBoard.PlaceFood(food);
        }

        private void ProcessInput()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (snake.CurrentDirection != Snake.Direction.Down)
                            snake.CurrentDirection = Snake.Direction.Up;
                        break;
                    case ConsoleKey.DownArrow:
                        if (snake.CurrentDirection != Snake.Direction.Up)
                            snake.CurrentDirection = Snake.Direction.Down;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (snake.CurrentDirection != Snake.Direction.Right)
                            snake.CurrentDirection = Snake.Direction.Left;
                        break;
                    case ConsoleKey.RightArrow:
                        if (snake.CurrentDirection != Snake.Direction.Left)
                            snake.CurrentDirection = Snake.Direction.Right;
                        break;
                    case ConsoleKey.Escape:
                        gameOver = true;
                        break;
                }
            }
        }

        private void Update()
        {
            (int newRow, int newCol) = snake.CalculateNextHeadPosition();
            
            if (CheckCollision(newRow, newCol))
            {
                gameOver = true;
                return;
            }
            
            bool ateFood = (newRow == food.row && newCol == food.col);
            
            snake.Move(ateFood);
            
            if (ateFood)
            {
                score++;
                GenerateFood();

                if (gameSpeed > 50)
                {
                    gameSpeed -= 5;
                }
            }

            UpdateGameBoard();
        }

        private bool CheckCollision(int row, int col)
        {
            if (gameBoard.IsWall(row, col))
            {
                return true;
            }
            
            return snake.WillCollideWithSelf((row, col));
        }

        private void UpdateGameBoard()
        {
            gameBoard.Clear();
            gameBoard.PlaceFood(food);
            gameBoard.PlaceSnake(snake);
        }

        private void Render()
        {
            Console.SetCursorPosition(0, 0);
            
            string gameDisplay = "";
            
            gameDisplay += $"Poäng: {score}\n";
            
            for (int row = 0; row < gameBoard.Height; row++)
            {
                for (int col = 0; col < gameBoard.Width; col++)
                {
                    gameDisplay += gameBoard.Board[row, col];
                }
                gameDisplay += "\n";
            }
            
            gameDisplay += "Använd piltangenterna för att styra. ESC för att avsluta.";
            
            Console.Write(gameDisplay);
        }

        public void Run()
        {
            while (!gameOver)
            {
                ProcessInput();
                Update();
                Render();
                Thread.Sleep(gameSpeed);
            }
        }
    }
}