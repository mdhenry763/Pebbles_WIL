using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Interaction : MonoBehaviour
{
    public actionType InteractionType;

    public GameObject conserveGame;
    public GameObject filterGame;
    public GameObject loadingScreen;
    public GameObject ui;
    
    public bool valveUnlocked;

    public UnityEvent onChangingScene;
    public UnityEvent onChangingBackScene;
    public UnityEvent onMiniGameComplete;

    private PipeC _pipe;
    private bool genUsed;

    private void Start()
    {
        _pipe = GetComponent<PipeC>();
        
        CrossSceneEvents.onMiniGameFinished += MiniGameFinished;
    }

    private void MiniGameFinished()
    {
        if(InteractionType != actionType.Generator) return;
        
        if(ui) ui.SetActive(true);
        StartCoroutine(LoadingSceneToGame());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Interactions>(out var interactions))
        {
            interactions.currentInteraction = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Interactions>(out var interactions))
        {
            interactions.currentInteraction = null;
        }
    }

    public void Valve()
    {
        valveUnlocked = !valveUnlocked;
        
        _pipe.ChangePipeConnection();

        Debug.Log($"Valve is locked: {valveUnlocked}");
    }

    public void Generator()
    {
        if(genUsed) return;
        StartCoroutine(LoadingScreenToMiniGame());
    }

    IEnumerator LoadingScreenToMiniGame()
    {
        if(loadingScreen) loadingScreen.SetActive(true);
        yield return new WaitForSeconds(3f);
        onChangingScene?.Invoke();
        filterGame.SetActive(true);
        conserveGame.SetActive(false);
        if(loadingScreen) loadingScreen.SetActive(false);
        
        
        genUsed = true;
    }

    IEnumerator LoadingSceneToGame()
    {
        if(loadingScreen) loadingScreen.SetActive(true);
        conserveGame.SetActive(true);
        filterGame.SetActive(false);
        yield return new WaitForSeconds(3f);
        if(loadingScreen) loadingScreen.SetActive(false);
        onChangingBackScene?.Invoke();
        onMiniGameComplete?.Invoke();
    }
}
