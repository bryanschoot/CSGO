using CSGO.Utils;
using System.Linq;

namespace CSGO.Data
{
    /// <summary>
    ///     Match data
    /// </summary>
    public class Match : Threading
    {
        protected override string ThreadName => nameof(Match);
        public Game Game { get; set; }
        public Me Me { get; set; }
        public Player[] Players { get; private set; }

        public Match(Game game)
        {
            Game = game;
            Me = new Me();
            Players = Enumerable.Range(0, 32).Select(index => new Player(index)).ToArray();
        }

        public override void Dispose()
        {
            base.Dispose();

            Me = default;
            Game = default;
        }

        protected override void FrameAction()
        {
            if (!Game.IsValid())
            {
                return;
            }

            Me.Update(Game);
            foreach (var entity in Players)
            {
                entity.Update(Game);
            }
        }
    }
}
