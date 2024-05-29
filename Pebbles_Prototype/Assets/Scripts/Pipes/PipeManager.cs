using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PipeManager : MonoBehaviour
{
    [SerializeField] private PipeEvents pipeEvents;
    [SerializeField] private Transform connectionCheckPoint1;
    [SerializeField] private Transform connectionCheckPoint2;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Vector3 rotation;

    public UnityEvent onPipeConnected;
    public UnityEvent onPipeDisconnected;

    private void OnEnable()
    {
        //pipeEvents.OnPipeMoved += CheckPipeConnection;
    }

    private void Update()
    {
        Debug.DrawRay(connectionCheckPoint1.position, connectionCheckPoint1.forward * 1, Color.red);
        Debug.DrawRay(connectionCheckPoint2.position, connectionCheckPoint2.forward * 1, Color.green);
       // Debug.DrawLine(connectionCheckPoint1.position, connectionCheckPoint1.forward * 100, Color.red);
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q pressed");
            CheckPipeConnection();
        }
    }

    private void CheckPipeConnection()
    {
        bool pipeConnected;
        
        Debug.Log("Raycasting");
        //First check
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(connectionCheckPoint1.position, connectionCheckPoint1.forward, out var firstHit, 1, layerMask))
        {
            Debug.DrawRay(connectionCheckPoint1.position, connectionCheckPoint1.forward * firstHit.distance, Color.red);
            Debug.Log("First Hit");
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(connectionCheckPoint2.position, connectionCheckPoint2.forward, out var secondHit, 1, layerMask))
            {
                Debug.DrawRay(connectionCheckPoint2.position, connectionCheckPoint2.forward * secondHit.distance, Color.red);
                Debug.Log("Second Hit Hit");
                pipeConnected = true;
            }
            else
            {
                pipeConnected = false;
            }
        }
        else
        {
            pipeConnected = false;
        }
        
        //if(pipeConnected) onPipeConnected?.Invoke();
        //else
        //{
        //    onPipeDisconnected?.Invoke();
       // }
        
    }

    public void RotatePipe()
    {
        transform.Rotate(rotation);
        CheckPipeConnection();
    }

    private void OnDrawGizmos()
    {
        
    }
}
