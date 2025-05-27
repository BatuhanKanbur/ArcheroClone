using Gamecore.MobManager.Interface;
using Gameplay.Character.Structure;
using UnityEngine;

namespace Gamecore.MobManager.Structure
{
    [CreateAssetMenu(fileName = "MobStats", menuName = "ScriptableObjects/MobStats", order = 1)]
    public class MobStats : CharacterStats , IMobStats
    {
        [SerializeField] private int earnedScore;
        public int EarnedScore => earnedScore;
        public MobStats(float movementSpeed, float rotationSpeed, float attackSpeed) : base(movementSpeed, rotationSpeed, attackSpeed)
        {
            
        }
    }
}
