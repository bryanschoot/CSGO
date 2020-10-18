using CSGO.Helpers;
using CSGO.Offsets;
using System;

namespace CSGO.Data
{
    public class Player : Entity
    {
        public int Index { get; }
        public bool Dormant { get; private set; } = true;
        public int GlowIndex { get; set; }

        public Player(int index)
        {
            Index = index;
        }

        public override bool IsAlive()
        {
            return base.IsAlive() && !Dormant;
        }

        protected override IntPtr ReadAddressBase(Game game)
        {
            return game.ModuleClient.Read<IntPtr>(Signatures.dwEntityList + Index * 0x10);
        }

        public override bool Update(Game game)
        {
            if (!base.Update(game))
            {
                return false;
            }

            UpdateGlowIndex(game);

            Dormant = game.Process.Read<bool>(AddressBase + Signatures.m_bDormant);

            return true;
        }

        private void UpdateGlowIndex(Game game)
        {
            GlowIndex = game.Process.Read<int>(AddressBase + NetVars.m_iGlowIndex);
        }
    }
}
