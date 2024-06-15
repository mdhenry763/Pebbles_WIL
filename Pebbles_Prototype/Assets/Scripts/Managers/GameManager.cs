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
    
    
    // Start is called before the first frame update
    void Start()
    {
        tutorialScore = 0;
        temperatureMiniGameScore = 0;
        level1Score = 0;
        level2Score = 0;
        filtrationMiniGameScore = 0;
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
