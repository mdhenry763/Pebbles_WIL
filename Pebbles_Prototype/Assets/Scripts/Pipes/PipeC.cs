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

    private float counter;
    
    private void Leakage()
    {
        if(pipeType != PipeType.Connection) return;
        if (ConnectedTo)
        {
            var pipeNext = ConnectedTo.GetComponent<PipeC>();

            if (IsConnected && !pipeNext.IsConnected && pipeNext.pipeType == PipeType.Connection)
            {
                counter += Time.deltaTime;
                Debug.Log($"Leaking amount: {counter}");
            }
        }

        
        
    }

    private void Update()
    {
        Leakage();
    }
}

public enum PipeType
{
    Valve,
    Connection,
    Normal
}
