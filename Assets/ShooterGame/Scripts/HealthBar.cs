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
        }

        private void Update()
        {
            transform.rotation = _myRotation;
        }

        public void UpdateHealthBar(float max, float current)
        {
            float maxVal = BackBar.rect.width;
            float newVal = maxVal * current / max;

            FillBar.sizeDelta = new Vector2(newVal, FillBar.sizeDelta.y);

        }
    }

}