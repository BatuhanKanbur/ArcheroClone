using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Player.Interface
{
    public interface IPlayer : IDisposable
    {
        public bool IsInitialized { get; }
        public CharacterController CharacterController { get; }
        public Animator Animator { get; }
        public IPlayerAnimator Animation { get; }
        public IPlayerCombat Combat { get; }
        public IPlayerMovement Movement { get; }
        public IPlayerStatus Status { get; }
        public void Initialize();
        public void OnEnable();
        public void OnDestroy();
        public void OnMove(InputValue input);
        public void OnAttack();
    }
}
