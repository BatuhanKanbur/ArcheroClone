using System;
using Gamecore.AnimatorBehaviour.Enums;
using Gamecore.AnimatorBehaviour.Structure;
using UnityEngine;
using EventType = Gamecore.AnimatorBehaviour.Enums.EventType;

namespace Gamecore.AnimatorBehaviour.Behaviour
{
    public class AnimatorStateMachine : StateMachineBehaviour
    {
        public AnimationType animationType = AnimationType.Idle;
        public AnimatorLayer targetLayer = AnimatorLayer.None;
        private Action<AnimatorEvent> _onEvent;
        private bool _hasEnded;
        public void AddListener(Action<AnimatorEvent> onEvent) => _onEvent += onEvent;
        public void RemoveListener(Action<AnimatorEvent> onEvent) => _onEvent -= onEvent;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var currentLayerName = animator.GetLayerName(layerIndex);
            if (!IsMatchingLayer(currentLayerName)) return;
            _hasEnded = false;
            _onEvent?.Invoke(new AnimatorEvent(stateInfo.shortNameHash, EventType.Start));
            base.OnStateEnter(animator, stateInfo, layerIndex);
        }
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var currentLayerName = animator.GetLayerName(layerIndex);
            if (!IsMatchingLayer(currentLayerName)) return;
            _onEvent?.Invoke(new AnimatorEvent(stateInfo.shortNameHash, EventType.Update, stateInfo.normalizedTime));
            if (!_hasEnded && stateInfo.normalizedTime >= 1f)
            {
                _hasEnded = true;
                _onEvent?.Invoke(new AnimatorEvent(stateInfo.shortNameHash, EventType.End,1f));
            }
            base.OnStateUpdate(animator, stateInfo, layerIndex);
        }
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var currentLayerName = animator.GetLayerName(layerIndex);
            if (!IsMatchingLayer(currentLayerName)) return;
            _hasEnded = true;
            _onEvent?.Invoke(new AnimatorEvent(stateInfo.shortNameHash, EventType.End,1f));
            base.OnStateExit(animator, stateInfo, layerIndex);
        }
        private bool IsMatchingLayer(string currentLayerName)
        {
            return currentLayerName.Equals(targetLayer.ToString(), StringComparison.OrdinalIgnoreCase);
        }
    }
}
