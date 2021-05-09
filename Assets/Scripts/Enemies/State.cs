using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : StateMachineBehaviour
{
    public virtual void Init(Enemy enemy) { }
}
