using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HintActivation : MonoBehaviour
{
    public UnityEvent<bool> onShowPlayerHint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Show Player Hint");
            onShowPlayerHint?.Invoke(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            onShowPlayerHint?.Invoke(false);
        }
    }
}
