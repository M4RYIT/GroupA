using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePath : Pathfinder
{
    public int N;
    public Vector2 Shift;

    List<Vector2> Positions;
    Vector2 start;

    private void Awake()
    {
        start = transform.position;        
    }

    public override List<Vector2> GetPath()
    {
        if (start==Vector2.zero) start = transform.position;

        Positions = new List<Vector2>();

        for (int i = 0; i < N; i++)
        {
            Positions.Add(start + Shift * i);
        }

        return Positions;
    }
}
