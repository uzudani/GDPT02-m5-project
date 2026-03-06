using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInteractionHandler : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private SO_UIEvent _showEvent;
    [SerializeField] private SO_UIEvent _hideEvent;

    private IInteractable _currentInteractable;


    private void Update()
    {
        InteractLogic();
    }

    private void InteractLogic()
    {
        if (_currentInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            _currentInteractable.Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            _currentInteractable = interactable;

            if (_showEvent != null)
                _showEvent.Call();

            Debug.Log("Puoi interagire con E!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.TryGetComponent(out IInteractable interactable) && interactable == _currentInteractable))
        {
            _currentInteractable = null;

            if (_hideEvent != null)
                _hideEvent.Call();

            Debug.Log("Puoi interagire con E!");
        }
    }
}
