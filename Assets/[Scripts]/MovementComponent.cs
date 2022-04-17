using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementComponent : MonoBehaviour
{
    [SerializeField]
    float walkSpeed = 5;
    [SerializeField]
    float runSpeed = 10;
    [SerializeField]
    float jumpForce = 5;

    //componenets
    private PlayerController controller;
    Rigidbody rigidbody;
    Animator animator;
    public RuntimeAnimatorController unarmed;
    public RuntimeAnimatorController twohanded;
    public RuntimeAnimatorController swordnboard;
    bool shield = true;
    bool canGetHit = true;
    

    public bool applyMeshMotion;

    //references
    Vector2 inputVector = Vector2.zero;
    Vector3 moveDirection = Vector3.zero;
    Vector2 lookInput = Vector2.zero;
    public GameObject followTarget;
    public GameObject interactObject;
    WeaponHolder weapon;
    public SoundManager soundManager;
    AudioSource audioSource;


    // animator hashes
    public readonly int movementXHash = Animator.StringToHash("MoveX");
    public readonly int movementYHash = Animator.StringToHash("MoveY");
    public readonly int isRunningHash = Animator.StringToHash("IsRunning");
    public readonly int isJumpingHash = Animator.StringToHash("IsJumping");
    public readonly int isAttackingHash = Animator.StringToHash("IsAttacking");
    public readonly int isBlockingHash = Animator.StringToHash("IsBlocking");



    private void Awake()
    {
        rigidbody = GetComponentInChildren<Rigidbody>();
        controller = GetComponent<PlayerController>();
        animator = GetComponentInChildren<Animator>();
        //animator.runtimeAnimatorController = swordnboard;
        weapon = GetComponent<WeaponHolder>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
            if (controller.isJumping) return;
            if (!(inputVector.magnitude > 0))
                moveDirection = Vector3.zero;

            moveDirection = transform.forward * inputVector.y + transform.right * inputVector.x;
            float currentSpeed = controller.isRunning ? runSpeed : walkSpeed;
            Vector3 movementDirection = moveDirection * (currentSpeed);
        //transform.position += movementDirection;
        if (!controller.isAttacking)
        {
            if (rigidbody.velocity.magnitude < (currentSpeed / 2))
            {
                rigidbody.AddForce(movementDirection, ForceMode.Force);
            }
        }
        else
        {
            if (rigidbody.velocity.magnitude < (currentSpeed / 4))
            {
                rigidbody.AddForce(movementDirection, ForceMode.Force);
            }
        }

       float relativeZVelocity = Vector3.Dot(rigidbody.velocity, transform.forward);
       float relativeXVelocity = Vector3.Dot(rigidbody.velocity, transform.right);


        followTarget.transform.rotation *= Quaternion.AngleAxis(lookInput.x, Vector3.up);
        followTarget.transform.rotation *= Quaternion.AngleAxis(lookInput.y, Vector3.left);

        var angles = followTarget.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTarget.transform.localEulerAngles.x;

        if(angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        else if(angle < 180 && angle > 40)
        {
            angles.x = 40;
        }

        followTarget.transform.localEulerAngles = angles;

        transform.rotation = Quaternion.Euler(0, followTarget.transform.rotation.eulerAngles.y, 0);

        followTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);

        Vector3 temp = rigidbody.velocity;
        temp.Normalize();

        animator.SetFloat(movementXHash, relativeXVelocity);
        animator.SetFloat(movementYHash, relativeZVelocity);
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    public void OnMovement(InputValue value)
    {
        inputVector = value.Get<Vector2>();
        
    }

    public void OnRun(InputValue value)
    {
        controller.isRunning = value.isPressed;
        animator.SetBool(isRunningHash, controller.isRunning);
    }

    public void OnBlock(InputValue value)
    {
        if (weapon.rightHandEquip)
        {
            controller.isBlocking = value.isPressed;
            animator.SetBool(isBlockingHash, controller.isBlocking);
        }
    }

    public void OnAttack(InputValue value)
    {
        if (weapon.rightHandEquip)
        {
            if (!controller.isAttacking)
            {
                if (controller.energy > 10f)
                {
                    controller.isAttacking = true;
                    controller.energy -= 10f;
                    animator.SetTrigger("IsAttacking");
                }
                else
                    controller.isAttacking = false;

            }
        }
        
    }

    public void OnJump(InputValue value)
    {
        if (!controller.isJumping)
        {
            
            animator.SetBool(isJumpingHash, value.isPressed);
        }
    }

    public void Jump()
    {
        controller.isJumping = true;
        rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    public void OnSwitchWeapon(InputValue value)
    {
        if (shield)
        {
            animator.runtimeAnimatorController = twohanded;
            shield = false;
        }
        else
        {
            animator.runtimeAnimatorController = swordnboard;
            shield = true;
        }
    }

    public void OnInteract(InputValue value)
    {
        if(interactObject)
        {
            Well temp = interactObject.GetComponent<Well>();
            if(temp.canUse)
            {
                controller.TakeDamage(-25);
                temp.canUse = false;
                temp.Use();
            }
        }
    }

    public void OnPause(InputValue value)
    {
        if (!GameManager.instance.isPaused)
            GameManager.instance.PauseGame();
        else
            GameManager.instance.ResumeGame();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground"));
        {
            controller.isJumping = false;
            animator.SetBool(isJumpingHash, false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("EnemyAttack"))
        {
            if (!controller.isBlocking)
            {
                if (canGetHit)
                {
                    canGetHit = false;
                    Vector3 direction = transform.position - other.GetComponent<EnemyHand>().parent.transform.position;
                    direction = direction.normalized * 5;

                    rigidbody.AddForce(direction, ForceMode.Impulse);
                    PlaySound("TakeDamage");
                    controller.TakeDamage(5);
                    StartCoroutine(RecoverFromHit());
                    Debug.Log("zombie hit");
                }
            }
            else if(weapon.rightHandEquip)
            {
                Vector3 direction = transform.position - other.GetComponent<EnemyHand>().parent.transform.position;
                PlaySound("Block");
                direction = direction.normalized * 5;
                EnemyScript temp = other.GetComponent<EnemyHand>().parent.GetComponent<EnemyScript>();
                temp.GetHit(-direction, 0);
            }
        }
    }

    public void AddImpulse(float force)
    {
        rigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
    }

    public void TurnOffMotion()
    {
        applyMeshMotion = false;
    }

    IEnumerator RecoverFromHit()
    {
        yield return new WaitForSeconds(1f);
        canGetHit = true;
    }

    public void PlaySound(string name)
    {
        if(!audioSource.isPlaying)
        {
            audioSource.clip = soundManager.GetSound(name);
            audioSource.Play();
        }
    }

}
