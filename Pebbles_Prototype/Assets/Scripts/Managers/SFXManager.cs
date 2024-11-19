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
    public AudioClip jumpClip;
    public AudioClip buttonClip;
    public AudioClip buttonEnterClip;
    public AudioClip shootSound;
    public AudioClip shootHit;
    public AudioClip winSound;

    public void PlayValveClip()
    {
        audio.PlayOneShot(valveClip);
    }

    public void PlayPuzzleClip()
    {
        audio.PlayOneShot(puzzleInteractClip);
    }

    public void PlayJumpSound()
    {
        audio.PlayOneShot(jumpClip);
    }

    public void PlayWinSound()
    {
        audio.PlayOneShot(winSound);
    }

    public void PlayBtnSound()
    {
        audio.PlayOneShot(buttonClip);
    }

    public void PlayEnterBtnSound()
    {
        audio.PlayOneShot(buttonEnterClip);
    }

    public void PlayShootSound()
    {
        audio.PlayOneShot(shootSound);
    }

    public void PlayHitSound()
    {
        audio.PlayOneShot(shootHit);
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
