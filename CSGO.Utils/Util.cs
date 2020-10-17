using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using CSGO.Sys;

namespace CSGO.Utils
{
    public static class Util
    {
        /// <summary>
        ///     Get window client rectangle.
        /// </summary>
        public static Rectangle GetClientRectangle(IntPtr handle)
        {
            return User32.ClientToScreen(handle, out var point) && User32.GetClientRect(handle, out var rect)
                ? new Rectangle(point.X, point.Y, rect.Right - rect.Left, rect.Bottom - rect.Top)
                : default;
        }

        /// <summary>
        ///     Get module by name.
        /// </summary>
        public static Module GetModule(this Process process, string moduleName)
        {
            var processModule = process.GetProcessModule(moduleName);
            return processModule is null || processModule.BaseAddress == IntPtr.Zero
                ? default
                : new Module(process, processModule);
        }

        /// <summary>
        ///     Get process module by name.
        /// </summary>
        public static ProcessModule GetProcessModule(this Process process, string moduleName)
        {
            return process?.Modules.OfType<ProcessModule>()
                .FirstOrDefault(a => string.Equals(a.ModuleName.ToLower(), moduleName.ToLower()));
        }

        /// <summary>
        ///     Get if process is running.
        /// </summary>
        public static bool IsRunning(this Process process)
        {
            try
            {
                Process.GetProcessById(process.Id);
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Read process memory.
        /// </summary>
        public static T Read<T>(this Process process, IntPtr lpBaseAddress)
            where T : unmanaged
        {
            return Read<T>(process.Handle, lpBaseAddress);
        }

        /// <summary>
        ///     Read process memory from module.
        /// </summary>
        public static T Read<T>(this Module module, int offset)
            where T : unmanaged
        {
            return Read<T>(module.Process.Handle, module.ProcessModule.BaseAddress + offset);
        }

        private static T Read<T>(object handle, object p) where T : unmanaged
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Read process memory.
        /// </summary>
        public static T Read<T>(IntPtr hProcess, IntPtr lpBaseAddress)
            where T : unmanaged
        {
            var size = Marshal.SizeOf<T>();
            var buffer = (object) default(T);
            Kernel32.ReadProcessMemory(hProcess, lpBaseAddress, buffer, size, out var lpNumberOfBytesRead);
            return lpNumberOfBytesRead == size ? (T) buffer : default;
        }
    }
}