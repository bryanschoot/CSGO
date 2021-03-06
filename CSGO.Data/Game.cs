﻿using CSGO.DLL;
using CSGO.Helpers;
using CSGO.Utils;
using System;
using System.Diagnostics;
using System.Linq;

namespace CSGO.Data
{
    /// <summary>
    ///     Game data
    /// </summary>
    public class Game : Threading
    {
        private const string NAME_PROCESS = "csgo";
        private const string NAME_MODULE_CLIENT = "client.dll";
        private const string NAME_MODULE_ENGINE = "engine.dll";
        private const string NAME_WINDOW = "Counter-Strike: Global Offensive";

        protected override string ThreadName => nameof(Game);
        protected override TimeSpan ThreadFrameSleep { get; set; } = new TimeSpan(0, 0, 0, 0, 500);

        public Process Process { get; set; }
        public Module ModuleEngine { get; set; }
        public Module ModuleClient { get; set; }
        public IntPtr WindowHwnd { get; set; }

        public Game()
        {
            EnsureProcessAndModules();
            EnsureWindow();
        }

        public bool IsValid()
        {
            return !(Process is null) && !(ModuleClient is null) && !(ModuleEngine is null);
        }

        private bool EnsureProcessAndModules()
        {
            if (Process is null)
            {
                Process = Process.GetProcessesByName(NAME_PROCESS).FirstOrDefault();
            }
            if (Process is null || !Process.IsRunning())
            {
                return false;
            }

            if (ModuleClient is null)
            {
                ModuleClient = Process.GetModule(NAME_MODULE_CLIENT);
            }
            if (ModuleClient is null)
            {
                return false;
            }

            if (ModuleEngine is null)
            {
                ModuleEngine = Process.GetModule(NAME_MODULE_ENGINE);
            }
            if (ModuleEngine is null)
            {
                return false;
            }

            return true;
        }

        private bool EnsureWindow()
        {
            WindowHwnd = User32.FindWindow(null, NAME_WINDOW);
            if (WindowHwnd == IntPtr.Zero)
            {
                return false;
            }

            return true;
        }

        private void InvalidateModules()
        {
            ModuleEngine?.Dispose();
            ModuleEngine = default;

            ModuleClient?.Dispose();
            ModuleClient = default;

            Process?.Dispose();
            Process = default;
        }

        private void InvalidateWindow()
        {
            WindowHwnd = IntPtr.Zero;
        }

        protected override void FrameAction()
        {
            if (!EnsureProcessAndModules())
            {
                InvalidateModules();
            }

            if (!EnsureWindow())
            {
                InvalidateWindow();
            }
        }
    }
}
