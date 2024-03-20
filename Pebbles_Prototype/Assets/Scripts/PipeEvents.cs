using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PipeEvents : MonoBehaviour
{
    public event Action OnPipeConnected;
    public event Action OnPipeDisconnected;
    public event Action<float, float, float> OnPipeMoved;

    public void PipeConnected(bool isConnected)
    {
        (isConnected ? OnPipeConnected : OnPipeDisconnected)?.Invoke();
    }

    public void PipeMovedEvent(float xRotation, float yRotation, float zRotation)
    {
        OnPipeMoved?.Invoke(xRotation, yRotation, zRotation);
    }
}
