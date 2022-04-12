using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float energy;
    public float maxEnergy;

    public float health;
    public float maxHealth;

    public void GetHit(float damage)
    {
        TakeDamage(damage);
        //Knockback;
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Clamp(health - damage, 0f, maxHealth);
    }
}
