using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LeakHandler : MonoBehaviour
{
    [Header("Sections: ")] public PipeSection[] pipeSections;
    
    [Header("Score")] 
    public float leakScoreMax = 80;
    public float leakMultiplier = 1f;

    [Header("UI")]
    public Image leakBar;
    public TMP_Text leakText;

    private float _leakScore = 80;
    private bool _canLeak;
    
    

    private void Start()
    {
        //StartCoroutine(RustNode());

        _leakScore = leakScoreMax;
    }
    

    public float GetLeakScore()
    {
        return _leakScore;
    }

    private void Update()
    {
        if(_canLeak) return;
        
        foreach (var pipeSection in pipeSections)
        {
            if (pipeSection.pipeStart.IsFlowing && pipeSection.pipeEnd.IsClosed)
            {
                leakText.text = "";
                continue;
            }

            if (pipeSection.pipeStart.IsFlowing)
            {
                //Leaking
                _leakScore -= Time.deltaTime * leakMultiplier;
                Debug.Log(_leakScore);
                
                if(leakBar == null) return;

                leakBar.fillAmount = _leakScore / leakScoreMax;
                
                if(leakText == null) return;

                leakText.text = "Leaking";
            }
        }
    }

    public void StartLeakSystem()
    {
        _canLeak = false;
    }

    public void StopLeakSystem()
    {
        _canLeak = true;
    }

    private bool CheckIfPipesConnected()
    {
        //Check start and end Nodes for Start Node: IsFlowing and End Node: !IsClosed then leaking
        var pipeSection = pipeSections[Random.Range(0, pipeSections.Length)];

        if (!pipeSection.pipeStart.IsFlowing || !pipeSection.pipeEnd.IsClosed) return false;
        
        var pipe = pipeSection.pipes[Random.Range(0, pipeSection.pipes.Length)];
        pipe.Rusting();
        return true;

    }

    IEnumerator RustNode()
    {
        while (true)
        {
            CheckIfPipesConnected();
            yield return new WaitForSeconds(5);
        }
        
        //TODO: Select Random Pipe Section and select random node to rust
    }
}



[Serializable]
public struct PipeSection
{
    public Node pipeStart;
    public Node connection;
    public Node[] pipes;
    public Node pipeEnd;
}
