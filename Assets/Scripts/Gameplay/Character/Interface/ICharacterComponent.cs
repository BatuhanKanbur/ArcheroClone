using System;

namespace Gameplay.Character.Interface
{
    public interface ICharacterComponent : IDisposable
    {
        public ICharacter Character { get; }
        public void Update();
        public void Reset();
    }
}
