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
    bool hit = false;
    MoveData data;

    public override void Init(GameObject enemy)
    {
        base.Init(enemy);

        p = enemy.GetComponent<Patroller>();
        rb = p.Rb;
        tr = p.Tr;
        points = p.Positions;
        p.Index = 0;
        data = p.MoveData;

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
        destPos = points[p.Index];

        if (hit)
        {
            Next();
            hit = false;
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (hit) return;

        rb.MovePosition(rb.position + MoveUtility.Move((destPos - rb.position), data) * data.Speed * Time.fixedDeltaTime);

        if (MoveUtility.Arrived(rb.position, destPos, data)) Next();
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
        hit = true;
    }
}
