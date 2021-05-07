using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public PoolManager PoolManager;
    public Transform BulletSpawn;

    SoundEvent sound;

    private void Awake()
    {
        sound = GetComponent<SoundEvent>();
    }

    public void Shoot()
    {
        //bullet from poolmgr
        GameObject bullet = PoolManager.GetObj(BulletSpawn.position);
        sound.PlayOneShot("Shoot");

        if (bullet!=null)
        {
            bullet.GetComponent<BulletMove>().Dir = new Vector2(-gameObject.transform.localScale.x, 0f);
        }
    }
}
