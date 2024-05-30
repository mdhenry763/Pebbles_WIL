using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiltrationPlayerController : MonoBehaviour
{
    public CharacterController _Controller;

    public float speed = 12f;
    private Vector3 velocity;
        
    // Start is called before the first frame update
 
   

   
    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        float z = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right  * x + transform.forward * z;

        _Controller.Move(move * speed * Time.deltaTime);
    }
}
