using System;
using Gameplay.Weapon.Interface;
using UnityEngine;

namespace Gameplay.Weapon.Behaviour
{
    public class WeaponObject : MonoBehaviour ,IWeaponObject
    {
        [SerializeField] private Transform attackPoint;
        public Transform SpawnPoint => attackPoint;
        public void SetActive(bool isActive) => gameObject.SetActive(isActive);
        public void SetParent(Transform parent)
        {
            if (!parent) throw new ArgumentNullException(nameof(parent), "Parent transform cannot be null.");
            transform.SetParent(parent);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }
}
