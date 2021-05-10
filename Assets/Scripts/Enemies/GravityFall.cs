using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFall : State
{
    Rigidbody2D rb;
    float fallMul;
    Trigger trg;

    public override void Init(Enemy enemy)
    {
        rb = enemy.Rb;

        GravitySetter gr = enemy as GravitySetter;
        if (gr!=null) fallMul = gr.FallMultiplier;

        if (enemy is Trigger)
        {
            trg = enemy as Trigger;
            trg.OnTrigger += () => { Fall(trg.Animator); };
            trg.OnPlayerDeath += () => { PosCheck(); };
        }

        Hitter h = enemy as Hitter;
        if (h!=null) h.OnHit += () => { h.Animator.SetBool("PosReached", false); rb.gravityScale = 0f; };
    }    

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;
        trg.Entered = false;
        trg.TriggerObject.SetActive(true);        
    }

    void PosCheck()
    {
        if (trg.TriggerObject.activeSelf && rb.position!=trg.StartPosition)
        {
            Fall(trg.Animator);
        }
    }

    void Fall(Animator anim)
    {
        trg.TriggerObject.SetActive(false);

        anim.SetBool("PosReached", false);
        anim.SetTrigger("Fall");

        rb.gravityScale = fallMul;
    }
}
