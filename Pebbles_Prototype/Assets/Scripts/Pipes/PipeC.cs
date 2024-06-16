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

    private bool _isRusting;
    private Material currentMaterial;

    public static event Action OnIsConnectedValueChange;

    private void Start()
    {
        IsConnected = false;
        _isRusting = false;

        meshRenderer = GetComponentInChildren<MeshRenderer>();
        currentMaterial = meshRenderer.materials[0];
    }

    public void ChangePipeConnection()
    {
        IsConnected = !IsConnected;
        OnIsConnectedValueChange?.Invoke();
    }

    public void ChangeColour(Material connectedMat, Material unconnectedMat)
    {
        if(_isRusting) return;
        currentMaterial = IsConnected ? connectedMat : unconnectedMat;
        ChangePipeColour(currentMaterial);
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

    private void ChangePipeColour(Material mat)
    {
        Material[] mats = meshRenderer.materials; 
        mats[0] = mat; 
        meshRenderer.materials = mats;
    }

    public void RustPipe(Material rustMaterial)
    {
        //Debug.Log($"Changing pipe colour: {IsConnected}");
        ChangePipeColour(rustMaterial);
        _isRusting = true;
    }

    public void StopRust()
    {
        if(!_isRusting) return;
        ChangePipeColour(currentMaterial);
        _isRusting = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            Debug.LogWarning("Touching player");
        }
    }
}

public enum PipeType
{
    Valve,
    Connection,
    Normal
}
