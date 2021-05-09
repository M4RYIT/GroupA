using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDeath : State
{
    Hitter h;
    Transform tr;
    SoundEvent s;

    public override void Init(Enemy enemy)
    {        
        tr = enemy.transform;
        s = enemy.Sound;
        if (enemy is Hitter) h = enemy as Hitter;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        HitCheck();
    }

    void HitCheck()
    {
        s.PlayOneShot("Hit");

        GameObject other = h.Hitted;

        if (other.CompareTag("Player"))
        {
            Vector3 v = (other.transform.position - tr.position).normalized;

            if (Mathf.Abs(v.x) > Mathf.Abs(v.y))
            {
                //Player's death event
                GameManager.Instance.OnPlayerDeath?.Invoke();
            }
            else
            {
                if (v.y<0)
                {
                    GameManager.Instance.OnPlayerDeath?.Invoke();
                }
                else
                {
                    h.Disable();
                }
            }
        }
    }
}
