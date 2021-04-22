using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : StateMachineBehaviour
{
    public abstract void Init(GameObject enemy);
}
