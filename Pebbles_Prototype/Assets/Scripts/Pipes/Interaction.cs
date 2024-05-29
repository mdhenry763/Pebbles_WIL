using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public actionType InteractionType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Interactions>(out var interactions))
        {
            interactions.currentInteraction = InteractionType;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Interactions>(out var interactions))
        {
            interactions.currentInteraction = actionType.None;
        }
    }
}
