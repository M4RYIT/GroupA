using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIColorSwitch : MonoBehaviour
{
    [SerializeField]
    Color FirstColor, SecondColor;

    Color[] colors;
    Image image;
    Button b;

    private void Awake()
    {
        colors = new Color[] { FirstColor, SecondColor };
        image = GetComponent<Image>();
    }

    public void SwitchColor(int i)
    {
        image.color = colors[i];
    }

    
}
