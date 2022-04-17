using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public ItemPickup[] itemPrefabs;
    public ItemSpawningManager manager;
    public bool itemPlaced = false;


    public ItemPickup SpawnItem(PickupType type)
    {
        ItemPickup temp = null;
        switch(type)
        {
            case PickupType.NONE:
                return null;
            case PickupType.WEAPON_SWORDNBOARD:
                temp = Instantiate(itemPrefabs[0]);
                temp.transform.SetParent(this.transform);
                temp.transform.localPosition = new Vector3(0f, 1.2f, 0f);
                break;
            case PickupType.WEAPON_GREATSWORD:
                temp = Instantiate(itemPrefabs[1]);
                temp.transform.SetParent(this.transform);
                temp.transform.localPosition = new Vector3(0f, 3f, 0f);
                break;
        }
        itemPlaced = true;
        temp.manager = this;
        return temp;
    }
}
