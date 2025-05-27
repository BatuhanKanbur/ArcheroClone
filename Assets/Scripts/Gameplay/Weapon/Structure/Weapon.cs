using Cysharp.Threading.Tasks;
using Gamecore.AssetManager;
using Gamecore.AssetManager.Behaviour;
using Gamecore.ObjectManager;
using Gameplay.Skill.Interface;
using Gameplay.Skill.Structure;
using Gameplay.Weapon.Interface;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Gameplay.Weapon.Structure
{
    [CreateAssetMenu(fileName = "WeaponObject", menuName = "ScriptableObjects/WeaponObject", order = 1)]
    public class Weapon : ScriptableObject, IWeapon, ISkillable
    {
        [SerializeField] private WeaponStats weaponStats;
        [SerializeField] private AssetReference leftHandObject,rightHandObject;
        [SerializeField] private AssetReference throwableObject;
        public WeaponStats WeaponStats { get; private set; }
        public IWeaponObject LeftHandObject { get; private set; }
        public IWeaponObject RightHandObject { get; private set; }
        public Transform GetSpawnPoint => LeftHandObject != null ? LeftHandObject.SpawnPoint : RightHandObject?.SpawnPoint;
        
        public async UniTaskVoid SpawnWeapon(Transform leftHand, Transform rightHand)
        {
            WeaponStats = Instantiate(weaponStats);
            if (leftHandObject.IsAssigned() && leftHand)
            {
                var leftHandPrefab = await ObjectManager.GetObject(leftHandObject);
                LeftHandObject = leftHandPrefab.GetComponent<IWeaponObject>();
                LeftHandObject?.SetParent(leftHand);
            }
            if (rightHandObject.IsAssigned() && rightHand)
            {
                var rightHandPrefab = await ObjectManager.GetObject(rightHandObject);
                RightHandObject = rightHandPrefab.GetComponent<IWeaponObject>();
                RightHandObject?.SetParent(rightHand);
            }
        }
        public void Attack(Vector3[] closetDamageables)
        {
            RightHandObject?.SetActive(false);
            SpawnThrowable(closetDamageables).Forget();
        }
        public void AttackEnd()
        {
            RightHandObject?.SetActive(true);
        }
        public async UniTaskVoid SpawnThrowable(Vector3[] closetDamageables)
        {
            if (!throwableObject.IsAssigned()) return;
            for (var i = 0; i < WeaponStats.ArrowCount; i++)
            {
                var throwable = await ObjectManager.GetObject(throwableObject, GetSpawnPoint.position, GetSpawnPoint.rotation);
                var throwableComponent = throwable.GetComponent<IThrowable>();
                throwableComponent?.Init(WeaponStats,closetDamageables);
                await UniTask.Delay(200);
            }
        }
        public void ApplySkill(StatsData[] skillStats)
        {
            foreach (var modifier in skillStats) 
                if(modifier is WeaponStats playerStats)
                    WeaponStats += playerStats;
        }
        public void RemoveSkill(StatsData[] skillStats)
        {
            foreach (var modifier in skillStats) 
                if(modifier is WeaponStats playerStats)
                    WeaponStats -= playerStats;
        }
    }
}
