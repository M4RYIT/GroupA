using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Patrol : State
{
    public bool Collider = true, Rotation = false;

    Patroller p;
    List<Vector2> points;
    Vector2 destPos;
    int increment = -1;
    Rigidbody2D rb;
    Transform tr;
    MoveData data;

    public override void Init(Enemy enemy)
    {       
        if (enemy is Patroller)
        {
            p = enemy as Patroller;
            points = p.Positions;
            p.Index = 0;
            data = p.MoveData;
            p.Hit = false;
        }       

        if (Collider)
        {
            Hitter h = enemy as Hitter;
            if (h!=null) h.OnHit += () => { Hit(); };
        }
        else
        {
            Trigger trg = enemy as Trigger;
            if (trg!=null) trg.OnTrigger += () => { Hit(); trg.Animator.SetTrigger("Hit"); trg.Entered = false; };
        }

        rb = enemy.Rb;
        tr = enemy.Tr;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        destPos = points[p.Index];

        if (p.Hit)
        {
            Next();
            p.Hit = false;
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (p.Hit) return;

        rb.MovePosition(rb.position + MoveUtility.Move((destPos - rb.position), data) * data.Speed * Time.fixedDeltaTime);

        if (MoveUtility.Arrived(rb.position, destPos, data))
        {
            Next();
        }
    }

    void Next()
    {
        Vector3 dir = destPos;

        p.Index = (p.Index + increment + points.Count) % points.Count;

        destPos = points[Mathf.Abs(p.Index)];

        dir = (Vector3)destPos - dir;

        tr.localScale = new Vector3(Mathf.Sign((destPos - rb.position).x), 1f, 1f);

        if (Rotation)
        {
            tr.up = Vector3.Cross(dir.normalized, Vector3.forward * increment);
        }
    }

    void Hit()
    {
        increment *= -1;
        p.Hit = true;
    }
}
