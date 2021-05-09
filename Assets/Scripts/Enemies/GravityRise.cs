using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityRise : State
{
    Rigidbody2D rb;
    float riseMul;
    Vector2 start;

    public override void Init(Enemy enemy)
    {
        rb = enemy.Rb;
        start = enemy.StartPosition;

        GravitySetter gr = enemy as GravitySetter;
        if (gr != null) riseMul = gr.RiseMultiplier;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Rise();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetBool("PosReached")) return;

        if (Vector2.Distance(rb.position, start) <= 0.05f || (start-rb.position).y<0) Stop(animator);
    }

    void Stop(Animator anim)
    {
        anim.SetBool("PosReached", true);
    }

    void Rise()
    {
        rb.gravityScale = riseMul;
    }
}
