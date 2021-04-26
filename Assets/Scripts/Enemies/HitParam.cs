using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HitParam : State
{
    public AnimParamCond ParamCond;
    public AnimParamSet ParamSet;

    GameObject other;

    public override void Init(GameObject enemy)
    {
        base.Init(enemy);

        other = enemy.GetComponent<Hitter>().Hitted;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        HitCheck(animator);
    }

    void HitCheck(Animator anim)
    {
        if (other.CompareTag("Player") && ParamCheck(anim))
        {
            //Player's death event
            GameManager.Instance.OnPlayerDeath?.Invoke();
        }
    }

    bool ParamCheck(Animator anim)
    {
        return (int)ParamCond == ParamSet.Compare(anim);
    }
}
