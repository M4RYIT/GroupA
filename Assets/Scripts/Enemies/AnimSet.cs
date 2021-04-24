using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimParamMod
{
    Bool,
    Float,
    Int,
    Switch,
    IntSwitch,
    FloatSwitch,
    Trigger
}

public enum AnimEvent
{
    OnEnter,
    OnExit,
    OnRepeat
}

public class AnimSet : State
{
    AnimSetter animSetter;
    List<AnimParamSet> paramSets = new List<AnimParamSet>();
    float setTime;

    public override void Init(GameObject enemy)
    {
        base.Init(enemy);

        animSetter = enemy.GetComponent<AnimSetter>();
        paramSets = animSetter.animParamSets;

        setTime = animSetter.SetTime;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var ps in paramSets.FindAll(p => p.AnimEvent==AnimEvent.OnEnter))
        {
            SetParam(ps);
        }

        animSetter.StartCoroutine(SetParamCo());
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var ps in paramSets.FindAll(p => p.AnimEvent == AnimEvent.OnExit))
        {
            SetParam(ps);
        }
    }

    IEnumerator SetParamCo()
    {
        yield return new WaitForSeconds(setTime);

        foreach (var ps in paramSets.FindAll(p => p.AnimEvent == AnimEvent.OnRepeat))
        {
            SetParam(ps);
        }       
    }

    void SetParam(AnimParamSet pSet)
    {
        Animator anim = animSetter.Animator;

        switch (pSet.AnimParamMod)
        {
            case AnimParamMod.Bool:
                anim.SetBool(pSet.ParamName, pSet.BoolValue);
                break;

            case AnimParamMod.Float:
                anim.SetFloat(pSet.ParamName, pSet.FloatValue);
                break;

            case AnimParamMod.FloatSwitch:
                pSet.FloatValue = (pSet.FloatValue + 1) % 2;
                anim.SetFloat(pSet.ParamName, pSet.FloatValue);
                break;

            case AnimParamMod.Int:
                anim.SetInteger(pSet.ParamName, pSet.IntValue);
                break;

            case AnimParamMod.IntSwitch:
                pSet.IntValue = (pSet.IntValue + 1) % 2;
                anim.SetInteger(pSet.ParamName, pSet.IntValue);
                break;

            case AnimParamMod.Switch:
                pSet.BoolValue = !pSet.BoolValue;
                anim.SetBool(pSet.ParamName, pSet.BoolValue);
                break;

            case AnimParamMod.Trigger:
                anim.SetTrigger(pSet.ParamName); 
                break;
        }
    }
}
