using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ShootingGame
{
    public class Spawner : MonoBehaviour
    {
        public float SpawnCount;
        public float SpawnTimer;
        private float _spawnCooldown;

        public Transform Target;

        // Start is called before the first frame update
        void Start()
        {
            _spawnCooldown = SpawnTimer;
        }

        // Update is called once per frame
        void Update()
        {
            if (_spawnCooldown > 0f)
            {
                _spawnCooldown -= Time.deltaTime;
            }
            else
            {
                for (int i = 0; i < SpawnCount; i++)
                {
                    Vector3 spawnPos = transform.position + new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));
                    Spawn(spawnPos);
                }
                _spawnCooldown = SpawnTimer;
            }

        }

        private void Spawn(Vector3 pos)
        {
            //GameObject go = Instantiate(SpawnPrefab, pos, Quaternion.identity);
            EnemyAgent enemy = ObjectPool.Instance.EnemyPool.Get();
            enemy.transform.position = pos;
            enemy.transform.rotation = Quaternion.identity;
            enemy.Init(Target);
            GameManager.Instance.Events.EnemySpawned();
        }
    }
}