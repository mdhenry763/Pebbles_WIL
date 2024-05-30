using System;
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

    private void OnCollisionEnter(Collision other)
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
