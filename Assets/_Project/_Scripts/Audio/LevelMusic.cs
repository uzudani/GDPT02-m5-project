using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private AudioClip _levelMusic;
    private void Start()
    {
        if (AudioManager.Instance != null && _levelMusic != null)
        {
            AudioManager.Instance.PlayMusic(_levelMusic);
        }
    }
}
