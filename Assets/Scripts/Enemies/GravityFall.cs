using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFall : State
{
    Rigidbody2D rb;
    float fallMul;

    public override void Init(GameObject enemy)
    {
        GravitySetter gr = enemy.GetComponent<GravitySetter>();
        fallMul = gr.FallMultiplier;
        rb = gr.Rb;

        Trigger tr = enemy.GetComponent<Trigger>();
        tr.OnTrigger += () => { Fall(tr.Animator); };

        Hitter h = enemy.GetComponent<Hitter>();
        h.OnHit += () => { rb.gravityScale = 0f; };
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("PosReached", false);
    }

    void Fall(Animator anim)
    {
        anim.SetBool("Fall", true);

        rb.gravityScale = fallMul;
    }
}
