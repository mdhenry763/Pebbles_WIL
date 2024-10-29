using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimonSaysUIManager : MonoBehaviour
{
    public SimonSaysButtonManager _SimonSaysButtonManager;
    public TMP_Text score;
    // Start is called before the first frame update
    void Start()
    {
        _SimonSaysButtonManager = GameObject.FindGameObjectWithTag("SimonSaysButtonManager")
            .GetComponent<SimonSaysButtonManager>();
    }

    public void UpdateScorUI()
    {
        score.text = "Score: " + _SimonSaysButtonManager.currentScore + "/6";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
