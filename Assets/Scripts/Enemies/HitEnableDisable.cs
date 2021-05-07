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
            t.OnTrigger += () => { Other = t.Triggered;  t.Animator.SetTrigger("Hit"); };
            Enemy = t;
        }

        s = Enemy.Sound;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        s.PlayOneShot("Hit");
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Other.CompareTag("Player")) Enemy.StartCoroutine(EnableDisable(animator));
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
