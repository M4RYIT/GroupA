using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Object drawn by player
//Once created lasts for tot time
public abstract class DrawnObject : MonoBehaviour
{
    public float LifeTime;
    public LineRenderer Line;
    public EdgeCollider2D Edge;

    protected List<Vector2> positions = new List<Vector2>();

    protected virtual void OnEnable()
    {
        GetComponent<Rigidbody2D>().simulated = true;

        Edge.GetPoints(positions);

        Destroy(gameObject, LifeTime);
    }

    //Initializes object properties
    public void Init(Color col, float life)
    {
        Line.startColor = Line.endColor = col;

        LifeTime = life;
    }
}
