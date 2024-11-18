using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutStateManager : MonoBehaviour
{
    [Header("Finish Conditions")] 
    public Node firstEndNode;

    public Node secondEndNode;
    public MiniGameHandler miniGameHandler;

    public event Action OnGameEnd;
    
    private bool _isEndGame;

    private void Start()
    {
        _isEndGame = false;
    }

    private void Update()
    {
        if (_isEndGame) return;

        if (!firstEndNode.IsClosed || !secondEndNode.IsClosed || !miniGameHandler.IsMiniGameFinished()) return;
        
        _isEndGame = true;
        OnGameEnd?.Invoke();
    }
}
