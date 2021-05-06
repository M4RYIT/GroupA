using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EventType
{
    TriggerEnter,
    TriggerExit,
    CollisionEnter,
    CollisionExit,
    ParticleStop
}

public class TriggerEvent : MonoBehaviour
{
    public EventType EventType;
    public UnityEvent OnTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (EventType == EventType.TriggerEnter) OnTrigger?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (EventType == EventType.TriggerExit) OnTrigger?.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (EventType == EventType.CollisionEnter) OnTrigger?.Invoke();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (EventType == EventType.CollisionExit) OnTrigger?.Invoke();
    }

    public void OnParticleSystemStopped()
    {
        if (EventType == EventType.ParticleStop) OnTrigger?.Invoke();
    }
}
