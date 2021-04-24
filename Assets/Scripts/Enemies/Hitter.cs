using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hitter : Enemy
{
    protected GameObject hitted;
    protected Action onHit;
    public Action OnHit { get => onHit; set => onHit = value; }
    public GameObject Hitted => hitted;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<GravityObject>() != null ||
            collision.gameObject.GetComponent<BulletMove>() != null) return;

        hitted = collision.gameObject;

        onHit?.Invoke();

        Animator.SetTrigger("Hit");
    }
}
