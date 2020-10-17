using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using CSGO.Sys;
using CSGO.Utils;

namespace CSGO.Data
{
    public class Game : Threaded
    {
        private const string NameProcess = "csgo";
        private const string NameModuleClient = "client.dll";
        private const string NameModuleEngine = "engine.dll";
        private const string NameWindow = "Counter-Strike: Global Offensive";

        /// <inheritdoc cref="ThreadName" />
        protected override string ThreadName => nameof(Game);

        /// <inheritdoc cref="TimeSpan" />
        protected override TimeSpan ThreadFrameSleep { get; set; } = new TimeSpan(0, 0, 0, 0, 500);

        /// <summary>
        ///     Game process.
        /// </summary>
        public Process Process { get; private set; }

        /// <summary>
        ///     Client module.
        /// </summary>
        public Module ModuleClient { get; private set; }

        /// <summary>
        ///     Engine module.
        /// </summary>
        public Module ModuleEngine { get; private set; }

        /// <summary>
        ///     Game window handle.
        /// </summary>
        private IntPtr WindowHwnd { get; set; }

        /// <summary>
        ///     Game window client rectangle.
        /// </summary>
        public Rectangle WindowRectangleClient { get; private set; }

        /// <summary>
        ///     Whether game window is active.
        /// </summary>
        private bool WindowActive { get; set; }

        /// <summary>
        ///     Is game process valid?
        /// </summary>
        public bool IsValid => WindowActive && !(Process is null) && !(ModuleClient is null) && !(ModuleEngine is null);

        /// <inheritdoc cref="Dispose" />
        public override void Dispose()
        {
            InvalidateWindow();
            InvalidateModules();

            base.Dispose();
        }

        /// <inheritdoc cref="FrameAction" />
        protected override void FrameAction()
        {
            if (!EnsureProcessAndModules()) InvalidateModules();

            if (!EnsureWindow()) InvalidateWindow();

            Console.WriteLine(IsValid
                ? $"0x{(int) Process.Handle:X8} {WindowRectangleClient.X} {WindowRectangleClient.Y} {WindowRectangleClient.Width} {WindowRectangleClient.Height}"
                : "Game process invalid");
        }

        /// <summary>
        ///     Invalidate all game modules.
        /// </summary>
        private void InvalidateModules()
        {
            ModuleEngine?.Dispose();
            ModuleEngine = default;

            ModuleClient?.Dispose();
            ModuleClient = default;

            Process?.Dispose();
            Process = default;
        }

        /// <summary>
        ///     Invalidate game window.
        /// </summary>
        private void InvalidateWindow()
        {
            WindowHwnd = IntPtr.Zero;
            WindowRectangleClient = Rectangle.Empty;
            WindowActive = false;
        }

        /// <summary>
        ///     Ensure game process and modules.
        /// </summary>
        private bool EnsureProcessAndModules()
        {
            if (Process is null) Process = Process.GetProcessesByName(NameProcess).FirstOrDefault();
            if (Process is null || !Process.IsRunning()) return false;

            if (ModuleClient is null) ModuleClient = Process.GetModule(NameModuleClient);
            if (ModuleClient is null) return false;

            if (ModuleEngine is null) ModuleEngine = Process.GetModule(NameModuleEngine);
            if (ModuleEngine is null) return false;

            return true;
        }

        /// <summary>
        ///     Ensure that user is in the game window.
        /// </summary>
        private bool EnsureWindow()
        {
            WindowHwnd = User32.FindWindow(null, NameWindow);
            if (WindowHwnd == IntPtr.Zero) return false;

            WindowRectangleClient = Utility.GetClientRectangle(WindowHwnd);
            if (WindowRectangleClient.Width <= 0 || WindowRectangleClient.Height <= 0) return false;

            WindowActive = WindowHwnd == User32.GetForegroundWindow();

            return WindowActive;
        }
    }
}