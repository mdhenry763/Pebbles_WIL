using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    [SerializeField] private PipeEvents pipeEvents;
    [SerializeField] private Transform connectionCheckPoint1;
    [SerializeField] private Transform connectionCheckPoint2;
    [SerializeField] private LayerMask layerMask;

    private void OnEnable()
    {
        pipeEvents.OnPipeMoved += CheckPipeConnection;
    }

    private void Update()
    {
        Debug.DrawRay(connectionCheckPoint1.position, connectionCheckPoint1.forward * 100, Color.red);
        Debug.DrawRay(connectionCheckPoint2.position, connectionCheckPoint2.forward * 100, Color.green);
        Debug.DrawLine(connectionCheckPoint1.position, connectionCheckPoint1.forward * 100, Color.red);
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q pressed");
            CheckPipeConnection(0, 0, 0);
        }
    }

    private void CheckPipeConnection(float x, float y, float z)
    {
        Debug.Log("Raycasting");
        //First check
        RaycastHit firstHit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(connectionCheckPoint1.position, connectionCheckPoint1.forward, out firstHit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(connectionCheckPoint1.position, connectionCheckPoint1.forward * firstHit.distance, Color.red);
            Debug.Log("Did Hit");
            RaycastHit secondHit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(connectionCheckPoint2.position, connectionCheckPoint2.forward, out secondHit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(connectionCheckPoint2.position, connectionCheckPoint2.forward * secondHit.distance, Color.red);
                Debug.Log("Did Hit");
            }
        }
        else
        {
            
        }
        
    }

    private void OnDrawGizmos()
    {
        
    }
}
