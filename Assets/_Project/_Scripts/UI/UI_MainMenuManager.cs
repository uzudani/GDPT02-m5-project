using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_MainMenuManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private AudioClip _backgroundMusic;
    [SerializeField] private AudioClip _clickSound;

    private void Start()
    {
        AudioManager.Instance.PlayMusic(_backgroundMusic);
    }
    private void OnEnable()
    {
        _newGameButton.onClick.AddListener(StartNewGame);
        _exitButton.onClick.AddListener(ExitGame);
    }

    private void OnDisable()
    {
        _newGameButton.onClick.RemoveListener(StartNewGame);
        _exitButton.onClick.RemoveListener(ExitGame);
    }

    private void StartNewGame()
    {
        AudioManager.Instance.PlaySFX(_clickSound);

        AudioManager.Instance.StopAudio();

        SceneManager.LoadScene(0);
    }

    private void ExitGame()
    {
        Debug.Log("Stai tentando di chiudere il gioco ma manca la Build!");
        //Application.Quit();
    }
}
