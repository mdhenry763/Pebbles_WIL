using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameScore : MonoBehaviour
{
    public TMP_Text scoreTxt;

    public void SetScoreTxt(string text)
    {
        scoreTxt.text = $"{text}";
    }
}
