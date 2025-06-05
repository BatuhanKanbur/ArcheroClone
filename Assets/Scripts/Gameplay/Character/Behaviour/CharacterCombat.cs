using System.Linq;
using Gamecore.AnimatorBehaviour.Enums;
using Gamecore.AnimatorBehaviour.Structure;
using Gameplay.Character.Interface;
using Gameplay.Weapon.Interface;
using UnityEngine;
using EventType = Gamecore.AnimatorBehaviour.Enums.EventType;
using static UnityEngine.Object;


namespace Gameplay.Character.Behaviour
{
    using Gameplay.Weapon.Structure;
    public class CharacterCombat : ICharacterCombat
    {
        public ICharacter Character { get; }
        public IWeapon Weapon { get; private set; }
        public bool IsAttacking { get; private set; }
        private bool HasMoving => Character.Movement.HasMoving;
        private bool HasStunned => Character.Status.IsStunned;
        private float _attackTime;
        private int _currentClipHash;
        private Transform[] _closetDamageables;
        public CharacterCombat(ICharacter character,Weapon weapon)
        {
            Character = character;
            Weapon = Instantiate(weapon);
            Weapon.SpawnWeapon(Character.Animation.LeftHand, Character.Animation.RightHand).Forget();
            Character.Animation.Subscribe(AnimationType.AttackStart, OnAttackStartEvent);
            Character.Animation.Subscribe(AnimationType.AttackEnd, OnAttackEndEvent);
        }
        public void Update()
        {
            if(HasMoving || HasStunned) return;
            _attackTime += Time.deltaTime * Character.Status.Stats.AttackSpeed;
            if (!(_attackTime >= 5)) return;
            IsAttacking = true;
            _attackTime = 0;
            Attack();
        }
        private void Attack()
        {
            _closetDamageables = Character.GetClosetTargetPositions(Weapon.WeaponStats.BounceCount);
            if (_closetDamageables.Length == 0) return;
            Character.Animation.SetIKTargetPosition(_closetDamageables.First().position);
            Character.Animation.SetAttack();
        }
        private void OnAttackStartEvent(AnimatorEvent animationEvent)
        {
            switch (animationEvent.eventType)
            {
                case EventType.Start:
                    _currentClipHash = animationEvent.ClipHash;
                    Character.Animation.SetAimConstraintsActive(true);
                    break;
                case EventType.Update when _currentClipHash == animationEvent.ClipHash:
                    break;
                case EventType.End when _currentClipHash == animationEvent.ClipHash:
                    Weapon?.Attack(_closetDamageables);
                    _currentClipHash = 0;
                    break;
            }
        }
        private void OnAttackEndEvent(AnimatorEvent animationEvent)
        {
            switch (animationEvent.eventType)
            {
                case EventType.Start:
                case EventType.Update when _currentClipHash == animationEvent.ClipHash:
                    break;
                case EventType.End:
                    _currentClipHash = 0;
                    Character.Animation.SetAimConstraintsActive(false);
                    Weapon?.AttackEnd();
                    IsAttacking = false;
                    break;
            }
        }

        public void Reset() { }
        public void Dispose()
        {
            Character.Animation.Unsubscribe(AnimationType.AttackStart, OnAttackStartEvent);
            Character.Animation.Unsubscribe(AnimationType.AttackEnd, OnAttackEndEvent);
            _attackTime = -1000;
        }
    }
}
