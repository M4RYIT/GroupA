using UnityEngine;

//Object drawn by player
//Once created lasts for tot time
public abstract class DrawnObject : MonoBehaviour
{
    public float LifeTime;

    private void OnEnable()
    {
        Destroy(gameObject, LifeTime);
    }

    //Initializes object properties
    public void Init(Color col, float life)
    {
        LineRenderer lr = GetComponent<LineRenderer>();
        lr.startColor = lr.endColor = col;

        LifeTime = life;
    }
}
