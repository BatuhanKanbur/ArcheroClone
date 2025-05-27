using Gameplay.Damageable.Interface;
using Gameplay.Damageable.Structure;
using Gameplay.Weapon.Interface;
using UnityEngine;

namespace Gameplay.Weapon.Behaviour
{
    public class ThrowableObject : MonoBehaviour, IThrowable
    {
        public IWeaponStats WeaponStats { get; set; }
        [Range(0,1.25f)]
        [SerializeField] private float throwForce = 1f;
        [Range(0,45)]
        [SerializeField] private float throwAngle = 45f;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private GameObject fireEffect;
        private int _bounceCount;
        private int _currentTargetIndex;
        private Vector3[] _targetPositions;
        public void Init(IWeaponStats weapon,Vector3[] targetPositions)
        {
            _bounceCount = 0;
            _currentTargetIndex = 0;
            WeaponStats = weapon;
            _targetPositions = targetPositions;
            fireEffect.SetActive(WeaponStats.BurnDuration>0);
            LaunchToTarget(_targetPositions[_bounceCount]);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(new DamageStats(WeaponStats));
                if (_bounceCount >= WeaponStats.BounceCount)
                {
                    gameObject.SetActive(false);
                    return;
                }
                _bounceCount++;
                if (_bounceCount > WeaponStats.BounceCount || _currentTargetIndex + 1 >= _targetPositions.Length) return;
                _currentTargetIndex++;
                LaunchToTarget(_targetPositions[_currentTargetIndex]);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        private void LaunchToTarget(Vector3 target)
        {
            var velocity = CalculateBallisticVelocity(target);
            if (velocity == Vector3.zero) return;
            rb.Sleep();
            rb.drag = 0;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.velocity = velocity;
        }

        private void FixedUpdate()
        {
            if (rb.velocity.sqrMagnitude > 0.1f)
                transform.rotation = Quaternion.LookRotation(rb.velocity.normalized);
        }

        private Vector3 CalculateBallisticVelocity(Vector3 target)
        {
            var dir = target - transform.position;
            var h = dir.y;
            dir.y = 0;
            var distance = dir.magnitude;
            var a = (throwAngle/throwForce) * Mathf.Deg2Rad;
            dir.y = distance * Mathf.Tan(a);
            distance += h / Mathf.Tan(a);
            var gravity = Mathf.Abs(Physics.gravity.y);
            var velocityMagnitude = Mathf.Sqrt(distance * gravity / Mathf.Sin(2 * a));
            return (velocityMagnitude * dir.normalized) * throwForce;
        }
    }
}
