using System;
using UnityEngine;

public class AudioManager1 : MonoBehaviour
{
    [Header ("---------- Audio Sorce ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------- Audio Clip ----------")]
    public AudioClip background;
    public AudioClip jump;
    public AudioClip collectCoin;
    public AudioClip walk;
    public AudioClip teleportIn;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

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
