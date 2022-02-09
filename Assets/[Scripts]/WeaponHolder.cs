using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
   public GameObject weaponToSpawn;
   public GameObject leftWeaponSpawn;

    PlayerController controller;
    Animator animator;

    [SerializeField]
    GameObject rightSocketLocation;
    [SerializeField]
    GameObject leftSocketLocation;
    // Start is called before the first frame update
    void Start()
    {
        GameObject spawnedWeapon = Instantiate(weaponToSpawn, rightSocketLocation.transform.position, rightSocketLocation.transform.rotation, rightSocketLocation.transform);
        GameObject leftspawnedWeapon = Instantiate(leftWeaponSpawn, leftSocketLocation.transform.position, leftSocketLocation.transform.rotation, leftSocketLocation.transform);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAnimatorIK(int layerIndex)
    {

        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftSocketLocation.transform.position);
    }
}
