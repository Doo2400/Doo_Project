using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum DragonState
{
    Sleep,
    Idle,
    Move,
    Attack,
    Hurt,
    Die,
}

public class Dragon : MonoBehaviour
{
    private GameManager gm;
    CharacterController cc;
    NavMeshAgent smith;
    Animator anim;

    public int hp;
    public int maxHp;
    public float distanceFromHostile;
    public GameObject targetedHostile;

    public float findDistance;
    public float chaseDistance;

    public float attackReach;
    public int attackDamage;
    public float attackDelay;
    private bool isAttack;
    private bool isDeath;
    private bool isStageChange = true;
    

    [SerializeField] DragonState dragonState;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        cc = GetComponent<CharacterController>();
        smith = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        dragonState = DragonState.Sleep;
        anim.SetInteger("Stage", 0);
    }

    private void Update()
    {
        switch (dragonState)
        {
            case DragonState.Sleep:
                WhenSleep();
                break;
            case DragonState.Idle:
                WhenIdle();
                break;
            case DragonState.Move:
                WhenMove();
                break;
            case DragonState.Attack:
                WhenAttack();
                break;
            case DragonState.Hurt:
                WhenHurt();
                break;
        }

        if (hp <= maxHp / 2)
        {
            anim.SetInteger("Stage", 1);
        }

        else
        {
            anim.SetInteger("Stage", 0);
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

    void WhenSleep()
    {
        foreach (GameObject player in gm.players)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < chaseDistance)
            {
                anim.SetTrigger("WakeUp");
                dragonState = DragonState.Idle;
            }
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
                dragonState = DragonState.Move;
            }
        }
    }

    void WhenMove()
    {
        if (smith.isStopped == true)
        {
            smith.isStopped = false;
        }

        if (isStageChange == true && hp <= maxHp / 2)
        {
            anim.SetTrigger("StageChange");
            isStageChange = false;
            attackDelay *= 13.5f;
            return;
        }

        if (Vector3.Distance(transform.position, targetedHostile.transform.position) > attackReach)
        {
            smith.stoppingDistance = attackReach;
            smith.destination = targetedHostile.transform.position;
        }

        else
        {
            anim.SetTrigger("MoveToAttack");
            dragonState = DragonState.Attack;
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
            dragonState = DragonState.Move;
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
        }

        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Claw"))
        {
            Player player = targetedHostile.GetComponent<Player>();
            player.GetDamageFromEnemy(attackDamage);
        }

        isAttack = false;
    }

    void WhenHurt()
    {
        StopAllCoroutines();
        smith.isStopped = true;

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hurt") == false)
        {
            dragonState = DragonState.Move;
        }
    }

    public void Hurt(int damageFromPlayer)
    {
        if (hp > 0)
        {
            if (isStageChange == true && hp <= maxHp / 2)
            {
                anim.SetTrigger("StageChange");
                isStageChange = false;
                attackDelay *= 13.5f;
                return;
            }

            dragonState = DragonState.Hurt;

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hurt") == false)
            {
                anim.SetTrigger("ToHurt");
            }

            hp -= damageFromPlayer;
        }
    }

    void Death()
    {
        dragonState = DragonState.Die;
        cc.enabled = false;
        smith.speed = 0;
    }
}
