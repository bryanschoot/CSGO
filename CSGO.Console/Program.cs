using System;
using CSGO.Data;

namespace CSGO.Console
{
    public class Program
    {
        private static GameProcess GameProcess { get; set; }
        private static GameData GameData { get; set; }

        private static void Main()
        {
            Startup();

            do
            {
            } while (System.Console.ReadKey(true).Key != ConsoleKey.Escape);

            Dispose();
        }

        /// <summary>
        ///     Startup of the application
        /// </summary>
        private static void Startup()
        {
            GameProcess = new GameProcess();
            GameData = new GameData(GameProcess);

            GameProcess.Start();
            GameData.Start();
        }

        /// <inheritdoc cref="Dispose" />
        private static void Dispose()
        {
            GameProcess.Dispose();
            GameProcess = default;

            GameData.Dispose();
            GameData = default;
        }
    }
}