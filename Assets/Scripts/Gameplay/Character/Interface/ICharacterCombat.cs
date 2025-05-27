using Gameplay.Weapon.Interface;

namespace Gameplay.Character.Interface
{
    public interface ICharacterCombat : ICharacterComponent
    {
        public IWeapon Weapon { get; }
        public bool IsAttacking { get; }
    }
}
