using Gameplay.Weapon.Interface;

namespace Gameplay.Player.Interface
{
    public interface IPlayerCombat : IPlayerComponent
    {
        public IWeapon Weapon { get; }
    }
}
