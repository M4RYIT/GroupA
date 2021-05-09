using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnableDisable : State
{
    public bool Collider = true;
    public float DisableAfter, EnableAfter;
    
    GameObject Other;
    Enemy Enemy;
    SoundEvent s;
    Transform tr;

    public override void Init(GameObject enemy)
    {
        base.Init(enemy);

        if (Collider)
        {
            Hitter h = enemy.GetComponent<Hitter>();
            h.OnHit += () => { Other = h.Hitted; };
            Enemy = h;
        }
        else
        {
            Trigger t = enemy.GetComponent<Trigger>();
            t.OnTrigger += () => { Other = t.Triggered;  t.Animator.SetTrigger("Hit"); t.Entered = false; };
            Enemy = t;
        }

        s = Enemy.Sound;
        tr = Enemy.Tr;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        s.PlayOneShot("Hit");
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        HitCheck(animator);
    }

    void HitCheck(Animator anim)
    {
        if (Other.CompareTag("Player"))
        {
            Vector3 v = (Other.transform.position - tr.position).normalized;

            Debug.Log(v);

            if (Mathf.Abs(v.y) >= Mathf.Abs(v.x)-0.25f && v.y>0)
            {
                Debug.Log("Hit");

                Enemy.StartCoroutine(EnableDisable(anim));
            }
        }
    }

    public IEnumerator EnableDisable(Animator anim)
    {
        yield return new WaitForSeconds(DisableAfter);

        s.Stop();
        s.PlayOneShot("Disappear");
        anim.gameObject.SetActive(false);
        Enemy.Rb.simulated = false;
        Enemy.enabled = false;

        yield return new WaitForSeconds(EnableAfter);

        s.PlayOneShot("Appear");
        anim.gameObject.SetActive(true);
        Enemy.Rb.simulated = true;
        Enemy.enabled = true;
        s.Play();
    }
}
