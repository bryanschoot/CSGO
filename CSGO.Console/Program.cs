using System;
using CSGO.Data;

namespace CSGO.Console
{
    public class Program
    {
        /// <inheritdoc cref="Game" />
        private static Game Game { get; set; }

        private static void Main()
        {
            Startup();

            do {} while (System.Console.ReadKey(true).Key != ConsoleKey.Escape);

            Dispose();
        }

        /// <summary>
        ///     Startup of the application
        /// </summary>
        private static void Startup()
        {
            Game = new Game();
            Game.Start();
        }

        /// <inheritdoc cref="Dispose" />
        private static void Dispose()
        {
            Game.Dispose();
            Game = default;
        }
    }
}