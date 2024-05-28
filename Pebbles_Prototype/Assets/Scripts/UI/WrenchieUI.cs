using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchieUI : MonoBehaviour
{
    public GameObject wrenchieUI;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wrenchieUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wrenchieUI.SetActive(false);
        }
    }
    
}
