using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingGame
{
    public class BulletAgent : MonoBehaviour
    {
        public Rigidbody RBody;
        public float MoveSpeed;

        private void ResetMe()
        {
            RBody.velocity = Vector3.zero;
            RBody.angularVelocity = Vector3.zero;
        }

        public void Fire(Vector3 direction)
        {
            RBody.AddForce(direction * MoveSpeed, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            ResetMe();
            ObjectPool.Instance.BulletPool.Release(this);
        }
    }

}