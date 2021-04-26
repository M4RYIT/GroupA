using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : State
{
    public bool Collider = true;

    float speed, angle;
    Transform tr;
    Rigidbody2D rb;
    bool hit;

    public override void Init(GameObject enemy)
    {
        base.Init(enemy);

        Mover m = enemy.GetComponent<Mover>();
        speed = m.LinearSpeed;
        angle = m.Angle;
        tr = m.Tr;
        rb = m.Rb;

        if (Collider)
        {
            enemy.GetComponent<Hitter>().OnHit += () => { hit=true; };
        }
        else
        {
            enemy.GetComponent<Trigger>().OnTrigger += () => { hit = true; m.Animator.SetTrigger("Hit"); };
        }
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hit = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (hit) return;

        rb.MoveRotation(angle * Mathf.Sin(speed * Time.fixedDeltaTime));
    }
}
