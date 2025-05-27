using System;
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
        public Vector3[] BouncePoints { get; set; } = Array.Empty<Vector3>();
        
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
            var newStats = CreateInstance<WeaponStats>();
            newStats.multiplier = a.multiplier + b.multiplier;
            newStats.baseDamage = a.baseDamage + b.baseDamage;
            newStats.arrowCount = a.arrowCount + b.arrowCount;
            newStats.burnDuration = a.burnDuration + b.burnDuration;
            newStats.bounceCount = a.bounceCount + b.bounceCount;
            return newStats;
        }
        public static WeaponStats operator -(WeaponStats a, WeaponStats b)
        {
            var newStats = CreateInstance<WeaponStats>();
            newStats.multiplier = a.multiplier - b.multiplier;
            newStats.baseDamage = a.baseDamage - b.baseDamage;
            newStats.arrowCount = a.arrowCount - b.arrowCount;
            newStats.burnDuration = a.burnDuration - b.burnDuration;
            newStats.bounceCount = a.bounceCount - b.bounceCount;
            return newStats;
        }
    }
}
