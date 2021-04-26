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
    OnRepeat,
    None
}

public enum AnimParamCond
{ 
    Less=-1,
    Equal=0,
    Greater=1
}

public class AnimSet : State
{
    public List<AnimParamSet> paramSets;

    AnimSetter animSetter;
    
    float setTime;
    bool coRunning = false;
    bool flip = true;

    public override void Init(GameObject enemy)
    {
        base.Init(enemy);

        animSetter = enemy.GetComponent<AnimSetter>();
        setTime = animSetter.SetTime;        
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var ps in paramSets.FindAll(p => p.AnimEvent==AnimEvent.OnEnter))
        {
            SetParam(ps);
        }

        if(!coRunning) animSetter.StartCoroutine(SetParamCo());
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
        coRunning = true;

        yield return new WaitForSeconds(setTime);

        foreach (var ps in paramSets.FindAll(p => p.AnimEvent == AnimEvent.OnRepeat))
        {
            SetParam(ps);
        }

        coRunning = false;
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
                anim.SetFloat(pSet.ParamName, (anim.GetFloat(pSet.ParamName)+1)%2);
                break;

            case AnimParamMod.Int:
                anim.SetInteger(pSet.ParamName, pSet.IntValue);
                break;

            case AnimParamMod.IntSwitch:
                anim.SetInteger(pSet.ParamName, (anim.GetInteger(pSet.ParamName)+1)%2);
                break;

            case AnimParamMod.Switch:
                anim.SetBool(pSet.ParamName, !anim.GetBool(pSet.ParamName));
                break;

            case AnimParamMod.Trigger:
                anim.SetTrigger(pSet.ParamName); 
                break;
        }
    }
}
