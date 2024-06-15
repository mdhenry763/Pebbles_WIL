using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HintActivation : MonoBehaviour
{
    public string hint;
    
    public UnityEvent<string> onShowPlayerHint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Show Player Hint");
            onShowPlayerHint?.Invoke(hint);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            //onShowPlayerHint?.Invoke(false);
        }
    }
}
