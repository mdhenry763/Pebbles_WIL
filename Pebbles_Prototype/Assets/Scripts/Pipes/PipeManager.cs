using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PipeManager : MonoBehaviour
{
    [SerializeField] private Transform connectionCheckPoint1;
    [SerializeField] private Transform connectionCheckPoint2;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Vector3 rotation;

    public UnityEvent onPipeConnected;
    public UnityEvent onPipeDisconnected;

    public bool moveInstead;
    public Vector3[] movePositions;

    private PipeC _pipe;

    private void Start()
    {
        _pipe = GetComponent<PipeC>();
    }

    private void Update()
    {
        Debug.DrawRay(connectionCheckPoint1.position, connectionCheckPoint1.forward * 1, Color.red);
        Debug.DrawRay(connectionCheckPoint2.position, connectionCheckPoint2.forward * 1, Color.green);
       // Debug.DrawLine(connectionCheckPoint1.position, connectionCheckPoint1.forward * 100, Color.red);
    }

    private void CheckPipeConnection()
    {
        bool pipeConnected;
        
        Debug.Log("Ray casting");
        //First check
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(connectionCheckPoint1.position, connectionCheckPoint1.forward, out var firstHit, 3, layerMask))
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
        
        
        if(pipeConnected) onPipeConnected?.Invoke();
        else
        {
            _pipe.ChangePipeConnection();
        }
        
    }
    
    int pos = 0;
    
    public void RotatePipe()
    {
        if (moveInstead)
        {
            _pipe.ChangePipeConnection();

            if (transform.position == movePositions[pos])
            {
                pos++;
            }
            
            if (pos > movePositions.Length - 1)
            {
                pos = 0;
               
            }
            transform.position = movePositions[pos];
           return;
        }
        
        transform.Rotate(rotation);
        CheckPipeConnection();
    }

    private void OnDrawGizmos()
    {
        
    }
}
