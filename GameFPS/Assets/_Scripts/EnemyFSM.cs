using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFSM : MonoBehaviour
{
    [SerializeField]
    private float findDistance = 5;
    [SerializeField]
    private float returnDistance = 1;
    [SerializeField]
    private float attackDistance = 3;
    [SerializeField]
    private float moveSpeed = 5;
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private int maxHp = 3;
    [SerializeField]
    private Animator animator;

    private EnemyState state;
    private PlayerMove player;
    private float gravity = 0.9f;
    private Vector3 dir;
    private Vector3 origin;
    private IEnumerator attack;
    private IEnumerator damaged;
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
    }

    // Update is called once per frame
    void Update()
    {
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
        }

        dir.y -= gravity * Time.deltaTime;
        if(controller.enabled)
        {
            controller.Move(dir.normalized * moveSpeed * Time.deltaTime);
        }
    }

    private void Idle()
    {
        walked = false;
        animator.Play("Z_Idle");
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
        transform.forward = -player.transform.forward;

        if (!walked)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    animator.Play("Z_Walk1_InPlace");
                    break;
                case 1:
                    animator.Play("Z_Walk_InPlace");
                    break;
                case 2:
                    animator.Play("Z_Run_InPlace");
                    break;
            }
            walked = true;
        }

        if (distance < attackDistance)
        {
            state = EnemyState.Attack;
            walked = false;
        }
        if (distance > findDistance)
        {
            state = EnemyState.Idle;
            walked = false;
        }
    }

    private void Attack()
    {
        if ((player.transform.position - transform.position).magnitude > attackDistance && attack == null)
        {
            state = EnemyState.Move;
        }

        if (attack == null)
        {
            attack = _Attack();
            StartCoroutine(attack);
        }
    }
    private IEnumerator _Attack()
    {
        animator.Play("Z_Attack");
        yield return new WaitForSeconds(1f);

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
        origin.y = transform.position.y;
        transform.LookAt(origin);

        if (!walked)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    animator.Play("Z_Walk1_InPlace");
                    break;
                case 1:
                    animator.Play("Z_Walk_InPlace");
                    break;
                case 2:
                    animator.Play("Z_Run_InPlace");
                    break;
            }
            walked = true;
        }

        if ((player.transform.position - transform.position).magnitude < findDistance)
        {
            state = EnemyState.Move;
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
            damaged = _Damaged();
            StartCoroutine(damaged);
        }
    }
    private IEnumerator _Damaged()
    {
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
        switch (Random.Range(0, 2))
        {
            case 0:
                animator.Play("Z_FallingBack");
                break;
            case 1:
                animator.Play("Z_FallingForward");
                break;
        }
        enabled = false;
    }
}
