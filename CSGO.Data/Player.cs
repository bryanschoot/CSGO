using CSGO.Helpers;
using CSGO.Offsets;
using System;
using System.Numerics;

namespace CSGO.Data
{
    /// <summary>
    ///     Player data
    /// </summary>
    public class Player
    {
        public void Update(Game game)
        {
            var addressBase = game.ModuleClient.Read<IntPtr>(Signatures.dwLocalPlayer);

            if (addressBase == IntPtr.Zero)
            {
                return;
            }

            var origin = game.Process.Read<Vector3>(addressBase + NetVars.m_vecOrigin);
            var viewOffset = game.Process.Read<Vector3>(addressBase + NetVars.m_vecViewOffset);
            var eyePosition = origin + viewOffset;

            Console.WriteLine($"{eyePosition.X:0.00} {eyePosition.Y:0.00} {eyePosition.Z:0.00}");
        }
    }
}
