using CSGO.Helpers;
using CSGO.Offsets;
using System;
using System.Numerics;

namespace CSGO.Data
{
    /// <summary>
    ///     Player data
    /// </summary>
    public class Me : Entity
    {
        public Vector3 ViewAngles { get; private set; }
        public Vector3 AimPunchAngle { get; private set; }
        public int ShotsFired { get; private set; }

        public Me() { }

        public override bool Update(Game game)
        {
            if (!base.Update(game))
            {
                return false;
            }

            ViewAngles = game.Process.Read<Vector3>(game.ModuleEngine.Read<IntPtr>(Signatures.dwClientState) + Signatures.dwClientState_ViewAngles);
            AimPunchAngle = game.Process.Read<Vector3>(AddressBase + NetVars.m_aimPunchAngle);
            ShotsFired = game.Process.Read<int>(AddressBase + NetVars.m_iShotsFired);

            return true;
        }

        protected override IntPtr ReadAddressBase(Game game)
        {
            return game.ModuleClient.Read<IntPtr>(Signatures.dwLocalPlayer);
        }
    }
}
