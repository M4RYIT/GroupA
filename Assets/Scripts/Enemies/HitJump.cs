using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitJump : State
{
    Hitter enemy;
    Transform tr;
    SoundEvent s;

    public override void Init(GameObject enemy)
    {
        base.Init(enemy);

        this.enemy = enemy.GetComponent<Hitter>();
        tr = enemy.transform;
        s = this.enemy.Sound;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        HitCheck();
    }

    void HitCheck()
    {
        s.PlayOneShot("Hit");

        GameObject other = enemy.Hitted;

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
                other.GetComponent<PlayerController>().Jump();
            }
        }
    }
}
