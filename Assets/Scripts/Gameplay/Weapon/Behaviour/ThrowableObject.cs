using Gameplay.Damageable.Interface;
using Gameplay.Damageable.Structure;
using Gameplay.Weapon.Interface;
using UnityEngine;

namespace Gameplay.Weapon.Behaviour
{
    public class ThrowableObject : MonoBehaviour, IThrowable
    {
        public IWeaponStats WeaponStats { get; set; }
        [SerializeField] private Rigidbody rb;
        [SerializeField] private GameObject fireEffect;
        [SerializeField] private float throwForce = 5f;

        private int _bounceCount;
        private int _currentTargetIndex;
        private Transform[] _targetPositions;
        public void Init(IWeaponStats weapon,Transform[] targetPositions)
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
        private void LaunchToTarget(Transform target)
        {
            var velocity = CalculateBallisticVelocity(target.position);
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

        private Vector3 CalculateBallisticVelocity(Vector3 targetPosition)
        {
            var dir = targetPosition - transform.position;
            var dirXZ = new Vector3(dir.x, 0, dir.z);
            var x = dirXZ.magnitude;
            var y = dir.y;
            var g = Mathf.Abs(Physics.gravity.y);
            var speed = throwForce;
            var finalSpeed = speed;
            while (finalSpeed <= 300)
            {
                var s2 = finalSpeed * finalSpeed;
                var disc = s2 * s2 - g * (g * x * x + 2 * y * s2);
                if (disc >= 0f)
                {
                    var sqrtDisc = Mathf.Sqrt(disc);
                    var angle = Mathf.Atan((s2 - sqrtDisc) / (g * x));
                    var velocity = dirXZ.normalized * finalSpeed * Mathf.Cos(angle);
                    velocity.y = finalSpeed * Mathf.Sin(angle);
                    return velocity;
                }
                finalSpeed += 0.5f;
            }
            return (dir.normalized * finalSpeed);
        }
    }
}
