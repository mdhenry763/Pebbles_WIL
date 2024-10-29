using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSaysButton : MonoBehaviour
{
    public Material material;     
    private Color baseColor;        
    private float emissionStrength = 5.0f; 
    public SimonSaysButtonManager _SimonSaysButtonManager;
    void Start()
    {
        _SimonSaysButtonManager = GameObject.FindGameObjectWithTag("SimonSaysButtonManager")
            .GetComponent<SimonSaysButtonManager>();
        
        if (material == null)
            material = GetComponent<Renderer>().material;

        baseColor = material.color;
    }

    private void OnMouseOver()
    {
        material.SetColor("_EmissionColor", baseColor * 2);
    }

    private void OnMouseExit()
    {
        material.SetColor("_EmissionColor", Color.black); 
    }

    private void OnMouseDown()
    {
        _SimonSaysButtonManager.ButtonPressed((_SimonSaysButtonManager.buttons.IndexOf(this)));
    }

    public void LightUp()
    {
        material.EnableKeyword("_EMISSION");
        material.SetColor("_EmissionColor", baseColor * emissionStrength);
    }

    public void ResetLight()
    {
        material.SetColor("_EmissionColor", Color.black); // Return to default (no emission)
    }
}
