using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingGame
{
    public class EnemyAgent : MonoBehaviour
    {
        public Transform Target;
        private PlayerAgent _player;

        public Animator AnimController;

        public Rigidbody RBody;
        public float MoveSpeed;

        public float RateOfAttack;
        private float _attackTimer;
        private bool _canAttack;

        public int AttackPower;

        public bool _isDead;

        public void Init(Transform target)
        {
            Target = target;
            _isDead = false;
            _canAttack = true;
            _attackTimer = 0f;
            _player = Target.GetComponent<PlayerAgent>();
        }

        private void FixedUpdate()
        {
            if (_isDead) return;

            transform.LookAt(Target);

            RBody.velocity = transform.forward * MoveSpeed * Time.fixedDeltaTime;

            if (RBody.velocity.magnitude > 0.1f)
                AnimController.SetBool("IsMoving", true);
            else
                AnimController.SetBool("IsMoving", false);
        }

        private void Update()
        {
            if (_attackTimer > 0f)
                _attackTimer -= Time.deltaTime;
            else
                _canAttack = true;
        }

        IEnumerator KillSequence()
        {
            _isDead = true;

            RBody.velocity = Vector3.zero;
            RBody.angularVelocity = Vector3.zero;

            AnimController.SetBool("IsDead", true);
            yield return new WaitForSeconds(1.5f);
            ObjectPool.Instance.EnemyPool.Release(this);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Bullet")
            {
                StartCoroutine(KillSequence());
                GameManager.Instance.Events.EnemyKilled();
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

            AnimController.SetTrigger("Attack");
            GameManager.Instance.Events.PlayerGotHit(AttackPower);
        }

    }
}
