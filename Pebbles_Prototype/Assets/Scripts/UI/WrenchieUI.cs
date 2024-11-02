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

    public PlayerEventSystemSO playerEvents;
    
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wrenchieUI.SetActive(true);
            
            playerEvents.FireWrenchieEvent();
            playerEvents.FireUIEvent(true);

            if (other.TryGetComponent<MovementTest>(out var test))
            {
                test.DisableInput();
                test.ActivateWrenchie();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wrenchieUI.SetActive(false);
            playerEvents.FireUIEvent(false);
            
            if (other.TryGetComponent<MovementTest>(out var test))
            {
                test.EnableInput();
            }
        }
    }

    public void CompletedFirstPuzzle()
    {
        header.text = "Good Job";
        body.text = "Nice one on solving the first puzzle. Jump over the pipes and unlock the valves!";
        wrenchieUI.SetActive(true);
    }
    
}
