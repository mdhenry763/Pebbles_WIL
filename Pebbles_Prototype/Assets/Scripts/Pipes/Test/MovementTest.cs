using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementTest : MonoBehaviour
{
    public Rigidbody playerRB;
    public float playerSpeed = 8f;
    public float rotateSpeed = 5f;
    public float jumpForce = 200f;

    public LayerMask fireLayer;

    private Vector3 _moveInput = Vector3.zero;
    private Camera _mainCam;

    private void Awake()
    {
        _mainCam = Camera.main;
    }

    private void FixedUpdate()
    {
        HandleMovementAndRotation();
    }

    private bool IsGrounded()
    {
        Vector3 pos = transform.position;

        bool hit = Physics.Raycast(pos, Vector3.down, 1f);

        return hit;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        _moveInput = new Vector3(input.x, 0, input.y);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!IsGrounded()) return;
        
        playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Force);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        RaycastHit hit;

        //casts ray to mous position
        Ray mousePos = _mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());

        //if there was a ray cast the player will look at it
        if (Physics.Raycast(mousePos, out hit, 100f, fireLayer))
        {
            Debug.Log($"Hit other {hit.collider.name}");
        }
    }

    private void HandleMovementAndRotation()
    {
        Vector3 moveDir = transform.forward * _moveInput.z;

        playerRB.velocity = moveDir * playerSpeed;

        Vector3 rotation = new Vector3(0, _moveInput.x, 0) * rotateSpeed;
        
        transform.Rotate(rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down);
    }
}
