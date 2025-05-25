using System;
using Gamecore.AnimatorBehaviour.Enums;
using Gamecore.Character.Structure;
using Gameplay.Player.Interface;
using UnityEngine;
using EventType = Gamecore.AnimatorBehaviour.Enums.EventType;

namespace Gameplay.Player.Behaviour
{
    public class PlayerCombat : IPlayerCombat
    {
        public IPlayer Player { get; }
        private bool HasMoving => Player.Movement.HasMoving;
        private float _attackTime;
        private int _currentClipHash;
        public PlayerCombat(IPlayer player)
        {
            Player = player;
            Player.Animation.Subscribe(AnimationType.Attack, OnAttackEvent);
        }
        public void Update()
        {
            if(HasMoving) return;
            _attackTime += Time.deltaTime * Player.Status.Stats.AttackSpeed;
            if (!(_attackTime >= 5)) return;
            _attackTime = 0;
            Player.OnAttack();
        }
        private void Attack()
        {
            Player.Animation.SetAttack();
        }
        private void OnAttackEvent(AnimatorEvent animationEvent)
        {
            switch (animationEvent.eventType)
            {
                case EventType.Start:
                    _currentClipHash = animationEvent.ClipHash;
                    break;
                case EventType.Update when _currentClipHash == animationEvent.ClipHash:
                    break;
                case EventType.End when _currentClipHash == animationEvent.ClipHash:
                    _currentClipHash = 0;
                    break;
            }
        }

        public void Reset() { }
        public void Dispose()
        {
            Player.Animation.Unsubscribe(AnimationType.Attack, OnAttackEvent);
        }
    }
}
