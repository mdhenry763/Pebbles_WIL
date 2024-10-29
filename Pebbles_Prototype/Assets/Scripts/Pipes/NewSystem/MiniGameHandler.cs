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
    private bool _isFinished;

    private void Update()
    {
        HandleConnectionChange();
    }

    private void HandleConnectionChange()
    {
        if(_isFinished) return;
        if (endNode.IsFlowing && endNode.IsClosed)
        {
            Debug.Log("Mini-game can be enabled");
            _isEnabled = true;
        }
        else
        {
            _isEnabled = false;
        }
    }

    public void EnableMiniGame()
    {
        if (_isFinished) return;
        if(!_isEnabled) return;
        
        Debug.Log("Enabled MiniGame");
        _isFinished = true;
    }
}

public enum MiniGameType{
    Filter,
    SimonSays,
}
