using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [Header("Sound References:")] 
    [Space]
    public AudioSource audio;
    public AudioSource walkAudioSource;
    
    [Header("Audio Clips:")]
    public AudioClip valveClip;
    public AudioClip puzzleInteractClip;
    public AudioClip wrenchieClip;
    public AudioClip walkClip;

    public void PlayValveClip()
    {
        audio.PlayOneShot(valveClip);
    }

    public void PlayPuzzleClip()
    {
        audio.PlayOneShot(puzzleInteractClip);
    }
    
    
    public void PlayWrenchieClip()
    {
        audio.PlayOneShot(wrenchieClip);
    }
    
    public void PlayWalkClip()
    {
        if (!walkAudioSource.isPlaying)
        {
            walkAudioSource.clip = walkClip;
            walkAudioSource.loop = true;
            walkAudioSource.Play();
        }
    }

    public void StopWalkClip()
    {
        if (walkAudioSource.isPlaying)
        {
            walkAudioSource.Stop();
        }
    }

    
}
