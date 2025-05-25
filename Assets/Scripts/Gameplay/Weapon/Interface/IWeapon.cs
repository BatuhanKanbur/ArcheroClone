using Cysharp.Threading.Tasks;
using Gameplay.Weapon.Structure;
using UnityEngine;

namespace Gameplay.Weapon.Interface
{
    public interface IWeapon
    {
        public WeaponStats WeaponStats { get; }
        public IWeaponObject LeftHandObject { get; }
        public IWeaponObject RightHandObject { get; }
        public Transform GetSpawnPoint { get; }
        public UniTaskVoid SpawnWeapon(Transform leftHand, Transform rightHand);
        public UniTaskVoid SpawnThrowable();
        public void Attack();
    }
}
