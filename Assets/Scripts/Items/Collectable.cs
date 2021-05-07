using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public PoolManager PoolManager;
    public DrawnObjectType DrawnObjectType;
    public float Amount;

    private void Awake()
    {
        GameManager.Instance.OnPlayerDeath += () => Activate(true);        
    }

    public void Collect()
    {
        GameManager.Instance.OnCollect?.Invoke((int)DrawnObjectType, Amount);        
        Activate(false);
        PoolManager.GetObj(transform.position);
    }

    void Activate(bool active)
    {
        gameObject.SetActive(active);
    }
}
