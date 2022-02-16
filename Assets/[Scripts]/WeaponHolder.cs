using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
   public GameObject oneHandedSword;
   public GameObject shield;
   public GameObject twoHandedSword;

    PlayerController controller;
    MovementComponent movement;
    Animator animator;

    [SerializeField]
    GameObject rightSocketLocation;
    [SerializeField]
    GameObject leftSocketLocation;

    public GameObject rightHandEquip;
    public GameObject leftHandEquip;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject spawnedWeapon = Instantiate(weaponToSpawn, rightSocketLocation.transform);
       // GameObject leftspawnedWeapon = Instantiate(leftWeaponSpawn, leftSocketLocation.transform.position, leftSocketLocation.transform.rotation, leftSocketLocation.transform);
        animator = GetComponent<Animator>();
        movement = GetComponent<MovementComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAnimatorIK(int layerIndex)
    {

        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftSocketLocation.transform.position);
    }

    public void EquipWeapon(PickupType type)
    {
        GameObject spawnedWeapon;
        GameObject leftspawnedWeapon;

        if(rightHandEquip)
        {
            Destroy(rightHandEquip.gameObject);

        }

        if(leftHandEquip)
        {
            Destroy(leftHandEquip.gameObject);

        }

        switch (type)
        {
            case PickupType.WEAPON_SWORDNBOARD:
                spawnedWeapon = Instantiate(oneHandedSword, rightSocketLocation.transform);
                rightHandEquip = spawnedWeapon;
                leftspawnedWeapon = Instantiate(shield, leftSocketLocation.transform.position, leftSocketLocation.transform.rotation, leftSocketLocation.transform);
                leftHandEquip = leftspawnedWeapon;
                animator.runtimeAnimatorController = movement.swordnboard;

                break;
            case PickupType.WEAPON_GREATSWORD:
                spawnedWeapon = Instantiate(twoHandedSword, rightSocketLocation.transform);
                rightHandEquip = spawnedWeapon;
                animator.runtimeAnimatorController = movement.twohanded;
                break;
        }
    }
}
