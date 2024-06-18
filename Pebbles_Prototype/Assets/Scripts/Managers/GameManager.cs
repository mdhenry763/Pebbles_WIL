using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    
    public float tutorialScore { get; set; }
    public float level1Score { get; set; }
    public float level2Score { get; set; } 
    public float filtrationMiniGameScore { get; set; } 
    public float temperatureMiniGameScore { get; set; }

    public JournalManager _JournalManager;
    
    // Start is called before the first frame update
    void Start()
    {
        //_JournalManager = GameObject.FindGameObjectWithTag("JournalManager").GetComponent<JournalManager>();
        tutorialScore = 100;
        temperatureMiniGameScore = 0;
        level1Score = 0;
        level2Score = 0;
        filtrationMiniGameScore = 0;
    }

    public void UpdateScore(int level, float score)
    {
        switch (level)
        {
            case 0:
                if (tutorialScore < score)
                {
                    tutorialScore = score; 
                }
               
                break;
            case 1:
                if (level1Score < score)
                {
                    level1Score = score;
                }
                
                break;
            case 2:
                if (level2Score < score)
                {
                    level2Score = score;
                }
               
                break;
            default: break;
        }
        //_JournalManager.UpdateJournalText();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
