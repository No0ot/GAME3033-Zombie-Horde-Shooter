using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum AIState
{
    IDLE,
    PATROL,
    CHASE,
    ATTACK,
    DIE
}

public class EnemyAI : MonoBehaviour
{
    EnemyScript controller;
    NavMeshAgent agent;
    GameObject target;
    AIState state;

    bool isIdling;

    private void Awake()
    {
        controller = GetComponentInParent<EnemyScript>();
        agent = GetComponentInParent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
        RunBehaviour();
    }

    void RunBehaviour()
    {
        switch(state)
        {
            case AIState.IDLE:
                Idle();
                break;
            case AIState.PATROL:
                Patrol();
                break;
            case AIState.CHASE:
                Chase();
                break;
            case AIState.ATTACK:
                Attack();
                break;
            case AIState.DIE:
                Die();
                break;
        }
    }

    void Idle()
    {
        if(!isIdling)
            StartCoroutine(IdleWait());
    }

    void Patrol()
    {
        if (agent.enabled)
        {
            if (agent.remainingDistance < 0.5f || agent.destination == null)
            {
                agent.speed = 0.5f;
                agent.SetDestination(GetRandomNavMeshLocation(10f));
            }
        }
    }

    void Chase()
    {
        agent.speed = 3.5f;
        if (agent.enabled)
        {
            if (Vector3.Distance(target.transform.position, transform.position) < 1.5f)
            {
                state = AIState.ATTACK;
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(target.transform.position);
            }
        }
    }

    void Attack()
    {
        if (agent.enabled)
        {
            agent.isStopped = true;
            controller.RotateToTarget(target);
            if (controller.canAttack)
            {
                controller.SetAnimatorIsAttacking(true);
            }
            else
                controller.SetAnimatorIsAttacking(false);
        }
        if(Vector3.Distance(target.transform.position, transform.position) > 1.5f)
        {
            state = AIState.CHASE;
            controller.SetAnimatorIsAttacking(false);
        }
    }

    void Die()
    {

    }

    IEnumerator IdleWait()
    {
        if(agent.enabled)
            agent.SetDestination(transform.position);
        isIdling = true;
        yield return new WaitForSeconds(2.0f);
        state = AIState.PATROL;
        isIdling = false;

    }

    Vector3 GetRandomNavMeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (state == AIState.PATROL)
        //{
            if (other.CompareTag("Player"))
            {
                state = AIState.CHASE;
                target = other.gameObject;
                //agent.SetDestination(target.transform.position);
            }
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (state == AIState.CHASE || state == AIState.ATTACK)
        {
            if (other.CompareTag("Player"))
            {
                agent.speed = 0.5f;
                controller.SetAnimatorIsAttacking(false);
                state = AIState.IDLE;
                target = null;
            }
        }
    }
}
