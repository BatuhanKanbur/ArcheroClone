using Cysharp.Threading.Tasks;
using Gameplay.Damageable.Structure;

namespace Gameplay.Damageable.Interface
{
    public interface IDamageable
    {
        public void TakeDamage(DamageStats damageStats);
        public UniTaskVoid DamageOverTime(DamageStats damageStats, float duration);
    }
}
