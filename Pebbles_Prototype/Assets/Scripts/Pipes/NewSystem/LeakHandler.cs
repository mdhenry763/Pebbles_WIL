using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LeakHandler : MonoBehaviour
{
    [Header("Sections: ")] public PipeSection[] pipeSections;

    private bool _isLeaking;

    private void Start()
    {
        StartCoroutine(RustNode());
    }

    private void Update()
    {
        foreach (var pipeSection in pipeSections)
        {
            if (pipeSection.pipeStart.IsFlowing && pipeSection.pipeEnd.IsClosed) return;

            if (pipeSection.pipeStart.IsFlowing)
            {
                //Leaking
                Debug.Log("Leaking");
            }
        }
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
