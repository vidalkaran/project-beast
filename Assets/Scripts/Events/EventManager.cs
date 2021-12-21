using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager current; // Singletone pattern

    public void Awake()
    {
        current = this;
    }

    public void TriggerEvent(Action gameEvent)
    {
        gameEvent?.Invoke();
    }
}
