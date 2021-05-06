using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : Enemy
{
    public Pathfinder Pathfinder;   
    public MoveData MoveData;

    public List<Vector2> Positions => Pathfinder.GetPath();
    public int Index { get; set; }
}

[System.Serializable]
public class MoveData
{
    public float Speed, Amplitude, Frequency;
    public MoveType MoveType;
}
