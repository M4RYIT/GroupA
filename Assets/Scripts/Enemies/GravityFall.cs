using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFall : State
{
    Rigidbody2D rb;
    float fallMul;
    Trigger trg;

    public override void Init(GameObject enemy)
    {
        if (init) return;

        init = true;

        GravitySetter gr = enemy.GetComponent<GravitySetter>();
        fallMul = gr.FallMultiplier;
        rb = gr.Rb;

        trg = enemy.GetComponent<Trigger>();
        trg.OnTrigger += () => { Fall(trg.Animator); };

        Hitter h = enemy.GetComponent<Hitter>();
        h.OnHit += () => { h.Animator.SetBool("PosReached", false); rb.gravityScale = 0f; };
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;
        trg.Entered = false;
        trg.TriggerObject.SetActive(true);        
    }

    void Fall(Animator anim)
    {
        trg.TriggerObject.SetActive(false);

        anim.SetBool("PosReached", false);
        anim.SetTrigger("Fall");

        rb.gravityScale = fallMul;
    }
}
