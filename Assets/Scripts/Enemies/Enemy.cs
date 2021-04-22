using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Enemy : MonoBehaviour
{
    public Animator Animator;

    protected Vector2 startPosition;
    protected Rigidbody2D rb;
    protected Transform tr;

    public Vector2 StartPosition => startPosition;
    public Rigidbody2D Rb => rb;
    public Transform Tr => tr;

    private void Awake()
    {
        tr = transform;
        rb = GetComponent<Rigidbody2D>();
        startPosition = rb.position;
    }

    private void Start()
    {
        foreach (var smb in Animator.GetBehaviours<State>())
        {
            smb.Init(this.gameObject);
        }
    }        
}
