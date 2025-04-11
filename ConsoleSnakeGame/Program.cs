namespace CoolSnakeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Console Snake";
            Console.CursorVisible = false;
            
            Game game = new Game();
            game.Run();
            
            Console.WriteLine("Spelet är slut! Tryck på valfri tangent för att avsluta.");
            Console.ReadKey();
        }
    }
}