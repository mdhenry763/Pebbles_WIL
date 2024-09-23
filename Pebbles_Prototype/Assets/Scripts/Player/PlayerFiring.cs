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
        if(!context.performed) return;

        //casts ray to mouse position
        //Get screen mid-point
        var screenMidpoint = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
        Ray mousePos = _mainCam.ScreenPointToRay(screenMidpoint);

        //if there was a ray cast the player will look at it
        if (Physics.Raycast(mousePos, out var hit, 100f, fireLayer))
        {
            Debug.Log($"Hit other {hit.collider.name}");
            if (hit.transform.TryGetComponent<PuzzlePiece>(out var puzzlePiece))
            {
                puzzlePiece.RotateObject();
            }
            
        }
    }

    IEnumerator FireCooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        cooldown = false;
    }
}
