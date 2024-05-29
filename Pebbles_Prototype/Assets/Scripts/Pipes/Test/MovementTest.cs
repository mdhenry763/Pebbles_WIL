using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementTest : MonoBehaviour
{
    public PlayerInput input;
    public Rigidbody playerRB;
    public float playerSpeed = 8f;
    public float rotateSpeed = 5f;
    public float jumpForce = 200f;
    public float mouseSensitivity = 100f;

    public LayerMask fireLayer;
    public CinemachineVirtualCamera _cinemachineCam;

    private Vector3 _moveInput = Vector3.zero;
    private Camera _mainCam;
    private float _rotationX = 0f;
    private CinemachineComposer _composer;

    private void Awake()
    {
        _mainCam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        _composer = _cinemachineCam.GetCinemachineComponent<CinemachineComposer>();
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
            hit.transform.GetComponent<PipeManager>().RotatePipe();
        }
    }

    private void HandleMovementAndRotation()
    {
        if(!IsGrounded()) return;
        
        Vector3 moveDir = (transform.forward * _moveInput.z) + (transform.right * _moveInput.x);
        moveDir.y = playerRB.velocity.y;
        playerRB.velocity = moveDir * playerSpeed ;
    }

    public float upDownSensitivity = 0.2f;
    
    private void HandleMouseLook()
    {
        Vector2 mouseInput = Mouse.current.delta.ReadValue();
        float mouseX = mouseInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseInput.y * mouseSensitivity * Time.deltaTime;

        _rotationX -= mouseY;
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);

        transform.Rotate(Vector3.up * mouseX);

        if (_composer != null)
        {
            _composer.m_TrackedObjectOffset.y = -_rotationX * upDownSensitivity;
        }
        //_mainCam.transform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
    }

    public void DisableInput()
    {
        input.actions.Disable();
        Cursor.lockState = CursorLockMode.None;
    }

    public void EnableInput()
    {
        input.actions.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down);
    }
}
