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
    public actionType currentInteraction;

    public UnityEvent onUIMenuOpen;
    public UnityEvent onUIMenuClose;

    private bool _isMenuOpen;
    private bool _isPauseMenuOpen;
    
    public void Interact(InputAction.CallbackContext context)
    {
        switch (currentInteraction)
        {
            case actionType.None:
                return;
            case actionType.Valve:
                //Unlock Valve
                return;
            case actionType.Generator:
                //Unlock Geni
                return;
            default:
                return;
        }
    }

    public void MenuOpenClose(InputAction.CallbackContext context)
    {
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
        _isPauseMenuOpen = !_isPauseMenuOpen;
        
        PauseMenu.SetActive(_isPauseMenuOpen);
        
        if(_isPauseMenuOpen) onUIMenuOpen?.Invoke();
        else
        {
            onUIMenuClose?.Invoke();
        }
    }
    
    
}
