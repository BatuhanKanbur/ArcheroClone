namespace Gameplay.Damageable.Interface
{
    public interface IHealthBar
    {
        public void Init(float maxHealth);
        public void SetHealth(float health, float maxHealth);
        public void SetActive(bool active);
        public void Reset();
    }
}
