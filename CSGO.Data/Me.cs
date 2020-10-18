using CSGO.Helpers;
using CSGO.Offsets;
using System;

namespace CSGO.Data
{
    /// <summary>
    ///     Player data
    /// </summary>
    public class Me : Entity
    {
        public Me() { }

        public override bool Update(Game game)
        {
            if (!base.Update(game))
            {
                return false;
            }

            return true;
        }

        protected override IntPtr ReadAddressBase(Game game)
        {
            return game.ModuleClient.Read<IntPtr>(Signatures.dwLocalPlayer);
        }
    }
}
