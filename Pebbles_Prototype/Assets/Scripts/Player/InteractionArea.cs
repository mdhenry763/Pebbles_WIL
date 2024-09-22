using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class InteractionArea : MonoBehaviour
{
    private Node _pipeSource;

    public void HandleInteraction(InputAction.CallbackContext context)
    {
        if(!context.performed) return;

        if (_pipeSource != null)
        {
            _pipeSource.PipeToggle();
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
    }

    private void OnTriggerExit(Collider other)
    {
        if(_pipeSource == null) return;
        if (!other.CompareTag("Pipe")) return;

        if (!other.TryGetComponent<Node>(out var pipe)) return;
        
        if (pipe.IsSource)
        {
            _pipeSource = null;
        }
    }
}
