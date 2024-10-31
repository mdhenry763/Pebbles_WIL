using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameHandler : MonoBehaviour
{
    [Header("Type")] 
    public Node endNode;
    public GameObject Level;
    public GameObject MiniGame;
    public MiniGameType miniGameType;

    private bool _isEnabled;
    private bool _isFinished;

    public static event Action OnMiniGameFinished;

    private void Start()
    {
        _isFinished = false;
    }

    private void Update()
    {
        HandleConnectionChange();
    }

    private void HandleConnectionChange()
    {
        if(_isFinished) return;
        
        if (endNode.IsFlowing && endNode.IsClosed)
        {
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

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Level.SetActive(false);
        MiniGame.SetActive(true);
    }

    private void MiniGameFinished()
    {
        OnMiniGameFinished?.Invoke();
        _isFinished = true;
    }
}

public enum MiniGameType{
    Filter,
    SimonSays,
}
