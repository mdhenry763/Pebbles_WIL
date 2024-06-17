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
    [field: SerializeField] public bool PipeConnected { get; private set; }

    private void Start()
    {
        _pipe = GetComponent<PipeC>();
        PipeConnected = false;
    }

    private void Update()
    {
        Debug.DrawRay(connectionCheckPoint1.position, connectionCheckPoint1.forward * 1, Color.red);
        Debug.DrawRay(connectionCheckPoint2.position, connectionCheckPoint2.forward * 1, Color.green);
       // Debug.DrawLine(connectionCheckPoint1.position, connectionCheckPoint1.forward * 100, Color.red);
    }

    private void CheckPipeConnection()
    {
        PipeConnected = !PipeConnected;
        
        _pipe.ChangePuzzleConnection(PipeConnected);
        
    }
    
    int pos = 0;
    
    public void RotatePipe()
    {
        if (moveInstead)
        {
            CheckPipeConnection();

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
