using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Enemy : MonoBehaviour
{
    public Animator Animator;

    protected Vector2 startPosition;
    protected Rigidbody2D rb;
    protected Transform tr;

    public Vector2 StartPosition => startPosition;
    public Rigidbody2D Rb => rb;
    public Transform Tr => tr;

    Material mat;

    protected virtual void Awake()
    {
        mat = GetComponentInChildren<Renderer>().material;
        tr = transform;
        rb = GetComponent<Rigidbody2D>();
        startPosition = rb.position;

        GameManager.Instance.OnPlayerDeath += () => gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        rb.simulated = true;
    }

    private void Start()
    {
        foreach (var smb in Animator.GetBehaviours<State>())
        {
            smb.Init(this.gameObject);
        }
    }

    public void Disable()
    {        
        StartCoroutine(CoDisable());        
    }

    IEnumerator CoDisable()
    {
        rb.simulated = false;
        float dissolve = mat.GetFloat("_Dissolve");

        while(dissolve<1.1f)
        {
            dissolve += Time.deltaTime;
            mat.SetFloat("_Dissolve", dissolve);
            yield return null;
        }

        mat.SetFloat("_Dissolve", 0f);
        gameObject.SetActive(false);
    }
}
