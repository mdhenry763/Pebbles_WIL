using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WrenchieUI : MonoBehaviour
{
    public GameObject wrenchieUI;
    public TMP_Text header;
    public TMP_Text body;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wrenchieUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wrenchieUI.SetActive(false);
        }
    }

    public void CompletedFirstPuzzle()
    {
        header.text = "Good Job";
        body.text = "Nice one on solving the first puzzle. Jump over the pipes and unlock the valves!";
        wrenchieUI.SetActive(true);
    }
    
}
