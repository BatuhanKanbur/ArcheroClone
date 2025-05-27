using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Particle
{
    public class ParticleAutoDisable : MonoBehaviour
    {
        private async void OnEnable()
        {
            await UniTask.Delay(1500);
            gameObject.SetActive(false);
        }
    }
}
