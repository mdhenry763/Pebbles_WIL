using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class InteractionArea : MonoBehaviour
{
    public SFXManager sfxManager;
    
    private Node _pipeSource;
    private MiniGameHandler _miniGameHandler;

    public static event Action OnPipeToggled;

    public void HandleInteraction(InputAction.CallbackContext context)
    {
        if(!context.performed) return;

        if (_pipeSource != null)
        {
            _pipeSource.PipeToggle();
            //Event to control what happens to pipe, connection check, leak check rust check
            OnPipeToggled?.Invoke();
            sfxManager.PlayValveClip();
        }

        if (_miniGameHandler != null)
        {
            _miniGameHandler.EnableMiniGame();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pipe"))
        {
            if (other.TryGetComponent<Node>(out var pipe))
            {
                if (pipe.IsSource)
                {
                    _pipeSource = pipe;
                }
            }
        }

        if (other.CompareTag("Mini-Game"))
        {
            if (other.TryGetComponent<MiniGameHandler>(out var miniGame))
            {
                Debug.Log("Mini Game");
                _miniGameHandler = miniGame;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Pipe"))
        {
            if(_pipeSource == null) return;
            if (!other.TryGetComponent<Node>(out var pipe)) return;
        
            if (pipe.IsSource)
            {
                _pipeSource = null;
            }
        }
        
        if (other.CompareTag("Mini-Game"))
        {
            _miniGameHandler = null;
        }

        
    }
}
