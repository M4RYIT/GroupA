using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimParamMod
{
    Bool,
    Float,
    Int,
    Switch,
    Trigger
}

public class AnimSet : State
{
    AnimSetter animSetter;
    string paramName;
    AnimParamMod paramMod;
    bool boolValue;
    float floatValue, setTime;
    int intValue;

    public override void Init(GameObject enemy)
    {
        this.animSetter = enemy.GetComponent<AnimSetter>();

        paramName = animSetter.ParamName;
        paramMod = animSetter.AnimParamMod;

        boolValue = animSetter.BoolValue;
        floatValue = animSetter.FloatValue;
        intValue = animSetter.IntValue;

        setTime = animSetter.SetTime;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animSetter.StartCoroutine(SetParamCo(animator));
    }

    IEnumerator SetParamCo(Animator anim)
    {
        yield return new WaitForSeconds(setTime);

        SetParam(anim);
    }

    void SetParam(Animator anim)
    {
        switch (paramMod)
        {
            case AnimParamMod.Bool:
                anim.SetBool(paramName, boolValue);
                break;

            case AnimParamMod.Float:
                anim.SetFloat(paramName, floatValue);
                break;

            case AnimParamMod.Int:
                anim.SetInteger(paramName, intValue);
                break;

            case AnimParamMod.Switch:
                boolValue = !boolValue;
                anim.SetBool(paramName, boolValue);
                break;

            case AnimParamMod.Trigger:
                anim.SetTrigger(paramName); 
                break;
        }
    }
}
