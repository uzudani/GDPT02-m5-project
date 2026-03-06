using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTotem : MonoBehaviour, IInteractable
{
    [Header("Settings")]
    [SerializeField] private float _stunTime = 5f;
    [SerializeField] private AudioClip _SFX;

    private bool _inCooldown = false;

    public static event Action <float> OnStunTriggered;
    public static event Action OnStunEnded;

    public void Interact()
    {
        if (!_inCooldown)
        {
            AudioManager.Instance.PlaySFX(_SFX);
            StartCoroutine(StunnedRoutine());
        }
        else
        {
            Debug.Log("Non puoi ancora usare il totem");
        }
    }

    private IEnumerator StunnedRoutine()
    {
        _inCooldown = true;
        Debug.Log("Hai stunnati i nemici!");

        OnStunTriggered?.Invoke(_stunTime);

        yield return new WaitForSeconds(_stunTime);

        OnStunEnded?.Invoke();

        Debug.LogWarning("I nemici si sono ripresi!");
        _inCooldown = false;
    }
}
