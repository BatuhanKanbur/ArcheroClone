using Gameplay.Damageable.Interface;
using Gameplay.Damageable.Structure;
using Gameplay.Weapon.Interface;
using UnityEngine;

namespace Gameplay.Weapon.Behaviour
{
    public class ThrowableObject : MonoBehaviour, IThrowable
    {
        public IWeaponStats WeaponStats { get; set; }
        [SerializeField] private float throwForce = 1f;
        [SerializeField] private Rigidbody rb;
        private int _bounceCount;
        private int _currentTargetIndex;
        private Vector3[] _targetPositions;
        public void Init(IWeaponStats weapon,Vector3[] targetPositions)
        {
            _bounceCount = 0;
            _currentTargetIndex = 0;
            WeaponStats = weapon;
            _targetPositions = targetPositions;
            LaunchToTarget(_targetPositions[_bounceCount]);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(new DamageStats(WeaponStats));
            else
                gameObject.SetActive(false);
            _bounceCount++;
            if (_bounceCount > WeaponStats.BounceCount || _currentTargetIndex + 1 >= _targetPositions.Length) return;
            _currentTargetIndex++;
            LaunchToTarget(_targetPositions[_currentTargetIndex]);
        }
        private void LaunchToTarget(Vector3 target)
        {
            transform.LookAt(target);
            rb.Sleep();
            rb.drag = 0;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(transform.forward * throwForce);
        }
    }
}
