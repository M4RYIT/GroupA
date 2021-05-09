using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : State
{
    public bool Collider = true;

    float speed;
    Rigidbody2D rb;
    bool hit;

    public override void Init(Enemy enemy)
    {
        Mover m = enemy as Mover;

        if (m!=null) speed = m.AngularSpeed;        

        if (Collider)
        {
            Hitter h = enemy as Hitter;
            if (h!=null) h.OnHit += () => { hit=true; };
        }
        else
        {
            Trigger trg = enemy as Trigger;
            if (trg!=null) trg.OnTrigger += () => { trg.Sound.PlayOneShot("Hit"); hit = true; trg.Animator.SetTrigger("Hit"); trg.Entered = false; };
        }

        rb = enemy.Rb;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hit = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (hit) return;

        //Pendulum Movement
        //rb.MoveRotation(angle * Mathf.Sin(speed * Time.fixedTime));

        rb.MoveRotation(rb.rotation + speed * Time.fixedDeltaTime);
    }
}
