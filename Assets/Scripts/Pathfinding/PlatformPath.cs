using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlatformPath : Pathfinder
{
    public CompositeCollider2D Composite;
    public int Index;

    List<Vector2> Positions = new List<Vector2>();

    private void Awake()
    {
        Composite.GetPath(Index, Positions);
    }

    public override List<Vector2> GetPath()
    {
        if (Positions==null||Positions.Count==0) Composite.GetPath(Index, Positions);

        List<Vector2> positions = new List<Vector2>();

        int index = Positions.IndexOf(Positions.OrderBy(pos => Vector2.Distance(pos, transform.position)).ToList()[0]);

        for (int i=0, j=index; i<Positions.Count; i++, j = (j+1)%Positions.Count)
        {
            positions.Add(Positions[j]);
        }

        return positions;
    }
}
