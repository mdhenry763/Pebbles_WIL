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
    public PipeSystemManager pipeSystemManager;
    public SFXManager sfxManager;
    
    [Header("Settings: ")]
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float playerSpeed = 8f;
    public float mouseSensitivity = 100f;
    public LayerMask jumpLayer;
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
        Cursor.visible = false;
        moveDirection = Vector3.zero;
    }

    private void Start()
    {
        DisableInput();
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
        Vector3 leftSide = new Vector3(pos.x - 0.5f, pos.y , pos.z);
        Vector3 rightSide = new Vector3(pos.x + 0.5f, pos.y , pos.z);
        Vector3 topSide = new Vector3(pos.x , pos.y , pos.z - 0.5f);
        Vector3 bottomSide = new Vector3(pos.x, pos.y , pos.z + 0.5f);

        bool hitLeft = Physics.Raycast(leftSide, Vector3.down, 1.25f, jumpLayer);
        bool hit = Physics.Raycast(pos, Vector3.down, 1.25f, jumpLayer);
        bool hitRight = Physics.Raycast(rightSide, Vector3.down, 1.25f, jumpLayer);
        bool hitTop = Physics.Raycast(topSide, Vector3.down, 1.25f, jumpLayer);
        bool hitBottom = Physics.Raycast(bottomSide, Vector3.down, 1.25f, jumpLayer);

        return hit || hitLeft || hitRight || hitBottom || hitTop;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        _moveInput = new Vector3(input.x, 0, input.y);

        if (input.y != 0)
        {
            sfxManager.PlayWalkClip();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (IsGrounded() && canMove)
        {
            Debug.Log("Jumping");
            moveDirection.y = jumpPower;
        }
    }

    private bool cooldown;
    
    public void OnFire(InputAction.CallbackContext context)
    {
        if(cooldown) return;
        RaycastHit hit;

        //casts ray to mouse position
        Ray mousePos = _mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());

        //if there was a ray cast the player will look at it
        if (Physics.Raycast(mousePos, out hit, 100f, fireLayer))
        {
            Debug.Log($"Hit other {hit.collider.name}");

            if (hit.transform.TryGetComponent<PipeManager>(out var pipeManager))
            {
                pipeManager.RotatePipe();
                sfxManager.PlayPuzzleClip();
                cooldown = true;
                StartCoroutine(FireCooldown());
            }
            
            if(hit.transform.parent.TryGetComponent<PipeC>(out var pipeC))
            {
                StopCoroutine(pipeSystemManager.RustMechanic());
                pipeC.StopRust();
                sfxManager.PlayPuzzleClip();
                cooldown = true;
                StartCoroutine(FireCooldown());
            }
            
        }
    }

    IEnumerator FireCooldown()
    {
        yield return new WaitForSeconds(1f);
        cooldown = false;
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
        Cursor.visible = true;
        canMove = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void EnableInput()
    {
        canMove = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ActivateWrenchie()
    {
        wrenchie.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 pos = transform.position;
        Vector3 leftSide = new Vector3(pos.x - 0.5f, pos.y , pos.z);
        Vector3 rightSide = new Vector3(pos.x + 0.5f, pos.y , pos.z);
        Vector3 topSide = new Vector3(pos.x , pos.y , pos.z - 0.5f);
        Vector3 bottomSide = new Vector3(pos.x, pos.y , pos.z + 0.5f);
        
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 1.225f);
        Gizmos.DrawLine(leftSide, leftSide + Vector3.down * 1.225f);
        Gizmos.DrawLine(rightSide, rightSide + Vector3.down * 1.225f);
        Gizmos.DrawLine(topSide, topSide + Vector3.down * 1.225f);
        Gizmos.DrawLine(bottomSide, bottomSide + Vector3.down * 1.225f);
    }
}
