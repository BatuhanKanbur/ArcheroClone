using System;
using UnityEngine;
using EventType = Gamecore.AnimatorBehaviour.Enums.EventType;

namespace Gamecore.AnimatorBehaviour.Structure
{
    [Serializable]
    public class AnimatorEvent
    {
        public int ClipHash{ get; private set; }
        public EventType eventType;
        public float targetTime;
        [HideInInspector]public float currentTime;
        public bool HasTriggered { get; private set; }
        public void SetClipHash(int hash) =>  ClipHash = hash;
        public void SetTime(float time) => currentTime = time;
        public void SetTrigger(bool trigger) => HasTriggered = trigger;

        public AnimatorEvent(int clipHash,EventType eventType,float currentTime=0f)
        {
            ClipHash = clipHash;
            this.eventType = eventType;
            this.currentTime = currentTime;
        }
    }
}