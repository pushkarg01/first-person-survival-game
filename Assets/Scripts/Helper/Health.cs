using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    private EnemyAnimations enemyAnim;
    private NavMeshAgent agent;
    private EnemyController enemyController;
    private EnemyAudio enemyAudio;

    private PlayerStats stats;

    public float health = 100f;
    public bool isPlayer, isBoar, isZombie;

    private bool isDead;

    void Awake()
    {
        if(isBoar || isZombie)
        {
            enemyAnim = GetComponent<EnemyAnimations>();
            enemyController = GetComponent<EnemyController>();
            agent = GetComponent<NavMeshAgent>();

            enemyAudio = GetComponentInChildren<EnemyAudio>();
        }

        if (isPlayer)
        {
            stats = GetComponent<PlayerStats>();
        }
    }

    public void ApplyDamage(float damage)
    {
        if (isDead) 
            return;

        health -= damage;

        if (isPlayer)
        {
            stats.DisplayHealthStats(health);
        }

        if (isBoar || isZombie)
        {
            if (enemyController.Enemy_State == EnemyState.PATROL)
            {
                enemyController.chaseDistance = 50f;
            }
        }

        if (health <= 0f)
        {
            PlayerDied();
            isDead = true;
        }
    }

    void PlayerDied()
    {
        // Zombie
        if (isZombie)
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().AddTorque(-transform.forward * 10f);

            enemyController.enabled = false;
            agent.enabled = false;
            enemyAnim.enabled = false;

            StartCoroutine(DeadSound());
        }

        // Boar
        if (isBoar)
        {
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
            enemyController.enabled=false;

            enemyAnim.Dead();
            StartCoroutine(DeadSound());
        }

        // Player
        if (isPlayer)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tag.ENEMY_TAG);

            for(int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }

            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);
        }

        if(tag == Tag.PLAYER_TAG)
        {
            Invoke("RestartGame", 3f);
        }
        else
        {
            Invoke("TurnOffGameObject", 3f);
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }

    IEnumerator DeadSound()
    {
        yield return new WaitForSeconds(0.3f);
        enemyAudio.PlayDeadSound();
    }
}
