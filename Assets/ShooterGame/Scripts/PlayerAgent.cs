using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingGame
{
    public class PlayerAgent : MonoBehaviour
    {
        public float MoveSpeed;
        public Rigidbody RBody;
        public Camera GameCam;

        public GameObject BulletPrefab;

        public float RateOfFire;
        private float _shootingTimer;
        private bool _canShoot;

        // Start is called before the first frame update
        void Start()
        {
            RBody = GetComponent<Rigidbody>();
            _canShoot = true;
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

            Vector3 targetPos = GameCam.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 25f);
            targetPos.y = transform.position.y;

            transform.LookAt(targetPos);

            if (Input.GetMouseButton(0) && _canShoot)
            {
                Vector3 bulletPos = transform.position + transform.forward;
                GameObject go = Instantiate(BulletPrefab, bulletPos, Quaternion.identity);
                go.GetComponent<BulletAgent>().Fire(transform.forward);
                _canShoot = false;
                _shootingTimer = RateOfFire;
            }
            
        }
    }

}