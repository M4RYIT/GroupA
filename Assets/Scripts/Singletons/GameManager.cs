using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Action OnPlayerDeath;
    public Action<int, float> OnCollect;

    private void Start()
    {
        SceneManager.activeSceneChanged += (Scene current, Scene next) => { GetComponent<AudioSource>().volume = Mathf.Lerp(0.6f, 0.2f, next.buildIndex); OnChangeScene(); };
    }

    public void OnChangeScene()
    {
        OnPlayerDeath = null;
        OnCollect = null;
    }

    private void OnDestroy()
    {
        OnChangeScene();
    }
}
