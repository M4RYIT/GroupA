using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : State
{
    Hitter enemy;

    public override void Init(GameObject enemy)
    {
        this.enemy = enemy.GetComponent<Hitter>();
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        HitCheck();
    }

    void HitCheck()
    {
        GameObject other = enemy.Hitted;

        if (other.CompareTag("Player"))
        {
            //Player's death event
        }
    }
}
