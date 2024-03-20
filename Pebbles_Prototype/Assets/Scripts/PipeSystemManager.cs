using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSystemManager : MonoBehaviour
{
    [SerializeField] private int totalConnections = 2;

    private int pipesConnected;

    private void OnEnable()
    {
        //Subscribe to connection event
    }

    private void OnDisable()
    {
        //Unsubscribe    
    }

    private void HandlePipeConnected()
    {
        pipesConnected++;

        if (pipesConnected == totalConnections)
        {
            Debug.Log("Player has won the game");
            //Fire Level over event
        }
    }
    
}
