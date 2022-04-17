using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{
    public Slider energyBar;
    public Slider healthBar;
    public PlayerController player;
    public WeaponHolder weaponHolder;

    public Slider weaponDurability;
    public Image weaponPicture;
    public TMP_Text weapontext;
    public Weapon equippedWeapon;

    public Sprite unarmedSprite;
    public Sprite swordSprite;
    public Sprite claymoreSprite;

    public TMP_Text waveNum;
    public TMP_Text zombieNum;

    private void Awake()
    {
        weaponHolder = player.GetComponent<WeaponHolder>();
        weaponHolder.equipNewWeapon += UpdateWeaponUI;
        weaponPicture.sprite = unarmedSprite;
        weapontext.text = "Unarmed";
    }
    // Update is called once per frame
    void Update()
    {
        energyBar.value = player.energy / player.maxEnergy;
        healthBar.value = player.health / player.maxHealth;
        if (equippedWeapon)
            weaponDurability.value = equippedWeapon.durability / equippedWeapon.durabilityMax;

        waveNum.text = "Wave:" + EnemyManager.instance.waveNum;
        zombieNum.text = "Zombies:" + EnemyManager.instance.numActiveZombies;
    }

    void UpdateWeaponUI(PickupType type)
    {
        switch(type)
        {
            case PickupType.NONE:
                weaponPicture.sprite = unarmedSprite;
                weapontext.text = "Unarmed";
                equippedWeapon = null;
                weaponDurability.gameObject.SetActive(false);
                break;
            case PickupType.WEAPON_SWORDNBOARD:
                weaponPicture.sprite = swordSprite;
                weapontext.text = "Sword 'n Board";
                equippedWeapon = weaponHolder.rightHandEquip.GetComponent<Weapon>();
                weaponDurability.gameObject.SetActive(true);
                break;
            case PickupType.WEAPON_GREATSWORD:
                weaponPicture.sprite = claymoreSprite;
                weapontext.text = "Claymore";
                equippedWeapon = weaponHolder.rightHandEquip.GetComponent<Weapon>();
                weaponDurability.gameObject.SetActive(true);
                break;
        }
    }
}
