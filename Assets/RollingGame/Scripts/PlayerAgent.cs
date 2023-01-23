using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAgent : MonoBehaviour
{

    public float MoveSpeed;
    public float JumpSpeed;
    public Rigidbody rigidbody;
    public bool IsGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 moveVec = new Vector3(moveX, 0, moveZ) * MoveSpeed * Time.fixedDeltaTime;

        rigidbody.AddForce(moveVec);

        if (Input.GetKey(KeyCode.Space) && IsGrounded)
        {
            rigidbody.AddForce(0, JumpSpeed, 0);
            IsGrounded = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            IsGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            Debug.Log("Game Over");
        }
    }



}
