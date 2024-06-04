using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public int currentScore, maxTime = 100, currentTime;
    public TMP_Text scoreDisplay, time;

    public MiniGameScore miniGameScore;

    public GameObject filterGame;

    public GameObject conserveGame;
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
        miniGameScore.score = currentScore;
        CrossSceneEvents.FireOnMiniGameFinished();
        filterGame.SetActive(false);
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