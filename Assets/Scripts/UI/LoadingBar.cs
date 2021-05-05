using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public MenuUtility MenuUtility;
    public Image Image;
    public float Speed;

    float amount = 0;

    private void Awake()
    {
        MenuUtility.OnLoadingScene += SetLoadingBar;
    }

    private void Update()
    {
        Image.fillAmount = Mathf.Lerp(Image.fillAmount, amount, Speed * Time.deltaTime);
    }

    bool SetLoadingBar(float progress)
    {       
        amount = progress;
        return Image.fillAmount >= 0.99f;
    }
}
