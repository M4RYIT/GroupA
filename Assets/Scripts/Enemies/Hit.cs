using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : State
{
    public bool Collider = true;

    GameObject Other;
    SoundEvent s;

    public override void Init(Enemy enemy)
    {
        s = enemy.Sound;

        if (Collider)
        {
            Hitter h = enemy as Hitter;
            if (h!=null) h.OnHit += () => { Other = h.Hitted; };
        }
        else
        {
            Trigger t = enemy as Trigger;
            if (t!=null) t.OnTrigger += () => { Other = t.Triggered; t.Entered = false; };
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
