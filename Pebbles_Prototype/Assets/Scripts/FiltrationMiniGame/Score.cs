using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public int currentScore;
    public TMP_Text scoreDisplay;
    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
        scoreDisplay.text = "Score: " + currentScore.ToString();
    }

    public void IncreaseScore()
    {
        currentScore = currentScore + 1;
        updateScore(currentScore);
    }

    public void updateScore(int currentScore)
    {
        scoreDisplay.text = "Score: " + currentScore.ToString();
        //GameManager.Instance.score = currentScore;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
