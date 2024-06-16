using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WrenchieHint : MonoBehaviour
{
    public GameObject hintObj;
    public TMP_Text hintText;
    public Image hintImage;

    private float progress = 5;

    public void ShowHint(string hint)
    {
        StopCoroutine(HideImage());
        
        Color color = new Color(255, 255, 255, 255);
        hintImage.color = color;
        
        hintText.text = hint;
        hintObj.SetActive(true);
        
        StartCoroutine(HideImage());
    }

    IEnumerator HideImage()
    {
         progress = 5;
        Color color = new Color(255, 255, 255, 255);
        
        while (progress > 0)
        {
            progress -= Time.deltaTime;

            float alpha = (progress / 5);
            color.a = alpha;
            hintImage.color = color;
            yield return null;

        }
        hintObj.SetActive(false);
        
    }
}
