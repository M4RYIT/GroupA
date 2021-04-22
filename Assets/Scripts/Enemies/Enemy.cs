using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Enemy : MonoBehaviour
{
    public Animator Animator;

    Vector2 startPosition;
    Rigidbody2D rb;
    Transform tr;
    GameObject other;
    Action onHit, onTrigger;

    public Vector2 StartPosition => startPosition;
    public Rigidbody2D Rb => rb;
    public Transform Tr => tr;
    public GameObject Other => other;
    public Action OnHit { get => onHit; set => onHit = value; }
    public Action OnTrigger { get => onTrigger; set => onTrigger = value; }

    private void Awake()
    {
        tr = transform;
        rb = GetComponent<Rigidbody2D>();
        startPosition = rb.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var smb in Animator.GetBehaviours<State>())
        {
            smb.Init(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<GravityObject>() != null) return;       
        
        other = collision.gameObject;

        onHit?.Invoke();

        Animator.SetTrigger("Hit");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            other = collision.gameObject;

            onTrigger?.Invoke();
        }
    }    
}
