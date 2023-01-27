using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public int EnemyCount;

        public EventManager Events;
        public PlayerAgent Player;
        public UIManager UI;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            Events.OnEnemySpawned += AddNewEnemy;
            Events.OnEnemyKilled += RemoveNewEnemy;

            Events.GameStarted();
            UI.Init();
            Player.Init();
            EnemyCount = 0;
            Events.EnemyCountChanged(EnemyCount);
        }

        private void RemoveNewEnemy()
        {
            EnemyCount -= 1;
            Events.EnemyCountChanged(EnemyCount);
        }

        private void AddNewEnemy()
        {
            EnemyCount += 1;
            Events.EnemyCountChanged(EnemyCount);
        }
    }
}