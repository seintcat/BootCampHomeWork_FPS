using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyFSM : MonoBehaviour
{
    [SerializeField]
    private float findDistance = 7;
    [SerializeField]
    private float returnDistance = 1;
    [SerializeField]
    private float attackDistance = 3;
    [SerializeField]
    private float moveSpeed = 5;
    [SerializeField]
    private float returnSpeed = 2;
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private int maxHp = 3;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private int walkActions;
    [SerializeField]
    private int dieActions;
    [SerializeField]
    private NavMeshAgent agent;

    private EnemyState state;
    private PlayerMove player;
    //private float gravity = 0.9f;
    private Vector3 dir;
    private Vector3 origin;
    private IEnumerator attack;
    private IEnumerator damaged;
    private IEnumerator delay;
    private IEnumerator dead;
    private int hpNow;
    private bool walked;

    public float hpBar
    {
        get
        {
            return (float)hpNow / maxHp;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        state = EnemyState.Idle;
        player = FindObjectOfType<PlayerMove>();
        origin = transform.position;
        hpNow = maxHp;
        walked = false;
        agent.stoppingDistance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManagerUI.gameStart)
        {
            return;
        }

        dir = Vector3.zero;
        switch (state)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damaged:
                Damaged();
                break;
            case EnemyState.Die:
                Die();
                break;
            case EnemyState.Delay:
                Delay();
                break;
        }

        if(controller.enabled)
        {
            //controller.Move(new Vector3(0, -gravity, 0) * Time.deltaTime * 3);
            //controller.Move(dir * Time.deltaTime * (state == EnemyState.Move ? moveSpeed : returnSpeed));
        }
    }

    private void Idle()
    {
        walked = false;
        animator.CrossFade("Idle", 0.1f);
        agent.isStopped = true;
        agent.ResetPath();
        if ( (player.transform.position - transform.position).magnitude < findDistance)
        {
            state = EnemyState.Move;
        }
        else if((origin - transform.position).magnitude > returnDistance)
        {
            state = EnemyState.Return;
        }
    }

    private void Move()
    {
        dir = player.transform.position - transform.position;
        float distance = dir.magnitude;

        if (!walked)
        {
            animator.CrossFade("Run", 0.1f);
            agent.speed = moveSpeed;
            walked = true;
        }
        agent.SetDestination(player.transform.position);

        if (distance < attackDistance)
        {
            state = EnemyState.Attack;
            walked = false;
        }
        if (distance > findDistance)
        {
            state = EnemyState.Delay;
            walked = false;
        }
    }

    private void Attack()
    {
        if ((player.transform.position - transform.position).magnitude > attackDistance && attack == null)
        {
            state = EnemyState.Move;
            agent.isStopped = true;
            agent.ResetPath();
        }

        if (attack == null)
        {
            attack = _Attack();
            StartCoroutine(attack);
        }
    }
    private IEnumerator _Attack()
    {
        if ((player.transform.position - transform.position).magnitude < attackDistance)
        {
            yield return new WaitForSeconds(0f);
            Vector3 look = player.transform.position;
            look.y = transform.position.y;
            transform.LookAt(look);
            animator.CrossFade("Attack", 0.1f);
            yield return new WaitForSeconds(0f);
            yield return new WaitForSeconds(2.5f);
        }

        StopCoroutine(attack);
        attack = null;
        yield return null;
    }
    public void AttackEvent()
    {
        if ((player.transform.position - transform.position).magnitude < attackDistance)
        {
            player.DamageFoo(damage);
        }
    }

    private void Return()
    {
        dir = origin - transform.position;
        float distance = dir.magnitude;
        //Vector3 look = origin;
        //look.y = transform.position.y;
        //dir = dir.normalized;
        //dir.y -= transform.position.y;
        //transform.LookAt(look);

        if (!walked)
        {
            switch (Random.Range(0, walkActions))
            {
                case 0:
                    animator.CrossFade("Walk1", 0.1f);
                    break;
                case 1:
                    animator.CrossFade("Walk2", 0.1f);
                    break;
            }
            walked = true;
            agent.speed = returnSpeed;
        }
        agent.destination = origin;

        if ((player.transform.position - transform.position).magnitude < findDistance)
        {
            state = EnemyState.Move;
            walked = false;
        }

        if (distance < returnDistance)
        {
            state = EnemyState.Idle;
            walked = false;
        }
    }

    public void Damage(int damage)
    {
        if (damaged == null)
        {
            hpNow -= damage;
            if (hpNow < 1)
            {
                state = EnemyState.Die;
            }
            else
            {
                state = EnemyState.Damaged;
            }
        }
    }
    private void Damaged()
    {
        if(damaged == null)
        {
            agent.isStopped = true;
            agent.ResetPath();
            damaged = _Damaged();
            StartCoroutine(damaged);
        }
    }
    private IEnumerator _Damaged()
    {
        animator.CrossFade("Damaged", 0.1f);
        yield return new WaitForSeconds(1f);

        StopCoroutine(damaged);
        damaged = null;
        state = EnemyState.Idle;
        yield return null;
    }

    private void Die()
    {
        walked = false;
        controller.enabled = false;
        agent.isStopped = true;
        agent.ResetPath();
        switch (Random.Range(0, dieActions))
        {
            case 0:
                animator.CrossFade("Die1", 0.1f);
                break;
            case 1:
                animator.CrossFade("Die2", 0.1f);
                break;
        }
        enabled = false;
        dead = Dead();
        StartCoroutine (dead);
    }
    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(10f);
        gameObject.SetActive(false);
    }

    private void Delay()
    {
        if (delay == null)
        {
            agent.isStopped = true;
            agent.ResetPath();
            delay = _Delay();
            StartCoroutine(delay);
        }
        if ((player.transform.position - transform.position).magnitude < findDistance)
        {
            if (delay != null)
            {
                StopCoroutine(delay);
                delay = null;
            }
            state = EnemyState.Move;
        }
    }
    private IEnumerator _Delay()
    {
        animator.CrossFade("Idle", 0.1f);
        yield return new WaitForSeconds(1f);
        StopCoroutine(delay);
        delay = null;
        state = EnemyState.Idle;
        yield return null;
    }
}
