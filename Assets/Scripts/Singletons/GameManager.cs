using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    public Action OnPlayerDeath;
    public Action<int, float> OnCollect;
}
