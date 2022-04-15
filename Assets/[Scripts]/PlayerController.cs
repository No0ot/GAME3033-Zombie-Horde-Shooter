using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    public bool isAttacking;
    public bool isReloading;
    public bool isJumping;
    public bool isRunning;
    public bool isBlocking;

    public float energyRegenRate;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    private void Start()
    {
        maxEnergy = 100f;
        energy = maxEnergy;
        maxHealth = 100f;
        health = maxHealth;
    }

    private void Update()
    {
        if (!isAttacking)
        {
            if (energy < 100f)
            {
                energy = Mathf.Clamp(energy + energyRegenRate * Time.deltaTime, 0.0f, 100f);
            }
        }
    }
}
