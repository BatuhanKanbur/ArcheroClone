using Gameplay.Character.Interface;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Player.Interface
{
    public interface IPlayer
    {
        public void Initialize(ITargetManager targetManager);
        public ICharacter Character { get; }
        public void OnMove(InputAction.CallbackContext input);
        public Transform Transform { get; }
    }
}
