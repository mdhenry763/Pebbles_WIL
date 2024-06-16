using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Interaction : MonoBehaviour
{
    public actionType InteractionType;

    [Header("References: ")]
    public GameObject conserveGame;
    public GameObject filterGame;
    public GameObject loadingScreen;
    public GameObject ui;

    [Header("Hint Message")] public string hint;
    
    [Header("Debug: ")]
    public bool valveUnlocked;

    public UnityEvent onChangingScene;
    public UnityEvent onChangingBackScene;
    public UnityEvent onMiniGameComplete;
    public UnityEvent<string> onShowHint;
    
    public bool GeneratorActive { get; set;}
    public bool IsUsingGen { get; set;}

    private PipeC _pipe;
    private bool genUsed;

    private void Start()
    {
        _pipe = GetComponent<PipeC>();

        GeneratorActive = false;
        IsUsingGen = false;
        
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
            
            onShowHint?.Invoke(hint);
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
        if(!GeneratorActive) return;
        IsUsingGen = true;
        StartCoroutine(LoadingScreenToMiniGame());
    }

    IEnumerator LoadingScreenToMiniGame()
    {
        if (!GeneratorActive)
        {
            if(loadingScreen) loadingScreen.SetActive(true);
            yield return new WaitForSeconds(3f);
            onChangingScene?.Invoke();
            filterGame.SetActive(true);
            conserveGame.SetActive(false);
            if(loadingScreen) loadingScreen.SetActive(false);
        
            genUsed = true;
        }

        yield return null;
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
        GeneratorActive = false;
        IsUsingGen = false;
    }
}

