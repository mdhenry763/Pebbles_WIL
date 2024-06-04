using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSystemManager : MonoBehaviour
{
    public Transform startingPipe;
    public Transform endPipe;
    
    [SerializeField] private Material unconnectedMat;
    [SerializeField] private Material connectedMat;

    [Header("Debug")] public List<Transform> pipeSystem;
    [Header("Debug")] public List<Transform> connectedPipes;

    private void Start()
    {
        GetFullPipeSystem();
        
    }

    private void OnEnable()
    {
        PipeC.OnIsConnectedValueChange += PipeCOnOnIsConnectedValueChange;
    }
    
    private void OnDisable()
    {
        PipeC.OnIsConnectedValueChange -= PipeCOnOnIsConnectedValueChange;
    }
    
    private void PipeCOnOnIsConnectedValueChange()
    {
        CheckPipeSystem();
    }
    
    private void CheckPipeSystem()
    {
        connectedPipes.Clear();
        Debug.Log("Checking Pipe System!");
        Transform currentPipe = startingPipe;

        while (currentPipe != null)
        {
            var pipe = currentPipe.GetComponent<PipeC>();
            
            if (pipe.ConnectedTo != null)
            {
                if (pipe.pipeType == PipeType.Normal)
                {
                    pipe.IsConnected = true;
                }
                
                if (!pipe.IsConnected)
                {
                    Debug.Log($"{currentPipe.name} is not connected");
                    break;
                }
                pipe.ChangeColour(connectedMat, unconnectedMat);
                connectedPipes.Add(currentPipe);
            }

            currentPipe = pipe.ConnectedTo;

            if (currentPipe == endPipe)
            {
                break;
            }
        }
        ChangePipeVisual();
    }

    private void ChangePipeVisual()
    {
        bool disconnected = false;
        
        foreach (var pipe in pipeSystem)
        {
            var pipeC = pipe.GetComponent<PipeC>();
            
            if(pipeC == null) return;

            if (!disconnected)
            {
                disconnected = !pipeC.IsConnected; 
            }

            pipeC.IsConnected = !disconnected;
            pipeC.ChangeColour(connectedMat, unconnectedMat);
        }
    }

    private void GetFullPipeSystem()
    {
        Transform currentPipe = startingPipe;

        while (currentPipe != null)
        {
            var pipe = currentPipe.GetComponent<PipeC>();
            if (pipe.ConnectedTo != null)
            {
                pipeSystem.Add(currentPipe);
            }

            currentPipe = pipe.ConnectedTo;

            if (currentPipe == endPipe)
            {
                break;
            }
        }
    }


}
