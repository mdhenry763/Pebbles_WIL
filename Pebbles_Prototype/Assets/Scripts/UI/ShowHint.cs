using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShowHint : MonoBehaviour
{
    public Node node;

    public UnityEvent onShowHint;

    private bool _showHint;

    // Update is called once per frame
    void Update()
    {
        if (_showHint) return;
        if (!node.IsClosed) return;
            
        onShowHint?.Invoke();
        _showHint = true;
    }
}
