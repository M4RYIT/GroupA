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
    protected SoundEvent sound;
    protected Action onPlayerDeath;

    public Vector2 StartPosition => startPosition;
    public Rigidbody2D Rb => rb;
    public Transform Tr => tr;
    public SoundEvent Sound => sound;
    public Action OnPlayerDeath { get => onPlayerDeath; set => onPlayerDeath = value; }

    Material mat;

    protected virtual void Awake()
    {
        mat = GetComponentInChildren<Renderer>().material;
        tr = transform;
        rb = GetComponent<Rigidbody2D>();
        sound = GetComponent<SoundEvent>();
        startPosition = rb.position;       
    }

    private void OnEnable()
    {
        rb.simulated = true;

        foreach (var smb in Animator.GetBehaviours<State>())
        {
            smb.Init(this);
        }
    }

    private void Start()
    {
        GameManager.Instance.OnPlayerDeath += () => { gameObject.SetActive(true); onPlayerDeath?.Invoke(); };
    }

    public void Disable()
    {        
        StartCoroutine(CoDisable());        
    }

    IEnumerator CoDisable()
    {
        sound.PlayOneShot("Death");
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
