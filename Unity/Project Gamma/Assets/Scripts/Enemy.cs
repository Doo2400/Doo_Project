using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Idle,
    Move,
    Attack,
    Hurt,
    Die,
}

public class Enemy : MonoBehaviour
{
    private GameManager gm;
    CharacterController cc;
    NavMeshAgent smith;
    Animator anim;

    public int hp;
    public int maxHp;
    public GameObject targetedHostile;
    public float distanceFromHostile;

    public float findDistance;
    public float chaseDistance;

    public float attackReach;
    public int attackDamage;
    public float attackDelay;
    private bool isAttack;

    private bool isDeath;

    [SerializeField] EnemyState enemyState;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        cc = GetComponent<CharacterController>();
        smith = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        enemyState = EnemyState.Idle;
    }

    private void Update()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                WhenIdle();
                break;
            case EnemyState.Move:
                WhenMove();
                break;
            case EnemyState.Attack:
                WhenAttack();
                break;
            case EnemyState.Hurt:
                WhenHurt();
                break;
        }

        if (hp <= 0)
        {
            if (isDeath == false)
            {
                anim.SetTrigger("ToDie");
                isDeath = true;
            }

            Death();
        }
    }

    void WhenIdle()
    {
        foreach (GameObject player in gm.players)
        {
            if (targetedHostile == null && Vector3.Distance(transform.position, player.transform.position) < findDistance)
            {
                targetedHostile = player;
                anim.SetTrigger("IdleToMove");
                enemyState = EnemyState.Move;
            }
        }
    }

    void WhenMove()
    {
        if (smith.isStopped == true)
        {
            smith.isStopped = false;
        }

        if (Vector3.Distance(transform.position, targetedHostile.transform.position) > attackReach)
        {
            smith.stoppingDistance = attackReach;
            smith.destination = targetedHostile.transform.position;
        }
        else
        {
            anim.SetTrigger("MoveToAttack");
            enemyState = EnemyState.Attack;
        }
    }

    void WhenAttack()
    {
        transform.rotation = Quaternion.LookRotation(
            new Vector3(targetedHostile.transform.position.x, transform.position.y, targetedHostile.transform.position.z)
            - transform.position);

        if (Vector3.Distance(transform.position, targetedHostile.transform.position) > attackReach)
        {
            StopAllCoroutines();
            isAttack = false;
            anim.SetTrigger("AttackToMove");
            enemyState = EnemyState.Move;
        }
        else
        {
            smith.isStopped = true;
            if (isAttack == false)
            {
                StartCoroutine(AttackDelay(attackDelay));
            }
        }
    }
    IEnumerator AttackDelay(float attackDelay)
    {
        isAttack = true;
        yield return new WaitForSeconds(attackDelay);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Player player = targetedHostile.GetComponent<Player>();
            player.GetDamageFromEnemy(attackDamage);
            print("Enemy Attacked Player");
        }

        isAttack = false;
    }

    void WhenHurt()
    {
        StopAllCoroutines();
        smith.isStopped = true;

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hurt") == false)
        {
            enemyState = EnemyState.Move;
        }
    }

    public void Hurt(int damageFromPlayer)
    {
        print("Enemy hit");
        if (hp > 0)
        {
            enemyState = EnemyState.Hurt;

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hurt") == false)
            {
                anim.SetTrigger("ToHurt");
            }

            hp -= damageFromPlayer;
        }
    }

    public void Death()
    {
        enemyState = EnemyState.Die;
        cc.enabled = false;
        smith.speed = 0;
    }
}
