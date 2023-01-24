using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingGame
{
    public class EnemyAgent : MonoBehaviour
    {
        public Transform Target;

        public Rigidbody RBody;
        public float MoveSpeed;

        private void FixedUpdate()
        {
            transform.LookAt(Target);

            RBody.velocity = transform.forward * MoveSpeed * Time.fixedDeltaTime;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Player")
            {
                Debug.Log("ATTACK!");
            }

            if (collision.collider.tag == "Bullet")
            {
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}
