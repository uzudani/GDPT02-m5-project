using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; } // Singleton

    [Header("Settings")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _SFXSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip != null)
        {
            _musicSource.clip = clip;
            _musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            _SFXSource.clip = clip;
            _SFXSource.Play();
        }
    }

    public void StopAudio()
    {
        _musicSource?.Stop();
    }
}
