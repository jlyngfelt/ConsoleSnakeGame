# Console Snake Game

A classic Snake game implementation for the console written in C#. This project was created by following and combining elements from two different guides.

## Project Overview

This is a simple implementation of the classic Snake game that runs in the console. The player controls a snake that grows longer as it eats food. The game ends when the snake collides with the wall or with itself.

## How the Game Works

- The snake is represented by '@' (head) and 'O' (body)
- Food is represented by '*'
- Walls are represented by '#'
- Control the snake using the arrow keys
- Press ESC to quit the game

## Project Structure

The game is organized into several classes:

- **Program.cs**: Entry point of the application
- **Game.cs**: Main game loop and logic
- **Snake.cs**: Snake movement and behavior
- **GameBoard.cs**: Rendering of the game area
- **FoodGenerator.cs**: Generates food at random positions
