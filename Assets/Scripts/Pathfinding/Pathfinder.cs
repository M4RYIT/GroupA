using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pathfinder : MonoBehaviour
{
    protected virtual Node AddNode(Vector2 pos) { return null; }

    protected virtual Node GetNode(Vector2 pos) { return null; }

    public virtual List<Vector2> GetPath(Vector2 start, Vector2 end) { return null; }

    public virtual List<Vector2> GetPath() { return null; }
}
