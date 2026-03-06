using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject _interactionImage;
    [SerializeField] private SO_UIEvent _showEvent;
    [SerializeField] private SO_UIEvent _hideEvent;

    private void OnEnable()
    {
        if (_showEvent != null) _showEvent.OnEventCall += ShowUI;
        if (_hideEvent != null) _hideEvent.OnEventCall += HideUI;
    }

    private void OnDisable()
    {
        if (_showEvent != null) _showEvent.OnEventCall -= ShowUI;
        if (_hideEvent != null) _hideEvent.OnEventCall -= HideUI;
    }

    private void ShowUI() => _interactionImage.SetActive(true);
    private void HideUI() => _interactionImage.SetActive(false);
}
