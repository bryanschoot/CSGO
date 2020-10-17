using System;
using CSGO.Offsets;
using CSGO.Sys;
using CSGO.Utils;

namespace CSGO.Data
{
    public class Player
    {
        /// <summary>
        ///     Update data from process.
        /// </summary>
        public void Update(GameProcess gameProcess)
        {
            var addressBase = gameProcess.ModuleClient.Read<IntPtr>(Signatures.dwLocalPlayer);
            if (addressBase == IntPtr.Zero) return;

            var origin = gameProcess.Process.Read<Win32.Vector3>(addressBase + NetVars.m_vecOrigin);
            var viewOffset = gameProcess.Process.Read<Win32.Vector3>(addressBase + NetVars.m_vecViewOffset);
            var eyePosition = origin + viewOffset;

            Console.WriteLine($"{eyePosition.X:0.00} {eyePosition.Y:0.00} {eyePosition.Z:0.00}");
        }
    }
}