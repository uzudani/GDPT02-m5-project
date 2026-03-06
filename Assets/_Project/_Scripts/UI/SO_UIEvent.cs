using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New UI Event", menuName = "Events/UI Event")]
public class SO_UIEvent : ScriptableObject
{
    public event Action OnEventCall;

    public void Call()
    {
        OnEventCall?.Invoke();
    }
}
