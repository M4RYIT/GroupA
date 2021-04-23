using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDeath : State
{
    public GameObject DeathPrefab;

    Hitter enemy;
    Transform tr;

    public override void Init(GameObject enemy)
    {
        this.enemy = enemy.GetComponent<Hitter>();
        tr = enemy.transform;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        HitCheck();
    }

    void HitCheck()
    {
        GameObject other = enemy.Hitted;

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
                //Instantiate(DeathPrefab, tr.position, Quaternion.identity);
                //Destroy(enemy.gameObject);
            }
        }
    }
}
