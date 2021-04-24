using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public PoolManager PoolManager;
    public Transform BulletSpawn;

    public void Shoot()
    {
        //bullet from poolmgr
        GameObject bullet = PoolManager.GetObj();

        if (bullet!=null)
        {
            bullet.transform.position = BulletSpawn.position;
            bullet.GetComponent<BulletMove>().Dir = new Vector2(-gameObject.transform.localScale.x, 0f);
        }
    }
}
