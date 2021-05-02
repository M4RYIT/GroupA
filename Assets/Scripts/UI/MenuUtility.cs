using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenuUtility : MonoBehaviour
{
    public Action<float> OnLoadingScene;

    public void SetTimeScale()
    {
        Time.timeScale = 1f - Time.timeScale;
    }

    public void SetTimeScale(float value)
    {
        Time.timeScale = value;
    }

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void ChangeSceneAsync(int index)
    {
        StartCoroutine(LoadSceneAsync(i:index));
    }

    public void ChangeSceneAsync(string name)
    {
        StartCoroutine(LoadSceneAsync(nm: name));
    }

    IEnumerator LoadSceneAsync(string nm="", int i=0)
    {
        AsyncOperation op = string.IsNullOrEmpty(nm) ? SceneManager.LoadSceneAsync(i) : SceneManager.LoadSceneAsync(nm);

        while (!op.isDone)
        {
            OnLoadingScene?.Invoke(op.progress);
            yield return null;
        }

        OnLoadingScene = null;
    }
}
