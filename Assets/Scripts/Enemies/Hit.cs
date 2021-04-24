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

        Other = Collider ? enemy.GetComponent<Hitter>().Hitted : enemy.GetComponent<Trigger>().Triggered;
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
