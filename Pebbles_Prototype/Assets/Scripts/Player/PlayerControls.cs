using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("References")]
    public Camera mainCam;
    public CinemachineVirtualCamera followCam;
    public PlayerEventSystemSO playerEvents;
    public GameObject wrenchie;
    public SFXManager soundManager;
    
    [Header("Player Settings")]
    public float moveSpeed = 0.1f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public float maxVelMagnitude = 200f;

    public bool disableCamInBeginning;
    
    //Player Components
    private InputActions _controls;
    private Rigidbody _playerRb;
    private Transform _camTransform;
    
    //State
    private bool isGrounded;
    
    //Movement
    private float _defaultMoveSpeed;

    private void Awake()
    {
        _controls = new InputActions();
        _playerRb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _camTransform = mainCam.transform;
        _defaultMoveSpeed = moveSpeed;
        followCam.enabled = !disableCamInBeginning;
    }

    private void OnEnable()
    {
        _controls.Enable();
        
        _controls.Player.Jump.performed += HandlePlayerJump;
        _controls.Player.Journal.performed += HandleJournalCalled;
        _controls.Player.Escape.performed += HandlePauseCalled;
        
        playerEvents.OnActivateUI += HandleUIActivation;
        playerEvents.OnActivateWrenchie += ActivateWrenchie;
    }

    private void OnDisable()
    {
        _controls.Player.Jump.performed -= HandlePlayerJump;
        _controls.Player.Journal.performed -= HandleJournalCalled;
        _controls.Player.Escape.performed -= HandlePauseCalled;
        _controls.Disable();
        
        playerEvents.OnActivateUI -= HandleUIActivation;
        playerEvents.OnActivateWrenchie -= ActivateWrenchie;
    }

    private void Update()
    {
        //Sphere check
        isGrounded = Physics.SphereCast(transform.position, 0.5f, Vector3.down, out var raycastHit, 1, groundLayer);
        
        MovePlayer();
    }
    
    private void HandleUIActivation(bool activate)
    {
        if (activate)
        {
            UnlockCursor();
        }
        else
        {
            LockCursor();
        }
    }

    private void ActivateWrenchie()
    {
        if (wrenchie == null) return;
        
        wrenchie.SetActive(true);
    }

    public void LockCursor()
    {
        followCam.enabled = true;
        moveSpeed = _defaultMoveSpeed;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        followCam.enabled = false;
        moveSpeed = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void HandleMenuEvent(UIEvents uiEvents)
    {
        HandleUIActivation(true);
        playerEvents.FireShowMenuEvent(uiEvents);
    }

    private void HandleJournalCalled(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            HandleMenuEvent(UIEvents.Journal);
        }
        
    }
    
    private void HandlePauseCalled(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            HandleMenuEvent(UIEvents.Pause);
        }
        
    }

    private void MovePlayer()
    {
        //Movement
        var input = _controls.Player.Move.ReadValue<Vector2>();
        var move = _camTransform.forward * input.y + _camTransform.right * input.x;
        move.y = 0;

        //if(!isGrounded) return;
        transform.Translate(move * moveSpeed);
        //_playerRb.velocity += move * moveSpeed;
    }

    private void HandlePlayerJump(InputAction.CallbackContext obj)
    {
        if(!isGrounded) return;
        Debug.Log("Jump now boy");
        var jumpVel = Vector3.up * jumpForce;
        _playerRb.velocity += jumpVel;
        soundManager.PlayJumpSound();
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
