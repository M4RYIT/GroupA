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
}

public class AnimSetter : Enemy
{
    public float SetTime;
    public List<AnimParamSet> animParamSets;
}
