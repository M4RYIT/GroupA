using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DeathFx : MonoBehaviour
{
    public Volume Volume;
    public float TimeScale, Duration;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnPlayerDeath += Fx;
    }

    void Fx()
    {
        StartCoroutine(CoFx());
    }

    IEnumerator CoFx()
    {
        Volume.enabled = true;
        Time.timeScale = TimeScale;

        yield return new WaitForSeconds(Duration);

        while (Time.timeScale<1.0f)
        {
            Time.timeScale += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = 1.0f;
        Volume.enabled = false;
    }
}
