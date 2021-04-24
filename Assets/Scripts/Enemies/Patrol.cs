using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Patrol : State
{
    public bool Collider = true, Rotation = false;

    List<Vector2> points;
    Vector2 destPos;
    int index = -1, increment = 1;
    Rigidbody2D rb;
    Transform tr;
    float speed;
    bool hit;

    public override void Init(GameObject enemy)
    {
        base.Init(enemy);

        Patroller p = enemy.GetComponent<Patroller>();
        speed = p.Speed;
        rb = p.Rb;
        tr = p.Tr;
        index = -1; 
        points = p.Positions; 
        destPos = points[0];

        if (Collider)
        {
            enemy.GetComponent<Hitter>().OnHit += () => { Hit(); };
        }
        else
        {
            enemy.GetComponent<Trigger>().OnTrigger += () => { Hit(); p.Animator.SetTrigger("Hit"); };
        }
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hit = false;
        
        //Next();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (hit) return;

        rb.MovePosition(rb.position + (destPos - rb.position).normalized * speed * Time.fixedDeltaTime);

        if (Vector2.Distance(rb.position, destPos) <= 0.05f) Next();
    }

    void Next()
    {
        Vector3 dir = destPos;

        index = (index + increment) % points.Count;

        destPos = points[Mathf.Abs(index)];

        dir = (Vector3)destPos - dir;

        tr.localScale = new Vector3(Mathf.Sign((destPos - rb.position).x), 1f, 1f);

        if (Rotation)
        {
            tr.up = Vector3.Cross(dir.normalized, Vector3.forward);
        }
    }

    void Hit()
    {
        increment *= -1;
        hit = true;
    }
}
