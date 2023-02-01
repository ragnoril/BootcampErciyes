using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlatformerGame
{
    public class PlayerAgent : MonoBehaviour
    {
        public Rigidbody2D RBody;
        public SpriteRenderer SRenderer;
        public Animator AnimController;

        public float JumpSpeed;
        public float MoveSpeed;
        public float DashFactor;

        public Transform TouchGround;
        public bool IsGrounded;
        public LayerMask GroundLayer;

        public int Coins;

        public GameObject Confetti;

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

            float dashAmount = 1f;
            if (Input.GetKey(KeyCode.LeftShift))
                dashAmount = DashFactor;

            Vector2 newVel = new Vector2(moveX * MoveSpeed * dashAmount * Time.fixedDeltaTime, RBody.velocity.y);
            RBody.velocity = newVel;

            AnimController.SetBool("IsGrounded", IsGrounded);

            if (RBody.velocity.x != 0f)
                AnimController.SetBool("IsWalking", true);
            else
                AnimController.SetBool("IsWalking", false);

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Finish")
            {
                Debug.Log("Level Finished!");
                Instantiate(Confetti, new Vector3(0f, -4f, 0f), Quaternion.identity);
            }

            if (collision.tag == "FallPlace")
            {
                SceneManager.LoadScene("GameScene");
            }

            if (collision.tag == "PickUp")
            {
                var item = collision.GetComponent<ItemAgent>();
                if (item.Type == ItemType.Coin)
                {
                    Coins += item.Amount;
                }
                Destroy(item.gameObject);
            }
        }
    }
}
