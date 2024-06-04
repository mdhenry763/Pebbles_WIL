using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public enum actionType
{
    Valve,
    Rust,
    Generator,
    None
}

public class Interactions : MonoBehaviour
{
    public GameObject UIMenu;
    public GameObject PauseMenu;
    
    public Interaction currentInteraction { get; set;}

    public UnityEvent onUIMenuOpen;
    public UnityEvent onUIMenuClose;

    private bool _isMenuOpen;
    private bool _isPauseMenuOpen;

    private bool _inputEnabled = true;

    private void Start()
    {
        _inputEnabled = true;
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if(!_inputEnabled) return;
        if(currentInteraction == null) return;
        
        switch (currentInteraction.InteractionType)
        {
            case actionType.None:
                return;
            case actionType.Valve:
                currentInteraction.Valve();
                //Unlock Valve
                return;
            case actionType.Generator:
                currentInteraction.Generator();
                //Unlock Geni
                return;
            default:
                return;
        }
    }

    public void MenuOpenClose(InputAction.CallbackContext context)
    {
        if(!_inputEnabled) return;
        _isMenuOpen = !_isMenuOpen;
        
        UIMenu.SetActive(_isMenuOpen);
        
        if(_isMenuOpen) onUIMenuOpen?.Invoke();
        else
        {
            onUIMenuClose?.Invoke();
        }
    }

    public void Escape(InputAction.CallbackContext context)
    {
        if(!_inputEnabled) return;
        
        _isPauseMenuOpen = !_isPauseMenuOpen;
        
        PauseMenu.SetActive(_isPauseMenuOpen);
        
        if(_isPauseMenuOpen) onUIMenuOpen?.Invoke();
        else
        {
            onUIMenuClose?.Invoke();
        }
    }

    public void DisableInput()
    {
        _inputEnabled = false;
    }

    public void EnableInput()
    {
        _inputEnabled = true;
    }
}

public struct InteractionData
{
    public Interaction interaction;
}