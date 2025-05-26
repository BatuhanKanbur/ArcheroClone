using UnityEngine.InputSystem;

namespace Gameplay.Player.Interface
{
    public interface IPlayer
    {
        public void Initialize();
        public void OnMove(InputValue input);
    }
}
