using System;
using UnityEngine;

namespace Gameplay.Player.Interface
{
    public interface IPlayerComponent : IDisposable
    {
        public IPlayer Player { get; }
        public void Update();
        public void Reset();
    }
}
