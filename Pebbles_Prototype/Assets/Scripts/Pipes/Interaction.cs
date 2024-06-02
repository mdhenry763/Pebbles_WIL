using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public actionType InteractionType;
    public bool valveUnlocked;

    private PipeC _pipe;

    private void Start()
    {
        _pipe = GetComponent<PipeC>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Interactions>(out var interactions))
        {
            interactions.currentInteraction = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Interactions>(out var interactions))
        {
            interactions.currentInteraction = null;
        }
    }

    public void Valve()
    {
        valveUnlocked = !valveUnlocked;
        
        _pipe.ChangePipeConnection();

        Debug.Log($"Valve is locked: {valveUnlocked}");
    }
}
