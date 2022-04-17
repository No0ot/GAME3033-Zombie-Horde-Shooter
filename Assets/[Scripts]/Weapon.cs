using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public CapsuleCollider collider;
    public float force;
    public Transform player;
    public float damage;

    public float durability;
    public float durabilityMax;
    public float durabilityDecrementAmount;

    SoundManager soundManager;

    private void Awake()
    {
        durability = durabilityMax;
        soundManager = GameObject.Find("InteractSoundManager").GetComponent<SoundManager>();
    }

    public void Attack(float newforce, float newdamage)
    {
        force = newforce;
        damage = newdamage;
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
            AudioSource.PlayClipAtPoint(soundManager.GetSound("SwordHit"), transform.position, 5.0f);
            EnemyScript temp = other.GetComponent<EnemyScript>();
            temp.GetHit(direction, damage);
            DamageWeapon();
        }
    }

    void DamageWeapon()
    {
        durability -= durabilityDecrementAmount;
        if(durability <= 0)
        {
            AudioSource.PlayClipAtPoint(soundManager.GetSound("WeaponBreak"), transform.position);
            player.GetComponent<WeaponHolder>().EquipWeapon(PickupType.NONE);
        }
    }
}
