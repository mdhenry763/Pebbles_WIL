using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using System.Timers;

public class buttonChecker : MonoBehaviour
{
    public UnityEngine.UI.Button Quit;
    public UnityEngine.UI.Button start;
    public UnityEngine.UI.Button Settings;
    public UnityEngine.UI.Button returns;
    public UnityEngine.UI.Button Menu;

    public AudioSource Click;


    // Start is called before the first frame update
    void Start()
    {
        Click = GetComponent<AudioSource>();
        start.onClick.AddListener(playUIClickSTART);
        Quit.onClick.AddListener(playUIClickQUIT);
        Settings.onClick.AddListener(playsound);
    }

    // Update is called once per frame
    void Update()
    {
    }
   
    void playsound()
    {
        StartCoroutine(playSounds());

    }
    void playUIClickSTART()
    {
        float elapsed = 0;
        while (elapsed< 18)
        {

            StartCoroutine(playSounds());
            elapsed += Time.deltaTime;
        }
      //  GameManager.Instance.onStartClicked();
    }
    void playUIClickQUIT()
    {
        StartCoroutine(playSounds());
        float elapsed = 0;
        while (elapsed < 18)
        {

            elapsed += Time.deltaTime;
        }
            StartCoroutine(playSounds());

      //  GameManager.Instance.onQuitClicked();

    }

    IEnumerator playSounds()
    {

        //SoundManager.Instance.playsound(Click.clip, Click);
        yield return null;
    }

}
