using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableButton : MonoBehaviour, IInteractable
{
    [Header("Settings")]
    [SerializeField] private UnityEvent _onInteract;
    [SerializeField] private AudioClip _SFX;

    public void Interact()
    {
        AudioManager.Instance.PlaySFX(_SFX);
        _onInteract?.Invoke();
    }
}
