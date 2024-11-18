using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Menus")] 
    public GameObject pauseMenu;
    public GameObject journalMenu;
    public GameObject startCanvas;
    public GameObject endGameMenu;

    [Header("Timer")] public TimeSystem timer;
    
    [Header("Scenes")]
    public string loadScene0 = "MainMenu";
    public string loadScene1 = "MichaelSandbox";

    public PlayerEventSystemSO playerEvents;

    private void OnEnable()
    {
        playerEvents.OnShowMenu += HandleShowMenuCalled;
        TutStateManager.OnGameEnd += HandleGameEnd;
    }

    private void OnDisable()
    {
        playerEvents.OnShowMenu -= HandleShowMenuCalled;
        TutStateManager.OnGameEnd -= HandleGameEnd;
    }

    private void HandleGameEnd(float score)
    {
        playerEvents.FireUIEvent(true);
        endGameMenu.SetActive(true);
        if (endGameMenu.TryGetComponent<EndGameScore>(out var endGameScore))
        {
            endGameScore.SetScoreTxt($"Score: {(int)score}");
        }
    }

    private void HandleShowMenuCalled(UIEvents uiEvents)
    {
        switch (uiEvents)
        {
            case UIEvents.Journal:
                //Show Journal
                journalMenu.SetActive(true);
                break;
            case UIEvents.Pause:
                pauseMenu.SetActive(true);
                //Show Pause Menu
                break;
        }
        
        timer.StopTime();
    }

    public void ExitMenu()
    {
        pauseMenu.SetActive(false);
        journalMenu.SetActive(false);
        
        timer.StartTime();
        
        if(!startCanvas.activeSelf)
            playerEvents.FireUIEvent(false);
    }

    public void LoadScene0()
    {
        SceneManager.LoadScene(loadScene0);
    }
    
    public void LoadScene1()
    {
        SceneManager.LoadScene(loadScene1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
