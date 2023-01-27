using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingGame
{
    public class HealthBar : MonoBehaviour
    {
        public RectTransform BackBar;
        public RectTransform FillBar;

        Quaternion _myRotation;

        private void Start()
        {
            _myRotation = transform.rotation;

            GameManager.Instance.Events.OnPlayerHealthChanged += UpdateHealthBar;

        }

        private void OnDestroy()
        {
            GameManager.Instance.Events.OnPlayerHealthChanged -= UpdateHealthBar;
        }

        private void Update()
        {
            transform.rotation = _myRotation;
        }

        public void UpdateHealthBar(int max, int current)
        {
            float maxVal = BackBar.rect.width;
            float newVal = maxVal * current / max;

            FillBar.sizeDelta = new Vector2(newVal, FillBar.sizeDelta.y);
        }
    }

}