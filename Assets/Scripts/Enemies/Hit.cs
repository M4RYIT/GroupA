using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : State
{
    public bool Collider = true;

    GameObject Other;

    public override void Init(GameObject enemy)
    {
        base.Init(enemy);

        if (Collider)
        {
            Hitter h = enemy.GetComponent<Hitter>();
            h.OnHit += () => { Other = h.Hitted; };
        }
        else
        {
            Trigger t = enemy.GetComponent<Trigger>();
            t.OnTrigger += () => { Other = t.Triggered; };
        }
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        HitCheck();
    }

    void HitCheck()
    {
        if (Other.CompareTag("Player"))
        {
            //Player's death event
            GameManager.Instance.OnPlayerDeath?.Invoke();
        }
    }
}
