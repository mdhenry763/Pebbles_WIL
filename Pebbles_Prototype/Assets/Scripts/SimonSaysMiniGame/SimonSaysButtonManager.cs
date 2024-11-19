using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SimonSaysButtonManager : MonoBehaviour
{
    public GameManager _GameManager;
    public List<SimonSaysButton> buttons;
    public MiniGameScore miniGame;
    private List<int> pattern = new List<int>();
    public int currentStep;
    public float currentScore;
    private bool isPlayerTurn = false;
    public float lightUpDuration = 0.5f;
    public SimonSaysUIManager _SimonSaysUIManager;
    public SFXManager soundManager;
    
    //Events
    public static event Action OnMiniGameComplete;
    
    
    void Start()
    {
        _GameManager = GameManager.Instance;
        currentScore = 0;
        currentStep = 0;
        _SimonSaysUIManager.UpdateScorUI();
        foreach (var button in buttons)
        {
            button.LightUp();
            
        }
        foreach (var button in buttons)
        {
            button.ResetLight();
            
        }
    }

    
    public void StartNewRound()
    {
        StartCoroutine(PlayNewRound());
    }
    IEnumerator PlayNewRound()
    {
        isPlayerTurn = false;
        currentStep = 0;
        pattern.Add(Random.Range(0, buttons.Count)); 

        yield return new WaitForSeconds(1f); // Wait before starting pattern

        foreach (int index in pattern)
        {
            buttons[index].LightUp();
            if(soundManager != null) soundManager.PlayBtnSound();
            yield return new WaitForSeconds(lightUpDuration); // Duration for each light-up
            buttons[index].ResetLight();
            yield return new WaitForSeconds(0.3f); // Pause between lights
        }

        if(soundManager != null) soundManager.PlayEnterBtnSound();
        isPlayerTurn = true;
    }

    public void GameOver()
    {
       //Todo Load Level 2
       _GameManager.filtrationMiniGameScore = currentScore;
       OnMiniGameComplete?.Invoke();
    }
    public void ButtonPressed(int index)
    {
        if (!isPlayerTurn) return;

        if (index == pattern[currentStep])
        {
            if(soundManager != null) soundManager.PlayBtnSound();
            currentStep++;
            miniGame.miniGameScore = currentStep * 10;
            
            if (currentStep >= pattern.Count)
            {
                currentScore++;
                _SimonSaysUIManager.UpdateScorUI();
                if (currentScore == 6)
                {
                    StopAllCoroutines();
                    GameOver();
                    return;
                }
                StartCoroutine(PlayNewRound()); // If player completes pattern, start new round 
            }
                
        }
        else
        {
            if(soundManager != null) soundManager.PlayErrorSound();
            GameOver();
            Debug.Log("Incorrect! Game Over!");
            
            // Optionally reset the game or end
        }
    }
}
