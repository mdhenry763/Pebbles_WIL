using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeakHandler : MonoBehaviour
{
    [Header("Sections: ")] public PipeSection[] pipeSections;

    private bool _isLeaking;

    private void CheckForLeak()
    {
        //Check start and end Nodes for Start Node: IsFlowing and End Node: !IsClosed then leaking
    }

    IEnumerator RustNode()
    {
        yield return new WaitForSeconds(5);
        //TODO: Select Random Pipe Section and select random node to rust
    }
}



[Serializable]
public struct PipeSection
{
    public Node pipeStart;
    public Node[] pipes;
    public Node pipeEnd;
}
