using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : State
{
    public bool Collider = true;

    GameObject Other;
    SoundEvent s;

    public override void Init(GameObject enemy)
    {
        base.Init(enemy);

        if (Collider)
        {
            Hitter h = enemy.GetComponent<Hitter>();
            h.OnHit += () => { Other = h.Hitted; };
            s = h.Sound;
        }
        else
        {
            Trigger t = enemy.GetComponent<Trigger>();
            t.OnTrigger += () => { Other = t.Triggered; };
            s = t.Sound;
        }
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        HitCheck();
    }

    void HitCheck()
    {
        s.PlayOneShot("Hit");

        if (Other.CompareTag("Player"))
        {
            //Player's death event
            GameManager.Instance.OnPlayerDeath?.Invoke();
        }
    }
}
