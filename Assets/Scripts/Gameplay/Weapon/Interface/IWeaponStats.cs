using UnityEngine;

namespace Gameplay.Weapon.Interface
{
    public interface IWeaponStats
    {
        public float BaseDamage { get; }
        public int ArrowCount { get; }
        public float BurnDuration { get; }
        public int BounceCount { get; }
        public Vector3[] BouncePoints { get; set; }
    }
}
