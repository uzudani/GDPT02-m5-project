using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private SO_UIEvent _winEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_winEvent != null)
            {
                _winEvent.Call(); // Per UI
            }
            Time.timeScale = 0f;
        }
    }
}

