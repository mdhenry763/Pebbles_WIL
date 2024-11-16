using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpMielies : MonoBehaviour
{
    [SerializeField] private float endPositionY = 1;
    [SerializeField] private float lerpSpeed = 3;

    private Vector3 _startPosition;
    private Vector3 _currentPos;
    private Vector3 _endPosition;

    private void Start()
    {
        _startPosition = transform.position;
        _currentPos = _startPosition;
        _endPosition = _startPosition;
        _endPosition.y = 1;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(_currentPos, _endPosition, lerpSpeed * Time.deltaTime);
        _currentPos = transform.position;
    }
}
