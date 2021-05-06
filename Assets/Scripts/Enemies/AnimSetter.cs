using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimParamSet
{
    public AnimEvent AnimEvent;
    public AnimParamMod AnimParamMod;
    public string ParamName;
    public float FloatValue;
    public int IntValue;
    public bool BoolValue;

    public int Compare(Animator anim)
    {
        return AnimParamMod switch
        {
            AnimParamMod.Bool => anim.GetBool(ParamName).CompareTo(BoolValue),
            AnimParamMod.Float => anim.GetFloat(ParamName).CompareTo(FloatValue),
            AnimParamMod.Int => anim.GetInteger(ParamName).CompareTo(IntValue),
            _ => -2
        };
    }
}

public class AnimSetter : Enemy
{
    public float SetTime;
}
