using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{

    [SerializeField]
    private GameObject objPrefab;
    [SerializeField]
    private Queue<GameObject> objPrefabPool = new Queue<GameObject>();
    [SerializeField]
    private int desiredPoolSize = 5;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < desiredPoolSize; i++)
        {
            GameObject prefab = Instantiate(objPrefab);
            objPrefabPool.Enqueue(prefab);
            prefab.SetActive(false);
        }
    }

    public void SpawnObj()
    {
        GetObj(gameObject.transform.position);
    }

    public GameObject GetObj()
    {
        if (objPrefabPool.Peek().activeSelf) return null;

        GameObject obj = objPrefabPool.Dequeue();
        obj.SetActive(true);

        objPrefabPool.Enqueue(obj);
        return obj;
    }

    public GameObject GetObj(Vector2 pos)
    {
        if (objPrefabPool.Peek().activeSelf) return null;

        GameObject obj = objPrefabPool.Dequeue();
        obj.transform.position = pos;
        obj.SetActive(true);

        objPrefabPool.Enqueue(obj);
        return obj;
    }
}
