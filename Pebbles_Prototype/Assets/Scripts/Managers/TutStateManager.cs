using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutStateManager : MonoBehaviour
{
    [Header("Finish Conditions")] 
    public Node firstEndNode;

    public Node secondEndNode;
    public MiniGameHandler miniGameHandler;
    public TimeSystem timer;
    public MiniGameScore miniGameScore;
    public LeakHandler leakHandler;
    public SFXManager soundManager;

    public static event Action<float> OnGameEnd;
    public UnityEvent onGameEnded;
    
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
        
        onGameEnded?.Invoke();
        if(soundManager != null) soundManager.PlayWinSound();
    }
}
