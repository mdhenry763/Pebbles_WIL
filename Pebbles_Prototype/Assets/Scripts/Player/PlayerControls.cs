using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    public float moveSpeed = 0.1f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public float maxVelMagnitude = 200f;
    
    //Player Components
    private InputActions _controls;
    private Rigidbody _playerRb;
    private Transform _camTransform;
    
    //State
    private bool isGrounded;

    private void Awake()
    {
        _controls = new InputActions();
        _playerRb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _camTransform = Camera.main.transform;
    }

    private void OnEnable()
    {
        _controls.Enable();
        
        _controls.Player.Jump.performed += HandlePlayerJump;
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void Update()
    {
        //Sphere check
        isGrounded = Physics.SphereCast(transform.position, 0.5f, Vector3.down, out var raycastHit, 1, groundLayer);
        
        MovePlayer();
    }

    private void MovePlayer()
    {
        //Movement
        var input = _controls.Player.Move.ReadValue<Vector2>();
        var move = _camTransform.forward * input.y + _camTransform.right * input.x;
        move.y = 0;

        if(!isGrounded) return;
        transform.Translate(move * moveSpeed);
        //_playerRb.velocity += move * moveSpeed;
    }

    private void HandlePlayerJump(InputAction.CallbackContext obj)
    {
        if(!isGrounded) return;
        Debug.Log("Jump now boy");
        var jumpVel = Vector3.up * jumpForce;
        _playerRb.velocity += jumpVel;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
