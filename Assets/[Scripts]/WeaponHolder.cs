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

    public delegate void EquipNewWeapon(PickupType type);
    public EquipNewWeapon equipNewWeapon;

    // Start is called before the first frame update
    void Awake()
    {
        //GameObject spawnedWeapon = Instantiate(weaponToSpawn, rightSocketLocation.transform);
       // GameObject leftspawnedWeapon = Instantiate(leftWeaponSpawn, leftSocketLocation.transform.position, leftSocketLocation.transform.rotation, leftSocketLocation.transform);
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<MovementComponent>();
        controller = GetComponent<PlayerController>();
    }
    private void Start()
    {
        EquipWeapon(PickupType.NONE);
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
            case PickupType.NONE:
                animator.runtimeAnimatorController = movement.unarmed;
                if (rightHandEquip)
                {
                    Destroy(rightHandEquip.gameObject);
                    rightHandEquip = null;
                }
                if (leftHandEquip)
                {
                    Destroy(leftHandEquip.gameObject);
                    leftHandEquip = null;
                }
                
                break;
            case PickupType.WEAPON_SWORDNBOARD:
                spawnedWeapon = Instantiate(oneHandedSword, rightSocketLocation.transform);
                rightHandEquip = spawnedWeapon;
                leftspawnedWeapon = Instantiate(shield, leftSocketLocation.transform.position, leftSocketLocation.transform.rotation, leftSocketLocation.transform);
                leftHandEquip = leftspawnedWeapon;
                animator.runtimeAnimatorController = movement.swordnboard;
                rightHandEquip.GetComponent<Weapon>().player = transform;
                break;
            case PickupType.WEAPON_GREATSWORD:
                spawnedWeapon = Instantiate(twoHandedSword, rightSocketLocation.transform);
                rightHandEquip = spawnedWeapon;
                animator.runtimeAnimatorController = movement.twohanded;
                rightHandEquip.GetComponent<Weapon>().player = transform;
                break;
        }

        equipNewWeapon?.Invoke(type);
    }

    public void EquippedWeaponAttack(string forceDamage)
    {
        string[] tempstring = forceDamage.Split(';');
        if (rightHandEquip)
        {
            Weapon temp = rightHandEquip.GetComponent<Weapon>();
            temp.Attack(int.Parse(tempstring[0]), int.Parse(tempstring[1]));
        }
    }

    public void EquppedWeaponStopAttacking()
    {
        if (rightHandEquip)
        {
            Weapon temp = rightHandEquip.GetComponent<Weapon>();
            temp.StopAttacking();
        }
            controller.isAttacking = false;
    }
}
