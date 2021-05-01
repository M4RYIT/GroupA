using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelMover : MonoBehaviour
{
    public Vector3 EndPos;
    public float Duration;

    Vector3 StartPos;
    Vector3 Dif;

    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position;
        Dif = EndPos - StartPos;
        StartCoroutine(EaseInOut());
    }

    IEnumerator EaseInOut()
    {
        float f = 0;

        while (f<Duration)
        {
            transform.position = EaseInOut(f, StartPos, Dif, Duration);
            f += Time.deltaTime;
            yield return null;
        }
    }

    Vector3 EaseInOut(float t, Vector3 b, Vector3 c, float d)
    {
        Vector3 pos;
        float s = 1.70158f;

        if ((t /= d / 2) < 1)
        {
            pos = c / 2 * (t * t * (((s *= (1.525f)) + 1) * t - s)) + b;
        }
        else
        {
            pos = c / 2 * ((t -= 2) * t * (((s *= (1.525f)) + 1) * t + s) + 2) + b;
        }        

        Vector3 diff = b - pos;
        float perc = Mathf.Abs(diff.x / c.x);
        Vector3 next = b + c * perc; 
        return next;
    }
}
