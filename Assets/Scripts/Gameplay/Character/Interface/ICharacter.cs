using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Character.Interface
{
    public interface ICharacter : IDisposable
    {
        public bool IsInitialized { get; }
        public CharacterController CharacterController { get; }
        public Animator Animator { get; }
        public ICharacterAnimator Animation { get; }
        public ICharacterCombat Combat { get; }
        public ICharacterMovement Movement { get; }
        public ICharacterStatus Status { get; }
        public void Initialize();
        public void OnEnable();
        public void OnDestroy();
        public void Move(InputValue input);
        public void Attack();
    }
}
