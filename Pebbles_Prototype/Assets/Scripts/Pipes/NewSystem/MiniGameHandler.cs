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

    [Header("Activate After mini-game")] 
    public GameObject objectToActivate;

    private bool _isEnabled;
    private bool _isFinished;

    private void Start()
    {
        _isFinished = false;
    }

    private void OnEnable()
    {
        SimonSaysButtonManager.OnMiniGameComplete += MiniGameFinished;
    }

    private void OnDisable()
    {
        SimonSaysButtonManager.OnMiniGameComplete -= MiniGameFinished;
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
        _isFinished = true;
    }

    private void MiniGameFinished()
    {
        Level.SetActive(true);
        MiniGame.SetActive(false);
        
        //Activate Sprinkler camera
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
        
        
        _isFinished = true;
    }
}

public enum MiniGameType{
    Filter,
    SimonSays,
}
