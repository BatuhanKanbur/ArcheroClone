using Gameplay.Player.Interface;
using Gameplay.Player.Structure;

namespace Gameplay.Player.Behaviour
{
    public class PlayerStatus : IPlayerStatus
    {
        public IPlayer Player { get; }
        public PlayerStats Stats { get; }
        public PlayerStatus(IPlayer player)
        {
            Player = player;
            Stats = new PlayerStats();
        }
        public void Dispose()
        {
        }

        public void Update()
        {
        }

        public void Reset()
        {
        }
    }
}
