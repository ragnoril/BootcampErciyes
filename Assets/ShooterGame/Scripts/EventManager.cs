using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingGame
{

    public class EventManager : MonoBehaviour
    {
        public event Action OnGameStarted;
        public event Action OnGameEnded;

        public event Action<int> OnPlayerHit;
        
        public event Action OnEnemySpawned;
        public event Action OnEnemyKilled;

        public event Action<int> OnKillCountChanged;
        public event Action<int, int> OnPlayerHealthChanged;
        public event Action<int> OnEnemyCountChanged;

        public void GameStarted()
        {
            OnGameStarted?.Invoke();
        }

        public void GameOver()
        {
            OnGameEnded?.Invoke();
        }

        public void PlayerGotHit(int damage)
        {
            OnPlayerHit?.Invoke(damage);
        }

        public void EnemySpawned()
        {
            OnEnemySpawned?.Invoke();
        }

        public void EnemyKilled()
        {
            OnEnemyKilled?.Invoke();
        }

        public void KillCountChanged(int killCount)
        {
            OnKillCountChanged?.Invoke(killCount);
        }

        public void PlayerHealthChanged(int max, int current)
        {
            OnPlayerHealthChanged?.Invoke(max, current);
        }

        public void EnemyCountChanged(int enemyCount)
        {
            OnEnemyCountChanged?.Invoke(enemyCount);
        }
    }
}