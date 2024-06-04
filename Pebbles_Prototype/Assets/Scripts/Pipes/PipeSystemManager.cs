using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PipeSystemManager : MonoBehaviour
{
    [Header("Pipes")]
    public Transform startingPipe;
    public Transform endPipe;
    [SerializeField] private Material unconnectedMat;
    [SerializeField] private Material connectedMat;

    [Header("UI")] public TMP_Text leakText;
    public Image waterSlider;
    public float leakageMax = 60f;
    public TMP_Text scoreText;
    public MiniGameScore score;

    [Header("Debug")] public List<Transform> pipeSystem;
    [Header("Debug")] public List<Transform> connectedPipes;

    private int _pipeSystemCount;
    private bool _miniGameComplete;
    private bool _pipeSystemComplete;

    public UnityEvent onGameEnd;

    private void Start()
    {
        GetFullPipeSystem();
        time = 0;
        amount = 0;
        _leakingPipe = false;

    }

    private void OnEnable()
    {
        PipeC.OnIsConnectedValueChange += PipeCOnOnIsConnectedValueChange;
    }
    
    private void OnDisable()
    {
        PipeC.OnIsConnectedValueChange -= PipeCOnOnIsConnectedValueChange;
    }

    private void Update()
    {
        if (_leakingPipe)
        {
            time += Time.deltaTime;
            float remainingTime = leakageMax - time; 
            float amount = remainingTime / leakageMax;
            Debug.Log($"Amount {amount}");
            waterSlider.fillAmount = amount;
        }
    }

    private void PipeCOnOnIsConnectedValueChange()
    {
        CheckPipeSystem();
    }

    private PipeC leakingPipe;
    private bool _leakingPipe;
    private float time;
    private float amount;
    
    private void CheckPipeSystem()
    {
        connectedPipes.Clear();
        Debug.Log("Checking Pipe System!");
        Transform currentPipe = startingPipe;

        while (currentPipe != null)
        {
            var pipe = currentPipe.GetComponent<PipeC>();
            
            if (pipe.ConnectedTo != null)
            {
                if (pipe.pipeType == PipeType.Normal)
                {
                    pipe.IsConnected = true;
                }

                if (pipe == leakingPipe)
                {
                    if (pipe.IsConnected)
                    {
                        leakText.text = "";
                        leakingPipe = null;
                        _leakingPipe = false;
                    }
                }
                
                if (!pipe.IsConnected)
                {
                    if (leakingPipe == null)
                    {
                        if (pipe.pipeType == PipeType.Connection)
                        {
                            leakingPipe = pipe;
                            _leakingPipe = true;
                            leakText.text = "Leak";
                            Debug.Log("Leakage");
                        }
                    }
                    
                    Debug.Log($"{currentPipe.name} is not connected");
                    break;
                }
                pipe.ChangeColour(connectedMat, unconnectedMat);
                connectedPipes.Add(currentPipe);
                
                if (connectedPipes.Count >= pipeSystem.Count-1)
                {
                    
                    _pipeSystemComplete = true;
                    CheckIfLevelComplete();
                }
            }

            currentPipe = pipe.ConnectedTo;

            if (currentPipe == endPipe)
            {
                break;
            }
        }
        ChangePipeVisual();
    }

    void CheckIfLevelComplete()
    {
        if (_miniGameComplete && _pipeSystemComplete)
        {
            score.leakScore = amount;
            float gameScore = (score.miniGameScore + score.timeScore + score.leakScore / 3);
            scoreText.text = "Score: " + gameScore.ToString();
            Debug.Log("Game Over");
            onGameEnd?.Invoke();
        }
        
    }

    private void ChangePipeVisual()
    {
        bool disconnected = false;
        
        foreach (var pipe in pipeSystem)
        {
            var pipeC = pipe.GetComponent<PipeC>();
            
            if(pipeC == null) return;

            if (!disconnected)
            {
                disconnected = !pipeC.IsConnected; 
            }

            pipeC.IsConnected = !disconnected;
            pipeC.ChangeColour(connectedMat, unconnectedMat);
        }
    }

    private void GetFullPipeSystem()
    {
        Transform currentPipe = startingPipe;

        while (currentPipe != null)
        {
            var pipe = currentPipe.GetComponent<PipeC>();
            if (pipe.ConnectedTo != null)
            {
                pipeSystem.Add(currentPipe);
                _pipeSystemCount++;
                Debug.Log($"Pipes: {_pipeSystemCount}");
            }

            currentPipe = pipe.ConnectedTo;

            if (currentPipe == endPipe)
            {
                break;
            }
        }
    }
    
    //
    public void SetMiniGameComplete()
    {
        Debug.Log("Mini-Game complete");
        _miniGameComplete = true;
        CheckIfLevelComplete();
    }


}
