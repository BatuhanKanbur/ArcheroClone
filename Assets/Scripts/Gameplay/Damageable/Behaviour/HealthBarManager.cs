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
        private Transform _cameraTransform;
        public void Init(float maxHealth)
        {
            healthBarImage.fillAmount = 1f;
            healthText.text = maxHealth.ToString(CultureInfo.InvariantCulture);
            if(Camera.main && _cameraTransform ==null)
                _cameraTransform = Camera.main.transform;
        }

        private void Update()
        {
            if(Time.frameCount % 30 != 0) return;
            if(!_cameraTransform) return;
            healthBarPrefab.transform.forward = _cameraTransform.transform.forward;
        }

        public void SetActive(bool active)
        {
            healthBarPrefab.SetActive(active);
        }
        public void SetHealth(float health, float maxHealth)
        {
            SetActive(true);
            healthText.text = health.ToString(CultureInfo.InvariantCulture);
            healthBarImage.fillAmount = health / maxHealth;
        }
        public void Reset()
        {
            SetActive(true);
            healthBarImage.fillAmount = 1f;
            healthText.text = string.Empty;
        }
    }
}
