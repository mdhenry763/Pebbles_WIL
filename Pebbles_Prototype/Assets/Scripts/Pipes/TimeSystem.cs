using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    public TMP_Text timeTakenText;
    public MiniGameScore score;
    public PlayerEventSystemSO playerEvents;

    private float time;
    private bool _isTakingTime;

    private void Start()
    {
        _isTakingTime = false;
        time = 0;
    }

    private void Update()
    {
        if(!_isTakingTime) return;

        time += Time.deltaTime;
        score.timeScore = 150 / time;
        
        timeTakenText.text = $"Time: {Mathf.Floor(time)}";
    }

    public void StartTime()
    {
        _isTakingTime = true;
    }

    public void StopTime()
    {
        _isTakingTime = false;
    }
}
