using System.Collections.Generic;

namespace CoolSnakeGame
{
    public class Snake
    {
        public List<(int row, int col)> Body { get; private set; }
        public (int row, int col) Head => Body[0];
        public enum Direction { Up, Down, Left, Right }
        public Direction CurrentDirection { get; set; }
        
        public const char BODY_CHAR = 'O';
        public const char HEAD_CHAR = '@';
        
        public Snake(int startRow, int startCol)
        {
            Body = new List<(int row, int col)>();

            Body.Add((startRow, startCol)); 
            Body.Add((startRow, startCol - 1)); 
            Body.Add((startRow, startCol - 2)); 
            
            CurrentDirection = Direction.Right;
        }
        
        public void Move(bool grow)
        {
            (int newRow, int newCol) = CalculateNextHeadPosition();

            Body.Insert(0, (newRow, newCol));
            
            if (!grow)
            {
                Body.RemoveAt(Body.Count - 1);
            }
        }
        
        public (int row, int col) CalculateNextHeadPosition()
        {
            (int headRow, int headCol) = Head;
            
            return CurrentDirection switch
            {
                Direction.Up => (headRow - 1, headCol),
                Direction.Down => (headRow + 1, headCol),
                Direction.Left => (headRow, headCol - 1),
                Direction.Right => (headRow, headCol + 1),
                _ => (headRow, headCol)
            };
        }
        
        public bool WillCollideWithSelf((int row, int col) nextPosition)
        {
            for (int i = 0; i < Body.Count - 1; i++)
            {
                if (Body[i].row == nextPosition.row && Body[i].col == nextPosition.col)
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}