using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingAudio : MonoBehaviour
{
    [Header("---------- Audio Sorce ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------- Audio Clip ----------")]
    public AudioClip background;
    public AudioClip clickbutton;




    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    internal static void PlaySFX(object teleport)
    {
        throw new NotImplementedException();
    }
}


