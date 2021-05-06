using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelMover : MonoBehaviour
{
    public Vector3 EndPos;
    public float Duration;

    Vector3 startPos;
    Vector3 dif;
    bool reached = false;
    bool running = false;

    private void Awake()
    {
        startPos = transform.position;
    }

    public void Move()
    {
        if (!running) MoveInOut();
    }

    void MoveInOut()
    {
        dif = (!reached) ? EndPos - startPos : startPos - EndPos;

        StartCoroutine(EaseInOut());

        reached = !reached;
    }

    IEnumerator EaseInOut()
    {
        running = true;
        Vector3 start = transform.position;
        
        float f = 0;
        while (f<Duration)
        {
            transform.position = EaseInOut(f, start, dif, Duration);
            f += Time.unscaledDeltaTime;
            yield return null;
        }

        running = false;
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
