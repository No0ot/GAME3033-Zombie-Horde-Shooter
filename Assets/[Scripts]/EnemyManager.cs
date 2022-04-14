using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    public GameObject zombiePrefab;

    public List<GameObject> enemyList = new List<GameObject>();

    public int maxZombieCount;
    public int numActiveZombies;

    private void Awake()
    {
        instance = this;
    }
    public GameObject GetEnemy()
    {
        numActiveZombies++;
        foreach (GameObject enemy in enemyList)
        {
            if (!enemy.activeInHierarchy)
            {
                return enemy;
            }
            else
                return AddEnemy();
        }
        return AddEnemy();
    }

    GameObject AddEnemy()
    {
        GameObject temp = Instantiate(zombiePrefab);
        enemyList.Add(temp);
        temp.SetActive(false);
        return temp;
    }

}
