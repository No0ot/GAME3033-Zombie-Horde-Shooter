using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    public GameObject zombiePrefab;

    public List<GameObject> enemyList = new List<GameObject>();

    public int maxZombieCount;

    private void Awake()
    {
        instance = this;
    }
    public GameObject GetEnemy()
    {
        GameObject temp = Instantiate(zombiePrefab);
        enemyList.Add(temp);
        return temp;
    }

}
