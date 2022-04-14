using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    bool canSpawn = true;
    Vector3 spawnLocation;
    private void Awake()
    {
        spawnLocation = transform.position;
    }

    private void Update()
    {
        if(canSpawn && EnemyManager.instance.numActiveZombies < EnemyManager.instance.maxZombieCount)
        {
            StartCoroutine(SpawnZombie());
        }
    }
    IEnumerator SpawnZombie()
    {
        canSpawn = false;
        GameObject temp = EnemyManager.instance.GetEnemy();

        temp.SetActive(true);
        temp.transform.SetParent(transform);
        temp.transform.localPosition = Vector3.zero;
        temp.GetComponent<EnemyScript>().agent.enabled = true;

        yield return new WaitForSeconds(10f);
        canSpawn = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            canSpawn = false;
            StopAllCoroutines();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canSpawn = true;
            
        }
    }
}
