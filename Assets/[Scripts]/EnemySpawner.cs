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
        if(canSpawn && EnemyManager.instance.enemyList.Count < EnemyManager.instance.maxZombieCount)
        {
            StartCoroutine(SpawnZombie());
        }
    }
    IEnumerator SpawnZombie()
    {
        canSpawn = false;
        yield return new WaitForSeconds(10f);
        GameObject temp = EnemyManager.instance.GetEnemy();

        temp.transform.SetParent(transform);
        temp.transform.localPosition = Vector3.zero;

        canSpawn = true;
    }
}
