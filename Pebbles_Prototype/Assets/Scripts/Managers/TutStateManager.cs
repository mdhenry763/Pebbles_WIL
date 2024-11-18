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
    public TimeSystem timer;
    public MiniGameScore miniGameScore;
    public LeakHandler leakHandler;

    public static event Action<float> OnGameEnd;
    
    private bool _isEndGame;

    private void Start()
    {
        _isEndGame = false;
    }

    private void Update()
    {
        if (_isEndGame) return;

        if (!firstEndNode.IsClosed || !secondEndNode.IsClosed || !miniGameHandler.IsMiniGameFinished()) return;
        
        EndGame();
    }

    private void EndGame()
    {
        _isEndGame = true;

        var score = miniGameScore.timeScore + (float)miniGameScore.miniGameScore + leakHandler.GetLeakScore();
        timer.StopTime();
        
        OnGameEnd?.Invoke(score);
    }
}
