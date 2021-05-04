using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HitParam : State
{
    public bool Collider;
    public AnimParamCond ParamCond;
    public AnimParamSet ParamSet;

    GameObject other;

    public override void Init(GameObject enemy)
    {
        base.Init(enemy);

        if (Collider)
        {
            Hitter h = enemy.GetComponent<Hitter>();
            h.OnHit += () => { other = h.Hitted; };
        }
        else
        {
            Trigger t = enemy.GetComponent<Trigger>();
            t.OnTrigger += () => { other = t.Triggered; };
        }
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        HitCheck(animator);
    }

    void HitCheck(Animator anim)
    {
        if (other != null && other.CompareTag("Player") && ParamCheck(anim))
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
