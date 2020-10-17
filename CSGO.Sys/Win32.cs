using System.Runtime.InteropServices;

namespace CSGO.Sys
{
    public static class Win32
    {
        /// <summary>
        ///     https://docs.microsoft.com/en-us/windows/win32/api/windef/ns-windef-rect
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int Left, Top, Right, Bottom;
        }

        /// <summary>
        ///     https://docs.microsoft.com/en-us/windows/win32/api/windef/ns-windef-point
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            public int X, Y;
        }
    }
}