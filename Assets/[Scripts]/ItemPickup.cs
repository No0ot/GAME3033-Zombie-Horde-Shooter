using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    CONSUMABLE,
    WEAPON_SWORDNBOARD,
    WEAPON_GREATSWORD
}

public class ItemPickup : MonoBehaviour
{
    public PickupType type;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            WeaponHolder temp = other.GetComponent<WeaponHolder>();
            switch (type)
            {
                case PickupType.CONSUMABLE:
                    break;
                case PickupType.WEAPON_SWORDNBOARD:
                    temp.EquipWeapon(type);
                    break;
                case PickupType.WEAPON_GREATSWORD:
                    temp.EquipWeapon(type);
                    break;
            }
            Destroy(gameObject);

        }

    }
}
