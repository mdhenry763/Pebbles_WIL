using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFiring : MonoBehaviour
{
    public LayerMask fireLayer;
    public float cooldownTime = 1f;
    
    private Camera _mainCam;
    private bool cooldown;

    private void Start()
    {
        _mainCam = Camera.main;
    }

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
            
            
        }
    }

    IEnumerator FireCooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        cooldown = false;
    }
}
