using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Animations;

namespace Gameplay.Character.Behaviour
{
    [RequireComponent(typeof(AimConstraint))]
    public class CharacterAimController : MonoBehaviour
    {
        private AimConstraint _aimConstraint;
        private float _activeWeight = 0f;
        private CancellationTokenSource _cts;
        private void Awake()
        {
            _aimConstraint = GetComponent<AimConstraint>();
            _activeWeight = _aimConstraint.weight;
            _aimConstraint.weight = 0f;
            _aimConstraint.constraintActive = false;
        }

        public void SetActive(bool active,float duration)
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            var targetWeight = active ? _activeWeight : 0f;
            SmoothWeightChangeAsync(targetWeight, duration, _cts.Token).Forget();
        }
        private async UniTask SmoothWeightChangeAsync(float targetWeight, float duration, CancellationToken token)
        {
            var startWeight = _aimConstraint.weight;
            var time = 0f;
            if(targetWeight > 0)
                _aimConstraint.constraintActive = true;
            while (time < duration)
            {
                token.ThrowIfCancellationRequested();
                time += Time.deltaTime;
                var t = time / duration;
                var newWeight = Mathf.Lerp(startWeight, targetWeight, t);
                _aimConstraint.weight = newWeight;
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }
            _aimConstraint.weight = targetWeight;
            _aimConstraint.constraintActive = targetWeight > 0f;
        }
        private void OnDestroy()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }
    }
}
