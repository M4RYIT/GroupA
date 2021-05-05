using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenuUtility : MonoBehaviour
{
    public Func<float, bool> OnLoadingScene;

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
        op.allowSceneActivation = false;
        while (!op.isDone)
        {
            if ((bool)(OnLoadingScene?.Invoke(op.progress / 0.9f))) op.allowSceneActivation = true;

            yield return null;
        }

        OnLoadingScene = null;
    }
}
