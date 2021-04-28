using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public PoolManager PoolManager;
    public DrawnObjectType DrawnObjectType;
    public float Amount;

    public void Collect()
    {
        GameManager.Instance.OnCollect?.Invoke((int)DrawnObjectType, Amount);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        PoolManager.GetObj(transform.position);
    }
}
