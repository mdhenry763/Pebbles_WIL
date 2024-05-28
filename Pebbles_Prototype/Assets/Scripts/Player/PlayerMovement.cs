using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController _Controller;

    public float speed = 12f, gravity = 9.81f, jumpHeight = 3f;

    private Vector3 velocity;

    public Transform groundCheck;
    
    public float groundDistance = 0.4f;

    public LayerMask groundMask;

    private bool isGrounded;
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");

        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        _Controller.Move(move * speed * Time.deltaTime);
        
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
        }

        velocity.y -= gravity * Time.deltaTime;

        _Controller.Move(velocity * Time.deltaTime);

       
        
    }
    
}





