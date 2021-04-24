using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : Enemy
{
    public Pathfinder Pathfinder;    
    public float Speed;

    public List<Vector2> Positions => Pathfinder.GetPath();
}
