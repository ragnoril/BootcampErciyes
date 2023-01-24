using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingGame
{
    public class FollowCam : MonoBehaviour
    {
        public Transform Target;
        private Vector3 FollowDistance;
        //public float FollowSpeed;

        // Start is called before the first frame update
        void Start()
        {
            FollowDistance = Target.position - transform.position;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            transform.position = Target.position - FollowDistance;
            //transform.position = Vector3.MoveTowards(transform.position, (Target.position - FollowDistance), FollowSpeed);
            //transform.position = Vector3.Lerp(transform.position, (Target.position - FollowDistance), FollowSpeed);
        }
    }

}