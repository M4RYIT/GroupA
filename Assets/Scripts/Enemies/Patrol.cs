using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{
    List<Vector3> points = new List<Vector3>();
    Vector3 dest;
    Rigidbody2D rb;
    Transform tr;
    int index = 0;
    float shift, speed;

    public override void Init(Enemy enemy)
    {
        Patroller p = enemy as Patroller;
        rb = p.Rb;
        tr = p.Tr;
        shift = p.Shift;
        speed = p.Speed;
        p.OnHit += () => rb.velocity = Vector2.zero;

        points.Add(enemy.StartPosition);
        points.Add(points[0] + new Vector3(shift, 0f, 0f));
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Next();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //rb.MovePosition(rb.position + (dest - rb.position).normalized * speed * Time.fixedDeltaTime);

        tr.Translate((dest - tr.position).normalized * Time.fixedDeltaTime * speed);

        if (Vector2.Distance(tr.position, dest) <= 0.05f) Next();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that processes and affects root motion


    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

    void Next()
    {
        index = (index + 1) % points.Count;

        dest = points[index];

        tr.localScale = new Vector3(Mathf.Sign((dest - tr.position).x), 1f, 1f);
    }
}
