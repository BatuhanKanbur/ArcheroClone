using System;
using Gameplay.Character.Interface;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Player.Interface
{
    public interface IPlayer
    {
        public void Initialize(ITargetManager targetManager,Action onDeath);
        public ICharacter Character { get; }
        public void OnMove(InputAction.CallbackContext input);
        public void OnDeath();
        public Transform Transform { get; }
    }
}
