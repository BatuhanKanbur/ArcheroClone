using Gameplay.Weapon.Interface;

namespace Gameplay.Damageable.Structure
{
    public record DamageStats
    {
        public float Damage;
        public float BurnDuration;

        public DamageStats(IWeaponStats weaponStats)
        {
            Damage = weaponStats.BaseDamage;
            BurnDuration = weaponStats.BurnDuration;
        }
    }
}
