using System;
using System.Runtime.InteropServices;

namespace CSGO.Sys
{
    public static class User32
    {
        /// <summary>
        ///     https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-clienttoscreen
        /// </summary>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool ClientToScreen(IntPtr hWnd, out Win32.Point lpPoint);

        /// <summary>
        ///     https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getclientrect
        /// </summary>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetClientRect(IntPtr hWnd, out Win32.Rect lpRect);

        /// <summary>
        ///     https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getforegroundwindow
        /// </summary>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetForegroundWindow();

        /// <summary>
        ///     https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-findwindowa
        /// </summary>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    }
}