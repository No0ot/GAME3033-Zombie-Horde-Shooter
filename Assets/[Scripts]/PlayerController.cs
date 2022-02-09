using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isAttacking;
    public bool isReloading;
    public bool isJumping;
    public bool isRunning;
    public bool isBlocking;

    public float energy;
    public float maxEnergy;

    public float health;
    public float maxHealth;

    private void Start()
    {
        maxEnergy = 100f;
        energy = maxEnergy;
        maxHealth = 100f;
        health = maxHealth;
    }

    private void Update()
    {
        if(energy < 100f)
        {
            energy += 2.0f * Time.deltaTime;
            if (energy > 100f)
                energy = maxEnergy;
        }
    }
}
