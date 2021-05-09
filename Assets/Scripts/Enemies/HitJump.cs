using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitJump : State
{
    public float JumpMultiplier;

    Hitter h;
    Transform tr;
    SoundEvent s;

    public override void Init(Enemy enemy)
    {
        if (enemy is Hitter) h = enemy as Hitter;
        tr = enemy.transform;
        s = enemy.Sound;
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

            if (Mathf.Abs(v.x)>Mathf.Abs(v.y))
            {
                //Player's death event
                GameManager.Instance.OnPlayerDeath?.Invoke();
            }
            else
            {
                //Call Jump on Player
                other.GetComponent<PlayerController>().Jump(JumpMultiplier);
            }
        }
    }
}
