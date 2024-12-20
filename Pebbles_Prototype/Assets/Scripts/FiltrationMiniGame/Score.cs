using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public int currentScore, maxTime = 30, currentTime;
    public TMP_Text scoreDisplay, time;

    public MiniGameScore miniGameScore;

    public GameObject filterGame;

    public GameObject conserveGame;

    public static event Action OnMiniGameComplete;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = maxTime;
        currentScore = 100;
        scoreDisplay.text = "Score: " + currentScore.ToString();
        StartCoroutine("Timer");
    }

    IEnumerator Timer()
    {
        while (true)
        {
            currentTime--;
            if (currentTime <= 0)
            {
                EndFiltration();
            }
            yield return new WaitForSeconds(1);
        }
    }
    
    public void DecreaseScore()
    {
        currentScore = currentScore - 10;
        updateScore(currentScore);
    }

    public void EndFiltration()
    {
        StopCoroutine("Timer");
        miniGameScore.miniGameScore = currentScore;
        conserveGame.SetActive(true);
        CrossSceneEvents.FireOnMiniGameFinished();
        OnMiniGameComplete?.Invoke();
    }
    
    
    
    public void updateScore(int currentScore)
    {
        scoreDisplay.text = "Score: " + currentScore.ToString();
        //GameManager.Instance.score = currentScore;
    }
    // Update is called once per frame
    void Update()
    {
        time.text = "Time: " + currentTime;
    }
}