using Gameplay.Player.Structure;

namespace Gameplay.Player.Interface
{
    public interface IPlayerStatus : IPlayerComponent
    {
        public PlayerStats Stats { get; }
    }
}
