using CSGO.Data;
using CSGO.Helpers;
using CSGO.Offsets;
using CSGO.Utils;
using System;

namespace CSGO.Features
{
    public class Wallhack : Threading
    {
        protected override string ThreadName => nameof(Wallhack);
        protected override TimeSpan ThreadFrameSleep { get; set; } = new TimeSpan(0, 0, 0, 0, 1);
        private Match Match { get; set; }

        private Glow Enemy { get; set; } = new Glow()
        {
            R = 1,
            G = 0,
            B = 0,
            A = 1,
            RWO = true,
            RWUO = false
        };

        private Glow Friendly { get; set; } = new Glow()
        {
            R = 0,
            G = 1,
            B = 0,
            A = 1,
            RWO = true,
            RWUO = false
        };

        public Wallhack(Match match)
        {
            Match = match;
        }

        protected override void FrameAction()
        {
            var glow = Match.Game.ModuleClient.Read<int>(Signatures.dwGlowObjectManager);

            foreach (var player in Match.Players)
            {
                if (player.IsAlive())
                {
                    if (Match.Me.Team != player.Team)
                    {
                        Match.Game.ModuleClient.WriteMemory(new IntPtr(glow + (player.GlowIndex * 0x38) + 0x4), Enemy.R);
                        Match.Game.ModuleClient.WriteMemory(new IntPtr(glow + (player.GlowIndex * 0x38) + 0x8), Enemy.G);
                        Match.Game.ModuleClient.WriteMemory(new IntPtr(glow + (player.GlowIndex * 0x38) + 0xC), Enemy.B);
                        Match.Game.ModuleClient.WriteMemory(new IntPtr(glow + (player.GlowIndex * 0x38) + 0x10), Enemy.A);
                        Match.Game.ModuleClient.WriteMemory(new IntPtr(glow + (player.GlowIndex * 0x38) + 0x24), Enemy.RWO);
                        Match.Game.ModuleClient.WriteMemory(new IntPtr(glow + (player.GlowIndex * 0x38) + 0x26), Enemy.RWUO);
                    }                                      
                    if (Match.Me.Team == player.Team)       
                    {                                       
                        Match.Game.ModuleClient.WriteMemory(new IntPtr(glow + (player.GlowIndex * 0x38) + 0x4), Friendly.R);
                        Match.Game.ModuleClient.WriteMemory(new IntPtr(glow + (player.GlowIndex * 0x38) + 0x8), Friendly.G);
                        Match.Game.ModuleClient.WriteMemory(new IntPtr(glow + (player.GlowIndex * 0x38) + 0xC), Friendly.B);
                        Match.Game.ModuleClient.WriteMemory(new IntPtr(glow + (player.GlowIndex * 0x38) + 0x10), Friendly.A);
                        Match.Game.ModuleClient.WriteMemory(new IntPtr(glow + (player.GlowIndex * 0x38) + 0x24), Friendly.RWO);
                        Match.Game.ModuleClient.WriteMemory(new IntPtr(glow + (player.GlowIndex * 0x38) + 0x26), Friendly.RWUO);
                    }
                }
            }
        }


    }
}
