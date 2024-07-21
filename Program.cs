using System;

using Ex02;

class Program
{
    public static void Main()
    {
        GameEngine gameEngine = Runner.SetUpGame();

        Runner.RunGame(gameEngine);
    }
}
