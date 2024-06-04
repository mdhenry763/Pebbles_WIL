using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintSystem : MonoBehaviour
{
    public GameObject hintDisplay;
    public TMP_Text hintText;

    public void DisplayHint(bool display)
    {
        hintDisplay.SetActive(display);
    }

    public void DisplayHint(string hint)
    {
        hintText.text = hint;
    }
}
