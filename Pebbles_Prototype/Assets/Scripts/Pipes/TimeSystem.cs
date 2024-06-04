using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    public TMP_Text timeTakenText;

    private float time;
    private bool _isTakingTime;

    private void Start()
    {
        _isTakingTime = true;
        time = 0;
    }

    private void Update()
    {
        if(!_isTakingTime) return;

        time += Time.deltaTime;

        timeTakenText.text = $"Time: {Mathf.Floor(time)}";
    }

    public void StopTime()
    {
        _isTakingTime = false;
    }
}
