using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    NONE,
    WEAPON_SWORDNBOARD,
    WEAPON_GREATSWORD
}

public class ItemPickup : MonoBehaviour
{
    public PickupType type;
    public ItemSpawner manager = null;

    SoundManager soundManager;

    private void Awake()
    {
        soundManager = GameObject.Find("InteractSoundManager").GetComponent<SoundManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            WeaponHolder temp = other.GetComponent<WeaponHolder>();
            switch (type)
            {
                case PickupType.NONE:
                    temp.EquipWeapon(type);
                    break;
                case PickupType.WEAPON_SWORDNBOARD:
                    temp.EquipWeapon(type);
                    break;
                case PickupType.WEAPON_GREATSWORD:
                    temp.EquipWeapon(type);
                    break;
            }
            AudioSource.PlayClipAtPoint(soundManager.GetSound("WeaponEquip"), transform.position);
            manager.manager.itemList.Remove(this);
            manager.itemPlaced = false;
            Destroy(gameObject);

        }

    }
}
