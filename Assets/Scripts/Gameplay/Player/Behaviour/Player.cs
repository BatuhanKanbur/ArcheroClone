using System;
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
    public class Player : Character, IPlayer, IDamageable, InputSystem.IPlayerActions
    {
        private InputSystem _inputSystem;
        private Camera _mainCamera;
        public Transform Transform => Animation.Spine;
        public ICharacter Character => this;
        private Action _onDeath;
        public void Initialize(ITargetManager targetManager, Action onDeath)
        {
            _mainCamera = Camera.main;
            TargetManager = targetManager;
            _inputSystem = new InputSystem();
            _inputSystem.Player.SetCallbacks(this);
            _inputSystem.Player.Enable();
            base.Initialize();
            _onDeath = onDeath;
            Status.OnDeath += OnDeath;
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            Movement?.SetMovementInput(GetMovementDirection(context.ReadValue<Vector2>()));
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

        public void OnDeath()
        {
            _onDeath?.Invoke();
            Dispose();
        }
        protected override void OnDestroy()
        {
            Status.OnDeath -= OnDeath;
            _inputSystem.Player.Disable();
            _inputSystem.Player.SetCallbacks(null);
            _inputSystem.Dispose();
            base.OnDestroy();
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
