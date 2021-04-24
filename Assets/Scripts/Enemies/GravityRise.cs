using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityRise : State
{
    Rigidbody2D rb;
    float riseMul;
    Vector2 start;

    public override void Init(GameObject enemy)
    {
        base.Init(enemy);

        GravitySetter gr = enemy.GetComponent<GravitySetter>();
        riseMul = gr.RiseMultiplier;
        rb = gr.Rb;
        start = gr.StartPosition;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Fall", false);
        animator.SetBool("PosReached", false);

        Rise();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetBool("PosReached")) return;

        if (Vector2.Distance(rb.position, start) <= 0.05f) Stop(animator);
    }

    void Stop(Animator anim)
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;

        anim.SetBool("PosReached", true);
    }

    void Rise()
    {
        rb.gravityScale = riseMul;
    }
}
