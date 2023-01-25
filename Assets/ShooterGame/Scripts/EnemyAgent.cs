using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingGame
{
    public class EnemyAgent : MonoBehaviour
    {
        public Transform Target;
        private PlayerAgent _player;

        public Rigidbody RBody;
        public float MoveSpeed;

        public float RateOfAttack;
        private float _attackTimer;
        private bool _canAttack;

        public int AttackPower;

        private void Start()
        {
            _canAttack = true;
            _attackTimer = 0f;
            _player = Target.GetComponent<PlayerAgent>();
        }

        private void FixedUpdate()
        {
            transform.LookAt(Target);

            RBody.velocity = transform.forward * MoveSpeed * Time.fixedDeltaTime;
        }

        private void Update()
        {
            if (_attackTimer > 0f)
                _attackTimer -= Time.deltaTime;
            else
                _canAttack = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Bullet")
            {
                _player.EnemyKilled();

                ObjectPool.Instance.EnemyPool.Release(this);
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.collider.tag == "Player" && _canAttack)
            {
                Attack();
            }
        }

        private void Attack()
        {
            _canAttack = false;
            _attackTimer = RateOfAttack;

            _player.GetHurt(AttackPower);

        }
    }
}
