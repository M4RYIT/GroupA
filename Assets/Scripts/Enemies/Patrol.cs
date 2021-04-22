using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{
    List<Vector2> points = new List<Vector2>();
    Vector2 dest;
    Rigidbody2D rb;
    Transform tr;
    int index = 0;
    float shift, speed;
    bool hit = false;

    public override void Init(GameObject enemy)
    {
        Patroller p = enemy.GetComponent<Patroller>();
        rb = p.Rb;
        tr = p.Tr;
        shift = p.Shift;
        speed = p.Speed;

        Hitter h = enemy.GetComponent<Hitter>();
        h.OnHit += () => { rb.velocity = Vector2.zero; hit = true; };

        points.Add(p.StartPosition);
        points.Add(points[0] + new Vector2(shift, 0f));
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hit = false;

        Next();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (hit) return;

        rb.MovePosition(rb.position + (dest - rb.position).normalized * speed * Time.fixedDeltaTime);

        if (Vector2.Distance(rb.position, dest) <= 0.05f) Next();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

    void Next()
    {
        index = (index + 1) % points.Count;

        dest = points[index];

        tr.localScale = new Vector3(Mathf.Sign((dest - rb.position).x), 1f, 1f);
    }
}
