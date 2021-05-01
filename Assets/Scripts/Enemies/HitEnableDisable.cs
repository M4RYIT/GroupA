using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnableDisable : State
{
    public bool Collider = true;
    public float DisableAfter, EnableAfter;
    
    GameObject Other;
    Enemy Enemy;

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
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Other.CompareTag("Player")) Enemy.StartCoroutine(EnableDisable(animator));
    }

    public IEnumerator EnableDisable(Animator anim)
    {
        yield return new WaitForSeconds(DisableAfter);

        anim.gameObject.SetActive(false);

        yield return new WaitForSeconds(EnableAfter);

        anim.gameObject.SetActive(true);
    }
}
