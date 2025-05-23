using Gameplay.Player.Interface;
using UnityEngine;

namespace Gameplay.Player.Behaviour
{
    public class PlayerCombat : IPlayerCombat
    {
        public IPlayer Player { get; }
        private bool HasMoving => Player.Movement.HasMoving;
        private float _attackTime;
        public PlayerCombat(IPlayer player)
        {
            Player = player;
        }
        public void Update()
        {
            if(HasMoving) return;
            _attackTime += Time.deltaTime;
            if (_attackTime >= Player.Status.Stats.AttackSpeed)
            {
                _attackTime = 0;
                Player.OnAttack();
            }
        }

        public void Reset()
        {
        }

        public void Dispose()
        {
        }
    }
}
