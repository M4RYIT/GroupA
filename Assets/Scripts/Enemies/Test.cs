using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public CompositeCollider2D CompositeCollider;
    public int Index;

    List<Vector2> pos = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(CompositeCollider.pathCount);

        CompositeCollider.GetPath(Index, pos);

        transform.position = pos[0];
    }

    private void OnValidate()
    {
        CompositeCollider.GetPath(Index, pos);

        transform.position = pos[0];
    }
}
