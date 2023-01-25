using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace ShootingGame
{

    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool Instance { get; private set; }

        public IObjectPool<BulletAgent> BulletPool;
        public BulletAgent BulletPrefab;
        
        public IObjectPool<EnemyAgent> EnemyPool;
        public EnemyAgent EnemyPrefab;
        

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

            BulletPool = new ObjectPool<BulletAgent>(CreateBullet, GetBullet, ReleaseBullet, DestroyBullet);
            EnemyPool = new ObjectPool<EnemyAgent>(CreateEnemy, GetEnemy, ReleaseEnemy, DestroyEnemy);
        }

        private void DestroyEnemy(EnemyAgent obj)
        {
            Destroy(this.gameObject);
        }

        private void ReleaseEnemy(EnemyAgent obj)
        {
            obj.gameObject.SetActive(false);
        }

        private void GetEnemy(EnemyAgent obj)
        {
            obj.gameObject.SetActive(true);
        }

        private EnemyAgent CreateEnemy()
        {
            EnemyAgent enemy = Instantiate(EnemyPrefab);
            enemy.transform.SetParent(this.transform);

            return enemy;
        }

        private BulletAgent CreateBullet()
        {
            BulletAgent bullet = Instantiate(BulletPrefab);
            bullet.transform.SetParent(this.transform);

            return bullet;
        }

        private void DestroyBullet(BulletAgent obj)
        {
            Destroy(obj.gameObject);
        }

        private void ReleaseBullet(BulletAgent obj)
        {
            obj.gameObject.SetActive(false);
        }

        private void GetBullet(BulletAgent obj)
        {
            obj.gameObject.SetActive(true);
        }

        /*
        public List<GameObject> pooledObjects;
        public GameObject objectToPool;
        public int poolLimit;

        // Start is called before the first frame update
        void Start()
        {
            pooledObjects = new List<GameObject>();
            for (int i = 0; i < poolLimit; i++)
            {
                GameObject go = Instantiate(objectToPool);
                go.SetActive(false);
                go.transform.SetParent(transform);
                pooledObjects.Add(go);
            }

        }

        public GameObject GetPooledObject()
        {
            for (int i = 0; i < poolLimit; i++)
            {
                if (!pooledObjects[i].activeSelf)
                    return pooledObjects[i];
            }

            return null;
        }
        */
    }
}
