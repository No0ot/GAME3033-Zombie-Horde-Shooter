using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : Entity
{
    Animator animator;
    public Transform player;
    NavMeshAgent agent;
    float rotationSpeed = 5.0f;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);

        if (Vector3.Distance(player.position, transform.position) < 1.5f)
        {
            agent.isStopped = true;
            Vector3 targetDirection = player.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

            animator.SetBool("IsAttacking", true);
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
            animator.SetBool("IsAttacking", false);
        }
    }
}

