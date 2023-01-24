using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingGame
{
    public class BulletAgent : MonoBehaviour
    {
        public Rigidbody RBody;
        public float MoveSpeed;

        public void Fire(Vector3 direction)
        {
            RBody.AddForce(direction * MoveSpeed, ForceMode.Impulse);
        }
    }

}