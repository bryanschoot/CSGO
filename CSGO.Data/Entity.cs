using CSGO.Helpers;
using CSGO.Offsets;
using System;
using System.Numerics;

namespace CSGO.Data
{
    public abstract class Entity
    {
        public IntPtr AddressBase { get; protected set; }
        public bool LifeState { get; protected set; }
        public int Health { get; protected set; }
        public Team Team { get; protected set; }
        public Vector3 Origin { get; private set; }

        public virtual bool IsAlive()
        {
            return AddressBase != IntPtr.Zero &&
                   !LifeState &&
                   Health > 0 &&
                   (Team == Team.Terrorists || Team == Team.CounterTerrorists);
        }

        protected abstract IntPtr ReadAddressBase(Game game);

        public virtual bool Update(Game game)
        {
            AddressBase = ReadAddressBase(game);

            if (AddressBase == IntPtr.Zero)
            {
                return false;
            }

            LifeState = game.Process.Read<bool>(AddressBase + NetVars.m_lifeState);
            Health = game.Process.Read<int>(AddressBase + NetVars.m_iHealth);
            Team = game.Process.Read<int>(AddressBase + NetVars.m_iTeamNum).ToTeam();
            Origin = game.Process.Read<Vector3>(AddressBase + NetVars.m_vecOrigin);

            return true;
        }
    }
}
