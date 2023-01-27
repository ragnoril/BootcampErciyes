using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

namespace ShootingGame
{
    public class UIManager : MonoBehaviour
    {
        public TMP_Text HitPointText;
        public TMP_Text KillCountText;
        public TMP_Text EnemyCountText;

        public GameObject GameOverCanvas;

        public void Init()
        {
            GameManager.Instance.Events.OnPlayerHealthChanged += UpdateHitPoints;
            GameManager.Instance.Events.OnKillCountChanged += UpdateKillCount;
            GameManager.Instance.Events.OnGameEnded += ShowGameOverCanvas;
            GameManager.Instance.Events.OnEnemyCountChanged += UpdateEnemyCount;
        }

        private void OnDestroy()
        {
            GameManager.Instance.Events.OnPlayerHealthChanged -= UpdateHitPoints;
            GameManager.Instance.Events.OnKillCountChanged -= UpdateKillCount;
            GameManager.Instance.Events.OnGameEnded -= ShowGameOverCanvas;
            GameManager.Instance.Events.OnEnemyCountChanged -= UpdateEnemyCount;
        }

        private void UpdateEnemyCount(int enemyCount)
        {
            EnemyCountText.text = enemyCount.ToString();
        }

        public void UpdateHitPoints(int maxHP, int currentHP)
        {
            HitPointText.text = currentHP.ToString() + "/" + maxHP.ToString();
        }

        public void UpdateKillCount(int count)
        {
            KillCountText.text = count.ToString();
        }

        public void PlayAgain()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void GotoMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void ShowGameOverCanvas()
        {
            GameOverCanvas.SetActive(true);
        }
    }
}