using CSGO.Utils;

namespace CSGO.Data
{
    public class GameData : Threading
    {
        /// <summary>
        ///     Data from the actual game
        /// </summary>
        public GameData(GameProcess gameProcess)
        {
            GameProcess = gameProcess;
            Player = new Player();
        }

        /// <inheritdoc cref="ThreadName" />
        protected override string ThreadName => nameof(GameData);

        /// <inheritdoc cref="GameProcess" />
        private GameProcess GameProcess { get; set; }

        /// <inheritdoc cref="Player" />
        public Player Player { get; set; }

        /// <inheritdoc cref="Dispose" />
        public override void Dispose()
        {
            base.Dispose();

            Player = default;
            GameProcess = default;
        }

        /// <inheritdoc cref="FrameAction" />
        protected override void FrameAction()
        {
            if (!GameProcess.IsValid) return;

            Player.Update(GameProcess);
        }
    }
}