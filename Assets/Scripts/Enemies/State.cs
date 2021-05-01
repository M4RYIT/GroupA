using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : StateMachineBehaviour
{
    protected bool init = false;

    public virtual void Init(GameObject enemy)
    {
        if (init) return;

        init = true;
    }
}
