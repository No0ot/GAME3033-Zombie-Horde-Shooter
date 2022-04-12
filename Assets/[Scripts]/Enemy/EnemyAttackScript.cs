using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    public SphereCollider handCollider;

    public void Attack()
    {
        handCollider.enabled = true;
    }

    public void StopAttacking()
    {
        handCollider.enabled = false;
    }
}
