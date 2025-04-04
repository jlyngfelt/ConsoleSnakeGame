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
    
    
    
    
    
    
    
}