using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementTest : MonoBehaviour
{
    public Camera playerCamera;
    public float jumpPower = 7f;
    public float gravity = 10f;
    
    public PlayerInput input;
    public float playerSpeed = 8f;
    public float mouseSensitivity = 100f;
    
    float rotationX = 0;

    public bool canMove = true;

    public LayerMask fireLayer;

    private Vector3 _moveInput = Vector3.zero;
    private Camera _mainCam;
    private float _rotationX = 0f;
    
    CharacterController characterController;

    private void Awake()
    {
        _mainCam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        HandleMouseLook();
    }

    private void FixedUpdate()
    {
        HandleMovementAndRotation();
    }

    private bool IsGrounded()
    {
        Vector3 pos = transform.position;

        bool hit = Physics.Raycast(pos, Vector3.down, 1.5f);

        return hit;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        _moveInput = new Vector3(input.x, 0, input.y);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (characterController.isGrounded)
        {
            characterController.Move(new Vector3(0, jumpPower, 0));
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        RaycastHit hit;

        //casts ray to mouse position
        Ray mousePos = _mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());

        //if there was a ray cast the player will look at it
        if (Physics.Raycast(mousePos, out hit, 100f, fireLayer))
        {
            Debug.Log($"Hit other {hit.collider.name}");
            hit.transform.GetComponent<PipeManager>().RotatePipe();
        }
    }

    private void HandleMovementAndRotation()
    {
        if(!canMove) return;
        
        Vector3 moveDir = (transform.forward * _moveInput.z) + (transform.right * _moveInput.x);
        characterController.Move(moveDir * playerSpeed);
    }
    
    private void HandleMouseLook()
    {
        // Vector2 mouseInput = Mouse.current.delta.ReadValue();
        // float mouseX = mouseInput.x * mouseSensitivity * Time.deltaTime;
        // float mouseY = mouseInput.y * mouseSensitivity * Time.deltaTime;
        //
        // _rotationX -= mouseY;
        // _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);
        
        if(!canMove) return;

        Vector2 mouseInput = Mouse.current.delta.ReadValue();

        rotationX += -mouseInput.y * mouseSensitivity;
        rotationX = Mathf.Clamp(rotationX, -90, 90);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, mouseInput.x * mouseSensitivity, 0);
        //_mainCam.transform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
    }

    public void DisableInput()
    {
        canMove = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void EnableInput()
    {
        canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down);
    }
}
