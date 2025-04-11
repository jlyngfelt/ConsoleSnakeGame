using System;
using System.Collections.Generic;

namespace CoolSnakeGame
{
    public class FoodGenerator
    {
        private Random random = new Random();
        private int boardHeight;
        private int boardWidth;
        
        public FoodGenerator(int height, int width)
        {
            boardHeight = height;
            boardWidth = width;
        }
        
        public (int row, int col) GenerateFood(List<(int row, int col)> occupiedPositions)
        {
            int foodRow, foodCol;
            
            do
            {
                foodRow = random.Next(1, boardHeight - 1);
                foodCol = random.Next(1, boardWidth - 1);
                
            } while (occupiedPositions.Contains((foodRow, foodCol)));
            
            return (foodRow, foodCol);
        }
    }
}