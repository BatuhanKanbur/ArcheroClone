using Gamecore.AnimatorBehaviour.Enums;
using Gamecore.Character.Structure;
using Gameplay.Character.Interface;
using Gameplay.Weapon.Interface;
using UnityEngine;
using EventType = Gamecore.AnimatorBehaviour.Enums.EventType;

namespace Gameplay.Character.Behaviour
{
    public class CharacterCombat : ICharacterCombat
    {
        public ICharacter Character { get; }
        public IWeapon Weapon { get; private set; }
        private bool HasMoving => Character.Movement.HasMoving;
        private float _attackTime;
        private int _currentClipHash;
        public CharacterCombat(ICharacter character,IWeapon weapon)
        {
            Character = character;
            Weapon = weapon;
            Weapon.SpawnWeapon(Character.Animation.LeftHand, Character.Animation.RightHand).Forget();
            Character.Animation.Subscribe(AnimationType.Attack, OnAttackEvent);
        }
        public void Update()
        {
            if(HasMoving) return;
            _attackTime += Time.deltaTime * Character.Status.Stats.AttackSpeed;
            if (!(_attackTime >= 5)) return;
            _attackTime = 0;
            Attack();
        }
        private void Attack()
        {
            Character.Animation.SetAttack();
        }
        private void OnAttackEvent(AnimatorEvent animationEvent)
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
                    _currentClipHash = 0;
                    Character.Animation.SetAimConstraintsActive(false);
                    break;
            }
        }

        public void Reset() { }
        public void Dispose()
        {
            Character.Animation.Unsubscribe(AnimationType.Attack, OnAttackEvent);
        }
    }
}
