using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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

        miniGameScore.miniGameScore = 0;
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
        leakHandler.StopLeakSystem();
        
        OnGameEnd?.Invoke(score);
        onGameEnded?.Invoke();
        
        if(soundManager != null) soundManager.PlayWinSound();
        
        UpdateGameManager(score);
    }

    private void UpdateGameManager(float score)
    {
        if(GameManager.Instance == null) return;

        if (SceneManager.GetActiveScene().name == "TutorialLevel_Rework")
        {
            GameManager.Instance.UpdateScore(0, score);
        }
        
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            GameManager.Instance.UpdateScore(1, score);
        }
        
        
    }
}
