using Gameplay.Damageable.Interface;
using Gameplay.Damageable.Structure;
using Gameplay.Weapon.Interface;
using UnityEngine;

namespace Gameplay.Weapon.Behaviour
{
    public class ThrowableObject : MonoBehaviour, IThrowable
    {
        public IWeaponStats WeaponStats { get; set; }
        [SerializeField] private float throwForce = 10f;
        [SerializeField] private Rigidbody rb;
        private int _bounceCount;
        public void Init(IWeaponStats weapon)
        {
            WeaponStats = weapon;
            rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(new DamageStats(WeaponStats));
            gameObject.SetActive(false);
        }
    }
}
