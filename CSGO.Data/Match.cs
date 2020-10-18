using CSGO.Utils;

namespace CSGO.Data
{
    /// <summary>
    ///     Match data
    /// </summary>
    public class Match : Threading
    {
        public Game Game { get; set; }
        public Player Player { get; set; }

        public Match(Game game)
        {
            Game = game;
            Player = new Player();
        }

        public override void Dispose()
        {
            base.Dispose();

            Player = default;
            Game = default;
        }

        protected override void FrameAction()
        {
            if (!Game.IsValid())
            {
                return;
            }

            Player.Update(Game);
        }
    }
}
