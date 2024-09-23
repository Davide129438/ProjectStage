using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header ("---------- Audio Sorce ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] public AudioSource WalkSource;
    

    [Header("---------- Audio Clip ----------")]
    public AudioClip background;
    public AudioClip jump;
    public AudioClip collectCoin;
    public AudioClip walk;
    public AudioClip teleportIn;
    public AudioClip Death;

   public static AudioManager instance;

    private void Start()
    {
        instance = this;
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void PlaySFX(AudioClip clip, float volume)
    {
        SFXSource.volume = volume;
        SFXSource.PlayOneShot(clip);
        SFXSource.volume = 1;
    }

    internal static void PlaySFX(object teleport)
    {
        throw new NotImplementedException();
    }
}
