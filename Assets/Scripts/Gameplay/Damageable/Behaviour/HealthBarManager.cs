using System;
using System.Globalization;
using Gameplay.Damageable.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Damageable.Behaviour
{
    public class HealthBarManager : MonoBehaviour, IHealthBar
    {
        [SerializeField] private GameObject healthBarPrefab;
        [SerializeField] private Image healthBarImage;
        [SerializeField] private Text healthText;
        public void Init(float maxHealth)
        {
            healthBarImage.fillAmount = 1f;
            healthText.text = maxHealth.ToString(CultureInfo.InvariantCulture);
            if(Camera.main)
                healthBarPrefab.transform.forward = Camera.main.transform.forward;
        }
        public void SetHealth(float health, float maxHealth)
        {
            healthText.text = health.ToString(CultureInfo.InvariantCulture);
            healthBarImage.fillAmount = health / maxHealth;
        }
        public void Reset()
        {
            healthBarImage.fillAmount = 1f;
            healthText.text = string.Empty;
        }
    }
}
