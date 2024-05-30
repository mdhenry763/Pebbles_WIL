using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementTest : MonoBehaviour
{
    [Header("References: ")]
    public Camera playerCamera;
    public PlayerInput input;
    public GameObject wrenchie;
    
    [Header("Settings: ")]
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float playerSpeed = 8f;
    public float mouseSensitivity = 100f;
    public LayerMask fireLayer;
    
    [Header("Debug")]
    public bool canMove = true;
    
    private Vector3 _moveInput = Vector3.zero;
    private Camera _mainCam;
    float rotationX = 0;
    
    CharacterController characterController;
    private Vector3 moveDirection;

    private void Awake()
    {
        _mainCam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        moveDirection = Vector3.zero;
    }

    private void LateUpdate()
    {
        Debug.Log(IsGrounded());
        
    }

    private void Update()
    {
        if(!canMove) return;
        HandleMouseLook();
        HandleMovementAndRotation();

        if (!IsGrounded())
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private bool IsGrounded()
    {
        Vector3 pos = transform.position;

        bool hit = Physics.Raycast(pos, Vector3.down, 1.25f);

        return hit;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        _moveInput = new Vector3(input.x, 0, input.y);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (IsGrounded() && canMove)
        {
            Debug.Log("Jumping");
            moveDirection.y = jumpPower;
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
        moveDirection.x = moveDir.x * playerSpeed;
        moveDirection.z = moveDir.z * playerSpeed;
    }
    
    private void HandleMouseLook()
    {
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
        Debug.Log("Disable Movement");
        canMove = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void EnableInput()
    {
        canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ActivateWrenchie()
    {
        wrenchie.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 1.225f);
    }
}
