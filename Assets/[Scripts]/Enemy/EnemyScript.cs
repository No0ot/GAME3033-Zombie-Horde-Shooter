using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : Entity
{
    Animator animator;
    public Transform player;
    public NavMeshAgent agent;
    float rotationSpeed = 5.0f;
    Rigidbody rigidbody;
    bool canBeHit = true;
    bool isDead = false;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
        health = maxHealth;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }

        if(health <= 0)
        {
            isDead = true;
            animator.SetBool("IsAttacking", false);
            animator.SetBool("IsDead", true);
        }
    }

    public void GetHit(Vector3 forceDirection, float damage)
    {
        if (canBeHit)
        {
            canBeHit = false;
            agent.isStopped = true;
            agent.enabled = false;
            rigidbody.isKinematic = false;
            TakeDamage(damage);

            rigidbody.AddForce(forceDirection, ForceMode.Impulse);
            animator.SetTrigger("GetHit");
        }
    }

    public void RecoverFromBeingHit()
    {
        agent.enabled = true;
        agent.isStopped = false;
        rigidbody.isKinematic = true;
        canBeHit = true;
    }

    public void RotateToTarget(GameObject target)
    {
        Vector3 targetDirection = target.transform.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    public void SetAnimatorIsAttacking(bool tf)
    {
        animator.SetBool("IsAttacking", tf);
    }

    public void DeleteEntity()
    {
        EnemyManager.instance.numActiveZombies--;
        gameObject.SetActive(false);
    }
}

