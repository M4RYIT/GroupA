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
    SoundEvent s;

    public override void Init(Enemy enemy)
    {
        if (Collider)
        {
            Hitter h = enemy as Hitter;
            if (h!=null) h.OnHit += () => { other = h.Hitted; };
        }
        else
        {
            Trigger t = enemy as Trigger;
            if (t!=null) t.OnTrigger += () => { other = t.Triggered; t.Entered = false; };
        }

        s = enemy.Sound;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        HitCheck(animator);
    }

    void HitCheck(Animator anim)
    {
        s.PlayOneShot("Hit");

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
