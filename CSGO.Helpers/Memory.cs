using CSGO.DLL;
using CSGO.Utils;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace CSGO.Helpers
{
    /// <summary>
    ///     Extensions on Process
    /// </summary>
    public static class Memory
    {
        public static Module GetModule(this Process process, string moduleName)
        {
            var processModule = process.GetProcessModule(moduleName);
            return processModule is null || processModule.BaseAddress == IntPtr.Zero
                ? default
                : new Module(process, processModule);
        }

        public static ProcessModule GetProcessModule(this Process process, string moduleName)
        {
            return process?.Modules.OfType<ProcessModule>()
                .FirstOrDefault(a => string.Equals(a.ModuleName.ToLower(), moduleName.ToLower()));
        }

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

        public static T Read<T>(this Process process, IntPtr lpBaseAddress) where T : unmanaged
        {
            return Read<T>(process.Handle, lpBaseAddress);
        }

        public static T Read<T>(this Module module, int offset) where T : unmanaged
        {
            return Read<T>(module.Process.Handle, module.ProcessModule.BaseAddress + offset);
        }

        public static T Read<T>(IntPtr hProcess, IntPtr lpBaseAddress) where T : unmanaged
        {
            var size = Marshal.SizeOf<T>();
            var buffer = (object)default(T);
            Kernel32.ReadProcessMemory(hProcess, lpBaseAddress, buffer, size, out var lpNumberOfBytesRead);
            return lpNumberOfBytesRead == size ? (T)buffer : default;
        }

        public static void WriteMemory<T>(this Module module, int address, T buffer) where T : struct
        {
            var hProc = Kernel32.OpenProcess(ProcessFlags.All, false, module.Process.Id);
            var val = GetBytes<T>(buffer);

            Kernel32.WriteProcessMemory(hProc, new IntPtr(address), val, (uint)val.LongLength, out _);
            Kernel32.CloseHandle(hProc);
        }

        public static Team ToTeam(this int team)
        {
            switch (team)
            {
                case 1:
                    return Team.Spectator;
                case 2:
                    return Team.Terrorists;
                case 3:
                    return Team.CounterTerrorists;
                default:
                    return Team.Unknown;
            }
        }

        private static byte[] GetBytes<T>(T str) where T : struct
        {
            int size = Marshal.SizeOf(str);
            byte[] arr = new byte[size];

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(str, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }
    }
}
