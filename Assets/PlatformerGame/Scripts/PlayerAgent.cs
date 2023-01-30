using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerGame
{
    public class PlayerAgent : MonoBehaviour
    {
        public Rigidbody2D RBody;
        public SpriteRenderer SRenderer;
        public Animator AnimController;

        public float JumpSpeed;
        public float MoveSpeed;

        public Transform TouchGround;
        public bool IsGrounded;
        public LayerMask GroundLayer;



        void Start()
        {
            RBody = GetComponent<Rigidbody2D>();
            SRenderer = GetComponent<SpriteRenderer>();
            AnimController = GetComponent<Animator>();
        }
        
        void FixedUpdate()
        {
            IsGrounded = Physics2D.OverlapPoint(TouchGround.position, GroundLayer);

            float moveX = Input.GetAxis("Horizontal");
            
            if (moveX < 0f)
                SRenderer.flipX = true;
            else
                SRenderer.flipX = false;
            
            if (IsGrounded && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
            {
                RBody.AddForce(Vector2.up * JumpSpeed, ForceMode2D.Impulse);
            }


            Vector2 newVel = new Vector2(moveX * MoveSpeed * Time.fixedDeltaTime, RBody.velocity.y);
            RBody.velocity = newVel;

            AnimController.SetBool("IsGrounded", IsGrounded);

            if (RBody.velocity.x != 0f)
                AnimController.SetBool("IsWalking", true);
            else
                AnimController.SetBool("IsWalking", false);

        }
    }
}
