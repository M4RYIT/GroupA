using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public MenuUtility MenuUtility;
    public Image Image;

    private void Awake()
    {
        MenuUtility.OnLoadingScene += SetLoadingBar;
    }

    void SetLoadingBar(float progress)
    {
        Image.fillAmount = progress;
    }
}
