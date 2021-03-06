using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PanelMover : MonoBehaviour
{
    public Vector3 EndPos;
    public float Duration;
    public UnityEvent OnEndMove;

    Vector3 startPos, endPos;
    Vector3 dif;
    bool reached = false;
    bool running = false;

    private void Awake()
    {
        startPos = transform.position;
        endPos = new Vector3(EndPos.x * Screen.width, EndPos.y * Screen.height, 0f);
    }

    public void Move()
    {       
        if (!running) MoveInOut();
    }

    void MoveInOut()
    {
        dif = (!reached) ? endPos - startPos : startPos - endPos;

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

        if (!reached) OnEndMove?.Invoke();
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
        float perc = Mathf.Abs((diff.x+diff.y) / (c.x+c.y));
        Vector3 next = b + c * perc; 
        return next;
    }
}
