using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameHandler : MonoBehaviour
{
    [Header("Type")] 
    public Node endNode;
    public MiniGameType miniGameType;

    private bool _isEnabled;

    private void OnEnable()
    {
        Node.OnConnectionChanged += HandleConnectionChange;
    }

    private void OnDisable()
    {
        Node.OnConnectionChanged -= HandleConnectionChange;
    }

    private void HandleConnectionChange()
    {
        if (endNode.IsFlowing && endNode.IsClosed)
        {
            Debug.Log("Minigame can be enabled");
            _isEnabled = true;
        }
        else _isEnabled = false;
    }

    public void EnableMiniGame()
    {
        if(!_isEnabled) return;
        
        Debug.Log("Enabled MiniGame");
    }
}

public enum MiniGameType{
    Filter,
    SimonSays,
}
