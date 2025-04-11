namespace CoolSnakeGame
{
    public class GameBoard
    {
        public int Width { get; }
        public int Height { get; }
        public char[,] Board { get; }
        
        public const char EMPTY = ' ';
        public const char WALL = '#';
        public const char FOOD = '*';
        
        public GameBoard(int width, int height)
        {
            Width = width;
            Height = height;
            Board = new char[height, width];
            
            InitializeBoard();
        }
        
        private void InitializeBoard()
        {
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    if (row == 0 || row == Height - 1 || col == 0 || col == Width - 1)
                    {
                        Board[row, col] = WALL;
                    }
                    else
                    {
                        Board[row, col] = EMPTY;
                    }
                }
            }
        }
        
        public void Clear()
        {
            for (int row = 1; row < Height - 1; row++)
            {
                for (int col = 1; col < Width - 1; col++)
                {
                    Board[row, col] = EMPTY;
                }
            }
        }
        
        public void PlaceFood((int row, int col) position)
        {
            Board[position.row, position.col] = FOOD;
        }
        
        public void PlaceSnake(Snake snake)
        {
            (int headRow, int headCol) = snake.Head;
            Board[headRow, headCol] = Snake.HEAD_CHAR;
            
            for (int i = 1; i < snake.Body.Count; i++)
            {
                (int row, int col) = snake.Body[i];
                Board[row, col] = Snake.BODY_CHAR;
            }
        }
        
        public bool IsWall(int row, int col)
        {
            return Board[row, col] == WALL;
        }
    }
}