using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Bar representing the ability to draw object in scene
public class DrawBar : MonoBehaviour
{
    public TextMeshProUGUI BarAmountText;
    public GameObject BarForeground;

    Image barAmount;
    float barTotalAmount;

    //Sets bar amount image and text
    public void Set(float newAmount)
    {
        barAmount.fillAmount = newAmount / barTotalAmount;
        BarAmountText.text = newAmount.ToString("F1");
    }

    public void Select(bool enable = true)
    {
        barAmount.enabled = enable;
    }

    //Initializes bar properties
    public void Init(Color col, float totAmount, Vector3 scale)
    {
        transform.localScale = BarAmountText.transform.localScale = scale;

        barAmount = BarForeground.GetComponent<Image>();
        barAmount.color = col;

        barTotalAmount = totAmount;

        Set(totAmount);
    }
}
