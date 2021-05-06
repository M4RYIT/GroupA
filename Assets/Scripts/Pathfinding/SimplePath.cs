using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePath : Pathfinder
{
    public int N;
    public float Shift;

    List<Vector2> Positions;

    private void Awake()
    {
        Positions = new List<Vector2>();

        for (int i = 0; i < N; i++)
        {
            Positions.Add((Vector2)transform.position + new Vector2(Shift * i, 0f));
        }
    }

    public override List<Vector2> GetPath()
    {
        return Positions;
    }
}
