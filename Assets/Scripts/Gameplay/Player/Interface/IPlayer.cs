using Gamecore.MobManager.Interface;
using Gameplay.Character.Interface;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Player.Interface
{
    public interface IPlayer
    {
        public void Initialize(ITargetManager targetManager);
        public ICharacter Character { get; }
        public void OnMove(InputValue input);
        public Transform Transform { get; }
    }
}
