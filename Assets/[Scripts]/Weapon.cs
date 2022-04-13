using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public CapsuleCollider collider;
    public float force;
    public Transform player;
    public float damage;

    public void Attack(float newforce, float newdamage)
    {
        force = newforce;
        damage = newdamage;
        Debug.Log(force);
        Debug.Log(damage);
        collider.enabled = true;
    }

    public void StopAttacking()
    {
        collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Vector3 direction = other.gameObject.transform.position - player.position ;
            direction = direction.normalized * force;

            EnemyScript temp = other.GetComponent<EnemyScript>();
            temp.GetHit(direction, damage);
            //other.
            Debug.Log("Hit");
        }
    }
}
