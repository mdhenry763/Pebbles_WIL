using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody _rigComp;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigComp = GetComponent<Rigidbody>();
        _rigComp.AddForce(transform.forward * 5000, ForceMode.Force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
