using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeC : MonoBehaviour
{
    public PipeType pipeType;
    [SerializeField] private MeshRenderer meshRenderer;
    
    public bool IsConnected;
    public Transform ConnectedTo;

    public static event Action OnIsConnectedValueChange;

    private void Start()
    {
        IsConnected = false;

        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public void ChangePipeConnection()
    {
        IsConnected = !IsConnected;
        OnIsConnectedValueChange?.Invoke();
    }

    public void ChangeColour(Material connectedMat, Material unconnectedMat)
    {
        Debug.Log($"Changing pipe colour: {IsConnected}");
        Material[] mats = meshRenderer.materials; 
        mats[0] = IsConnected ? connectedMat : unconnectedMat; 
        meshRenderer.materials = mats;
    }
    
    
}

public enum PipeType
{
    Valve,
    Connection,
    Normal
}
