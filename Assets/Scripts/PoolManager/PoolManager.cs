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

    public GameObject GetObj()
    {
        if(objPrefabPool.Count > 0)
        {
            GameObject obj = objPrefabPool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            return null; 
        }
    }

    public void ReturnObj(GameObject objToEnqueue)
    {
        objPrefabPool.Enqueue(objToEnqueue);
        objToEnqueue.SetActive(false);
    }
}
