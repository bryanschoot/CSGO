using System;
using CSGO.Data;

namespace CSGO.Console
{
    public class Program
    {
        /// <inheritdoc cref="Game" />
        private Game Game { get; set; }

        private static void Main()
        {
            var game = new Game();
            game.Start();

            do
            {
                while (!System.Console.KeyAvailable)
                {
                    // Do something
                }
            } while (System.Console.ReadKey(true).Key != ConsoleKey.Escape);

            game.Dispose();
        }
    }
}