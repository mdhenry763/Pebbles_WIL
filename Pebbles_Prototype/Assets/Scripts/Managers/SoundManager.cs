using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioMixer mixer;
    
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;

    public const string MasterKey = "Master";
    public const string BGMKey = "BGM";
    public const string SFXKey = "SFX";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        SetupMixer();
        SetupSliders();
    }

    private void SetupSliders()
    {
        if (masterSlider != null)
        {
            masterSlider.onValueChanged.AddListener(MasterValueChange);
            masterSlider.value = PlayerPrefs.GetFloat(MasterKey, 0.75f); // Initialize slider value
        }

        if (bgmSlider != null)
        {
            bgmSlider.onValueChanged.AddListener(MusicValueChange);
            bgmSlider.value = PlayerPrefs.GetFloat(BGMKey, 0.75f); // Initialize slider value
        }

        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.AddListener(SFXValueChange);
            sfxSlider.value = PlayerPrefs.GetFloat(SFXKey, 0.75f); // Initialize slider value
        }
    }

    private void SetupMixer()
    {
        var master = PlayerPrefs.GetFloat(MasterKey, 0.75f);
        var bgm = PlayerPrefs.GetFloat(BGMKey, 0.75f);
        var sfx = PlayerPrefs.GetFloat(SFXKey, 0.75f);

        mixer.SetFloat(MasterKey, Mathf.Log10(master) * 20);
        mixer.SetFloat(BGMKey, Mathf.Log10(master) * 20);
        mixer.SetFloat(SFXKey, Mathf.Log10(master) * 20);
    }

    private void MasterValueChange(float value)
    {
        mixer.SetFloat(MasterKey, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(MasterKey, value);
        Debug.Log($"Master volume changed to {value}");
    }
    
    private void MusicValueChange(float value)
    {
        mixer.SetFloat(BGMKey, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(BGMKey, value);
    }
    
    private void SFXValueChange(float value)
    {
        mixer.SetFloat(SFXKey, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(SFXKey, value);
    }
}
