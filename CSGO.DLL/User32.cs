using System;
using System.Runtime.InteropServices;

namespace CSGO.DLL
{
    /// <summary>
    ///     User32 uses the Win32 api from windows
    /// </summary>
    public class User32
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    }
}
