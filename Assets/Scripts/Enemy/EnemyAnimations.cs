using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{

    private Animator enemyAnim;

    private void Awake()
    {
        enemyAnim = GetComponent<Animator>();
    }

    public void Walk(bool walk)
    {
        enemyAnim.SetBool(AnimationTags.WALK_PARAMETER, walk);
    }

    public void Run(bool run)
    {
        enemyAnim.SetBool(AnimationTags.RUN_PARAMETER, run);
    }

    public void Attack()
    {
        enemyAnim.SetTrigger(AnimationTags.ATTACK_TRIGGER);
    }
    public void Dead()
    {
        enemyAnim.SetTrigger(AnimationTags.DEAD_TRIGGER);
    }

}

