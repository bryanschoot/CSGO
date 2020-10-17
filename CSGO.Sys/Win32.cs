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

        /// <summary>
        ///     https://docs.microsoft.com/en-us/uwp/api/windows.foundation.numerics.vector3?view=winrt-19041
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct Vector3
        {
            public float X, Y, Z;

            public Vector3(float x, float y, float z)
            {
                X = x;
                Y = y;
                Z = z;
            }

            public static Vector3 operator +(Vector3 left, Vector3 right)
            {
                return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
            }
        }
    }
}