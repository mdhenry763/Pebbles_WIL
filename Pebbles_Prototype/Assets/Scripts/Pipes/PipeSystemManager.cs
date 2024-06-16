using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PipeSystemManager : MonoBehaviour
{
    [Header("Pipes")]
    public Transform startingPipe;
    public Transform endPipe;
    [SerializeField] private Material unconnectedMat;
    [SerializeField] private Material connectedMat;
    [SerializeField] private Material rustMat;

    [Header("Generator: ")]
    public Interaction generator;

    public int numPipesTillGen;

    [Header("UI")] public TMP_Text leakText;
    public Image waterSlider;
    public float leakageMax = 60f;
    public TMP_Text scoreText;
    public MiniGameScore score;

    [Header("Debug")] public List<Transform> pipeSystem;
    [Header("Debug")] public List<Transform> connectedPipes;

    [Header("Rust Mechanic: ")] public Vector2 rustRandomTime = new Vector2(10, 20);

    private int _pipeSystemCount;
    private bool _miniGameComplete;
    private bool _pipeSystemComplete;

    public UnityEvent onGameEnd;
    public UnityEvent<string> onPipeRusting;

    private void Start()
    {
        GetFullPipeSystem();
        time = 0;
        amount = 0;
        _leakingPipe = false;
        _pipesConnected = false;
        
        //Start Rust Mechanic
        StartCoroutine(RustMechanic());

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
    private bool _pipesConnected;
    
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
                
                if (LeakingPipe(pipe, currentPipe)) break;
                pipe.ChangeColour(connectedMat, unconnectedMat);
                connectedPipes.Add(currentPipe);

                PipeSystemChecking();
            }

            currentPipe = pipe.ConnectedTo;

            if (currentPipe == endPipe)
            {
                break;
            }
        }
        ChangePipeVisual();
    }

    private bool LeakingPipe(PipeC pipe, Transform currentPipe)
    {
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
            return true;
        }

        return false;
    }

    private void PipeSystemChecking()
    {
        if (connectedPipes.Count > 0) _pipesConnected = true;
        else
        {
            _pipesConnected = false;
        }

        if (connectedPipes.Count >= numPipesTillGen)
        {
            generator.GeneratorActive = true;
        }
                
        if (connectedPipes.Count >= pipeSystem.Count-1)
        {
                    
            _pipeSystemComplete = true;
            CheckIfLevelComplete();
        }
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

    private bool _isPipeRusting;
    private float _rustTimer;
    
    public IEnumerator RustMechanic()
    {
        _isPipeRusting = false;
        _rustTimer = 0;
        
        float beginRustTime = Random.Range(rustRandomTime.x, rustRandomTime.y);
        
        while (true)
        {
            if (_pipesConnected && !_isPipeRusting)
            {
                _rustTimer += Time.deltaTime;
                Debug.Log($"Rust Timer: {_rustTimer} | {beginRustTime}");

                if (_rustTimer >= beginRustTime)
                {
                    //Handle Rust
                    if(!generator.IsUsingGen) Rust();

                    _rustTimer = 0;
                    beginRustTime = Random.Range(rustRandomTime.x, rustRandomTime.y);
                }

                if (_isPipeRusting)
                {
                    _rustTimer = 0;
                    break;
                }
                
            }
            
            yield return null;
        }

        yield return null;
    }

    void Rust()
    {
        var pipeToRust = connectedPipes[Random.Range(1, connectedPipes.Count)];
        var pipe = pipeToRust.GetComponent<PipeC>();
        
        pipe.RustPipe(rustMat);
        onPipeRusting?.Invoke("A pipe in the system is rusting! Fix the rusting pipe by clicking on it.");
        _isPipeRusting = true;

    }

}

