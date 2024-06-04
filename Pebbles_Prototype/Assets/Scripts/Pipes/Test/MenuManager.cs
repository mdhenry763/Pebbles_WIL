using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public string loadScene0 = "MainMenu";
    public string loadScene1 = "MichaelSandbox";

    public void LoadScene0()
    {
        SceneManager.LoadScene(loadScene0);
    }
    
    public void LoadScene1()
    {
        SceneManager.LoadScene(loadScene1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
