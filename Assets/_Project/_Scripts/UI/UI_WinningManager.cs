using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_WinningManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private SO_UIEvent _winEvent;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private Button _newGameButton;
    [SerializeField] private AudioClip _SFX;

    private void OnEnable()
    {
        if (_winEvent != null) _winEvent.OnEventCall += ShowUI;

        _newGameButton.onClick.AddListener(RestartGame);
    }
    private void OnDisable()
    {
        if (_winEvent != null) _winEvent.OnEventCall -= ShowUI;

        _newGameButton.onClick.RemoveListener(RestartGame);
    }

    private void ShowUI()
    {
        AudioManager.Instance.PlaySFX(_SFX);
        _winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(0);
    }
}
