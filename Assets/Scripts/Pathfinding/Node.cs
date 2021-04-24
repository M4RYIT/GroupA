using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector2 Position;
    public List<Node> Neighbours;

    public Node(Vector2 pos)
    {
        Position = pos;
    }

    public void AddNeighbour(Node n)
    {
        if (n != null) Neighbours.Add(n);
    }
}
