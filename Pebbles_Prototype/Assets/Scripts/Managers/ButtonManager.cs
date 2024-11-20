using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameManager _GameManager;
    // Start is called before the first frame update
    void Start()
    {
        _GameManager = GameManager.Instance;
    }

    public void UpdateJournal()
    {
        _GameManager._JournalManager =
            GameObject.FindGameObjectWithTag("JournalManager").GetComponent<JournalManager>();
        _GameManager._JournalManager.UpdateJournalText();
    }
    public void LoadFiltrationMiniGame()
    {
        SceneManager.LoadScene(2);
    }

    public void WatchVideo()
    {
        SceneManager.LoadScene("Video");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
