using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public float  moveVelocity, moveSpeed = 2;
    private Vector3 moveInput;
    public Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //Aim player at mouse
        //which direction is up
        Vector3 upAxis = new Vector3(0, 0, -1);
        Vector3 mouseScreenPosition = Input.mousePosition;
        //set mouses z to your targets
        mouseScreenPosition.z = transform.position.z;
        Vector3 mouseWorldSpace = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        transform.LookAt(mouseWorldSpace, upAxis);
        //zero out all rotations except the axis I want
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}
