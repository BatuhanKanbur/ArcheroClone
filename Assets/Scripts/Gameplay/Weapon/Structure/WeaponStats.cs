using Gameplay.Skill.Structure;
using Gameplay.Weapon.Interface;
using UnityEngine;

namespace Gameplay.Weapon.Structure
{
    [CreateAssetMenu(fileName = "WeaponStats", menuName = "ScriptableObjects/WeaponStats", order = 1)]
    public class WeaponStats : StatsData , IWeaponStats
    {
        public float BaseDamage => baseDamage * multiplier;
        public int ArrowCount => arrowCount * multiplier;
        public float BurnDuration => burnDuration * multiplier;
        public int BounceCount => bounceCount * multiplier;
        
        [SerializeField] private float baseDamage;
        [SerializeField] private int arrowCount;
        [SerializeField] private float burnDuration;
        [SerializeField] private int bounceCount;
        public WeaponStats(float baseDamage, int arrowCount, float burnDuration, int bounceCount) : base(1)
        {
            this.baseDamage = baseDamage;
            this.arrowCount = arrowCount;
            this.burnDuration = burnDuration;
            this.bounceCount = bounceCount;
        }
        public static WeaponStats operator +(WeaponStats a, WeaponStats b)
        {
            return new WeaponStats(
                a.baseDamage + b.baseDamage,
                a.arrowCount + b.arrowCount,
                a.burnDuration + b.burnDuration,
                a.bounceCount + b.bounceCount
            );
        }
        public static WeaponStats operator -(WeaponStats a, WeaponStats b)
        {
            return new WeaponStats(
                a.baseDamage - b.baseDamage,
                a.arrowCount - b.arrowCount,
                a.burnDuration - b.burnDuration,
                a.bounceCount - b.bounceCount
            );
        }
    }
}
