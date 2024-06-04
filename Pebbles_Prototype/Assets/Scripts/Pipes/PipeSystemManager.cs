using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PipeSystemManager : MonoBehaviour
{
    public Transform startingPipe;
    public Transform endPipe;
    
    [SerializeField] private Material unconnectedMat;
    [SerializeField] private Material connectedMat;

    [Header("Debug")] public List<Transform> pipeSystem;
    [Header("Debug")] public List<Transform> connectedPipes;

    private int _pipeSystemCount;
    private bool _miniGameComplete;
    private bool _pipeSystemComplete;

    public UnityEvent onGameEnd;

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

    private PipeC leakingPipe;
    
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

                if (pipe == leakingPipe)
                {
                    if (pipe.IsConnected)
                    {
                        leakingPipe = null;
                    }
                }
                
                if (!pipe.IsConnected)
                {
                    if (leakingPipe == null)
                    {
                        if (pipe.pipeType == PipeType.Connection)
                        {
                            leakingPipe = pipe;
                            Debug.Log("Leakage");
                        }
                    }
                    
                    Debug.Log($"{currentPipe.name} is not connected");
                    break;
                }
                pipe.ChangeColour(connectedMat, unconnectedMat);
                connectedPipes.Add(currentPipe);
                
                if (connectedPipes.Count >= pipeSystem.Count-1)
                {
                    
                    _pipeSystemComplete = true;
                    CheckIfLevelComplete();
                }
            }

            currentPipe = pipe.ConnectedTo;

            if (currentPipe == endPipe)
            {
                break;
            }
        }
        ChangePipeVisual();
    }

    void CheckIfLevelComplete()
    {
        var gameCond = _miniGameComplete && _pipeSystemComplete;


        if (gameCond)
        {
            Debug.Log("Game Over");
            onGameEnd?.Invoke();
        }
        
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
                _pipeSystemCount++;
                Debug.Log($"Pipes: {_pipeSystemCount}");
            }

            currentPipe = pipe.ConnectedTo;

            if (currentPipe == endPipe)
            {
                break;
            }
        }
    }
    
    //
    public void SetMiniGameComplete()
    {
        Debug.Log("Mini-Game complete");
        _miniGameComplete = true;
    }


}
