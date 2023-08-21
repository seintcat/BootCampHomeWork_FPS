using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    [SerializeField]
    private float findDistance = 5;
    [SerializeField]
    private float attackDistance = 3;
    [SerializeField]
    private float moveSpeed = 5;
    [SerializeField]
    private CharacterController controller;

    private EnemyState state;
    private PlayerMove player;
    private float gravity = 0.9f;
    private Vector3 dir;
    private Vector3 origin;

    // Start is called before the first frame update
    void Start()
    {
        state = EnemyState.Idle;
        player = FindObjectOfType<PlayerMove>();
        origin = transform.position;
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
        controller.Move(dir.normalized * moveSpeed * Time.deltaTime);
    }

    private void Idle()
    {
        if(
            (player.transform.position - transform.position).magnitude < findDistance
            )
        {
            state = EnemyState.Move;
        }
    }
    private void Move()
    {
        dir = player.transform.position - transform.position;
        float distance = dir.magnitude;
        if (distance < attackDistance)
        {
            state = EnemyState.Move;
        }
        if (distance > findDistance)
        {
            state = EnemyState.Idle;
        }
    }
    private void Attack()
    {

    }
    private void Return()
    {

    }
    private void Damaged()
    {

    }
    private void Die()
    {

    }
}
