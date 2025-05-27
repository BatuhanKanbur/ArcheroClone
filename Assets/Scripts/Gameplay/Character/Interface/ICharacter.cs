using System;
using Gameplay.Weapon.Interface;
using UnityEngine;

namespace Gameplay.Character.Interface
{
    public interface ICharacter : IDisposable
    {
        public bool IsInitialized { get; }
        public IWeapon CurrentWeapon { get; }
        public CharacterController CharacterController { get; }
        public Animator Animator { get; }
        public ICharacterAnimator Animation { get; }
        public ICharacterCombat Combat { get; }
        public ICharacterMovement Movement { get; }
        public ICharacterStatus Status { get; }
        public ICharacterSkillController SkillController { get; }
        public ITargetManager TargetManager { get; set; }
        public Vector3[] GetClosetTargetPositions(int targetCount);
        public void Initialize();
        public void OnEnable();
        public void OnDestroy();
        public void Move(Vector3 input);
    }
}
