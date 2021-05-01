using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Trigger : Enemy
{
    public GameObject TriggerObject;

    protected GameObject triggered;
    protected Action onTrigger;

    public GameObject Triggered => triggered;
    public Action OnTrigger { get => onTrigger; set => onTrigger = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            triggered = collision.gameObject;

            onTrigger?.Invoke();
        }
    }

    private void OnDestroy()
    {
        //Reset action
        onTrigger = null;
    }
}
