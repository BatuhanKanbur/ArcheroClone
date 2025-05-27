using Cysharp.Threading.Tasks;
using Gameplay.Character.Interface;
using Gameplay.Damageable.Interface;
using Gameplay.Damageable.Structure;
using Gameplay.Player.Interface;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Player.Behaviour
{
    using Gameplay.Character.Behaviour;
    public class Player : Character, IPlayer, IDamageable
    {
        private Camera _mainCamera;
        public Transform Transform => transform;
        public ICharacter Character => this;

        public void Initialize(ITargetManager targetManager)
        {
            _mainCamera = Camera.main;
            TargetManager = targetManager;
            base.Initialize();
        }
        public void OnMove(InputValue input)
        {
            Movement?.SetMovementInput(GetMovementDirection(input.Get<Vector2>()));
        }
        private Vector3 GetMovementDirection(Vector2 moveInput)
        {
            var camForward = _mainCamera.transform.forward;
            var camRight = _mainCamera.transform.right;
            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();
            var desiredMove = camForward * moveInput.y + camRight * moveInput.x;
            return desiredMove;
        }

        public void TakeDamage(DamageStats damageStats)
        {
            Status.OnHit(damageStats.Damage);
        }

        public UniTaskVoid DamageOverTime(DamageStats damageStats, float duration)
        {
            return default;
        }
    }
}
