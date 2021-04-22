using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitJump : State
{
    Enemy enemy;
    Transform tr;

    public override void Init(Enemy enemy)
    {
        this.enemy = enemy;
        tr = enemy.transform;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        HitCheck();
    }

    void HitCheck()
    {
        GameObject other = enemy.Other;

        if (other.CompareTag("Player"))
        {
            Vector3 v = (other.transform.position - tr.position).normalized;

            if (Mathf.Abs(v.x)>Mathf.Abs(v.y))
            {
                //Player's death event
            }
            else
            {
                //Call Jump on Player
            }
        }
    }
}
