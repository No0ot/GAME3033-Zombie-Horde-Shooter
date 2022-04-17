using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawningManager : MonoBehaviour
{
    [SerializeField]
    ItemSpawner[] itemSpawners;

    public List<ItemPickup> itemList;
    [SerializeField]
    int maxItems;

    private void Awake()
    {
        foreach(ItemSpawner spawner in itemSpawners)
        {
            spawner.manager = this;
        }
    }

    private void Update()
    {
        if(itemList.Count < maxItems)
        {
            SpawnItem();
        }
    }


    void SpawnItem()
    {
        List<ItemSpawner> possibleLocations = new List<ItemSpawner>();
        foreach(ItemSpawner spawner in itemSpawners)
        {
            if(!spawner.itemPlaced)
            {
                possibleLocations.Add(spawner);
            }
        }

        int rand = Random.Range(0, possibleLocations.Count - 1);
        int item = Random.Range(1, 3);
        possibleLocations[rand].itemPlaced = true;
        ItemPickup temp = possibleLocations[rand].SpawnItem((PickupType)item);
        itemList.Add(temp);
    }
}
