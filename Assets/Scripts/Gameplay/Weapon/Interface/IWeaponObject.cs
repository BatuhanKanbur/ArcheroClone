using UnityEngine;

namespace Gameplay.Weapon.Interface
{
    public interface IWeaponObject
    {
        public Transform SpawnPoint { get; }
        public void SetActive(bool isActive);
        public void SetParent(Transform parent);
    }
}
