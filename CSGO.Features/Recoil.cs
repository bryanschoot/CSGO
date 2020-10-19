using CSGO.Data;
using CSGO.Helpers;
using CSGO.Offsets;
using CSGO.Utils;
using System;
using System.Numerics;

namespace CSGO.Features
{
    public class Recoil : Threading
    {
        private Match Match { get; set; }
        private Vector3 OldPunch { get; set; } = new Vector3() { X = 0, Y = 0, Z = 0 };

        public Recoil(Match match)
        {
            Match = match;
        }

        protected override void FrameAction()
        {
            //dwEngineBase = MemoryManager->dwGetModuleBaseAddress(pid, "engine.dll");
            //dwClientState = MemoryManager->Read<DWORD>(dwEngineBase + m_dwClientState);
            //MemoryManager->Write<Vector3>(dwClientState + dwClientState_ViewAngles, AimVector);

            var punchAngle = Match.Me.AimPunchAngle * 2;
            if (Match.Me.ShotsFired > 1)
            {
                var newAngle = Match.Me.ViewAngles + OldPunch - punchAngle;
                newAngle.Normalize();
                Match.Game.ModuleClient.WriteMemory(Match.Game.ModuleEngine.Read<IntPtr>(Signatures.dwClientState) + Signatures.dwClientState_ViewAngles, newAngle);
            }
            OldPunch = punchAngle;
        }
    }
}
