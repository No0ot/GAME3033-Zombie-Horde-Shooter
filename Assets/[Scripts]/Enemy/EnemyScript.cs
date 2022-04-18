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
    public bool canAttack = true;
    bool isDead = false;

    public SoundManager soundManager;
    AudioSource source;

    bool soundCoroutineRunning = false;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
        soundManager = GameObject.Find("EnemySoundManager").GetComponent<SoundManager>();
        source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {

        canBeHit = true;
        isDead = false;
        health = maxHealth;
        canAttack = true;
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
        if(!soundCoroutineRunning)
        {
            StartCoroutine(PlayRandomPatrolSound());
        }
    }

    public void GetHit(Vector3 forceDirection, float damage)
    {
        if (canBeHit)
        {
            EnemyAttackScript temp = GetComponent<EnemyAttackScript>();
            temp.StopAttacking();
            animator.SetBool("IsAttacking", false);
            canAttack = false;
            canBeHit = false;
            agent.isStopped = true;
            agent.enabled = false;
            rigidbody.isKinematic = false;
            TakeDamage(damage);

            PlaySound("GetHit");
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
        canAttack = true;
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
        EnemyManager.instance.CheckWave();
        gameObject.SetActive(false);
    }

    public void PlaySound(string name)
    {

            source.clip = soundManager.GetSound(name);
            source.Play();
        
    }

    IEnumerator PlayRandomPatrolSound()
    {
        soundCoroutineRunning = true;
        float rand = Random.Range(5f, 20f);
        yield return new WaitForSeconds(rand);
        int randsound = Random.Range(0, 2);
        if (randsound > 0)
            PlaySound("PatrolSound1");
        else
            PlaySound("PatrolSound2");

        soundCoroutineRunning = false;
    }
}

