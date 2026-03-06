using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_PauseManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _exitButton;

    private bool _isPaused = false;

    private void OnEnable()
    {
        _resumeButton.onClick.AddListener(ResumeGame);
        _exitButton.onClick.AddListener(ToMainMenu);
    }

    private void OnDisable()
    {
        _resumeButton.onClick.RemoveListener(ResumeGame);
        _exitButton.onClick.RemoveListener(ToMainMenu);
    }

    private void Update()
    {
        SwitchLogic();
    }

    private void SwitchLogic()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (_isPaused) ResumeGame();
            else PauseGame();
        }
    }

    private void PauseGame()
    {
        _isPaused = true;
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        _isPaused = false;
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    private void ToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}
