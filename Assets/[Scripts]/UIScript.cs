using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Slider energyBar;
    public Slider healthBar;
    public PlayerController player;

    // Update is called once per frame
    void Update()
    {
        energyBar.value = player.energy / player.maxEnergy;
        healthBar.value = player.health / player.maxHealth;
    }
}
