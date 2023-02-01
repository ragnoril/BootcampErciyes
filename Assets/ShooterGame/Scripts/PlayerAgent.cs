using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingGame
{
    public class PlayerAgent : MonoBehaviour
    {
        public UIManager UI;
        public HealthBar HPBar;

        public float MoveSpeed;
        public Rigidbody RBody;
        public Camera GameCam;

        public float RateOfFire;
        private float _shootingTimer;
        private bool _canShoot;

        public int MaxHP;
        public int CurrentHP;
        public int KillCount;

        private void OnDestroy()
        {
            GameManager.Instance.Events.OnEnemyKilled -= EnemyKilled;
            GameManager.Instance.Events.OnPlayerHit -= GetHurt;
        }

        // Start is called before the first frame update
        public void Init()
        {
            GameManager.Instance.Events.OnEnemyKilled += EnemyKilled;
            GameManager.Instance.Events.OnPlayerHit += GetHurt;

            RBody = GetComponent<Rigidbody>();
            _canShoot = true;
            _shootingTimer = 0f;
            CurrentHP = MaxHP;
            KillCount = 0;
            GameManager.Instance.Events.PlayerHealthChanged(MaxHP, CurrentHP);
            GameManager.Instance.Events.KillCountChanged(KillCount);


        }

        // Update is called once per frame
        void FixedUpdate()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 newVelocity = new Vector3(moveX, 0, moveZ) * MoveSpeed * Time.fixedDeltaTime;
            newVelocity.y = RBody.velocity.y;
            RBody.velocity = newVelocity;
            RBody.angularVelocity = Vector3.zero;
        }

        private void Update()
        {
            if (_shootingTimer > 0f)
                _shootingTimer -= Time.deltaTime;
            else
                _canShoot = true;

            Vector3 targetPos = GameCam.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * GameCam.transform.position.y);
            targetPos.y = transform.position.y;

            transform.LookAt(targetPos);

            if (Input.GetMouseButton(0) && _canShoot)
            {
                Fire();
            }
            
        }

        public void Fire()
        {
            Vector3 bulletPos = transform.position + transform.forward;
            BulletAgent bullet = ObjectPool.Instance.BulletPool.Get();

            bullet.transform.position = bulletPos;
            bullet.transform.rotation = Quaternion.identity;
            bullet.Fire(transform.forward);
            _canShoot = false;
            _shootingTimer = RateOfFire;

        }

        public void GetHurt(int damage)
        {
            CurrentHP -= damage;
            GameManager.Instance.Events.PlayerHealthChanged(MaxHP, CurrentHP);

            if (CurrentHP < 0)
                GameManager.Instance.Events.GameOver();

        }

        public void EnemyKilled()
        {
            KillCount += 1;
            GameManager.Instance.Events.KillCountChanged(KillCount);
        }

    }

}