using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Generic Singleton abstract class
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T instance;

    public static T Instance => instance;

    private void Awake()
    {
        if (instance!=null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this as T;
        DontDestroyOnLoad(this);
    }
}
