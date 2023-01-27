using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingGame
{
    public class MousePointer : MonoBehaviour
    {
        public Camera GameCam;

        // Update is called once per frame
        void Update()
        {
            Vector3 targetPos = GameCam.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * GameCam.transform.position.y);
            targetPos.y = 0.01f;

            transform.position = targetPos;
        }
    }

}