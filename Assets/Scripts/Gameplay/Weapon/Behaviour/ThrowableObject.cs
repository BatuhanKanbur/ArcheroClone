using Gameplay.Damageable.Interface;
using Gameplay.Weapon.Interface;
using UnityEngine;

namespace Gameplay.Weapon.Behaviour
{
    public class ThrowableObject : MonoBehaviour, IThrowable
    {
        public IWeaponStats Weapon { get; set; }
        [SerializeField] private float throwForce = 10f;
        [SerializeField] private Rigidbody rb;
        public void Init(IWeaponStats weapon)
        {
            Weapon = weapon;
            rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                
            }
        }
    }
}
