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
    public RuntimeAnimatorController twohanded;
    public RuntimeAnimatorController swordnboard;
    bool shield = true;
    

    //references
    Vector2 inputVector = Vector2.zero;
    Vector3 moveDirection = Vector3.zero;
    Vector2 lookInput = Vector2.zero;
    public GameObject followTarget;

    // animator hashes
    public readonly int movementXHash = Animator.StringToHash("MoveX");
    public readonly int movementYHash = Animator.StringToHash("MoveY");
    public readonly int isRunningHash = Animator.StringToHash("IsRunning");
    public readonly int isJumpingHash = Animator.StringToHash("IsJumping");
    public readonly int isAttackingHash = Animator.StringToHash("IsAttacking");
    public readonly int isBlockingHash = Animator.StringToHash("IsBlocking");

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        controller = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        //animator.runtimeAnimatorController = swordnboard;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
 
            if (controller.isJumping) return;
            if (!(inputVector.magnitude > 0))
                moveDirection = Vector3.zero;

            moveDirection = transform.forward * inputVector.y + transform.right * inputVector.x;
            float currentSpeed = controller.isRunning ? runSpeed : walkSpeed;
            Vector3 movementDirection = moveDirection * (currentSpeed);
        //transform.position += movementDirection;
        if (rigidbody.velocity.magnitude < (currentSpeed / 2))
        {
            rigidbody.AddForce(movementDirection, ForceMode.Force);
        }

       float relativeZVelocity = Vector3.Dot(rigidbody.velocity.normalized, transform.forward);
       float relativeXVelocity = Vector3.Dot(rigidbody.velocity.normalized, transform.right);


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
        controller.isBlocking = value.isPressed;
        animator.SetBool(isBlockingHash, controller.isBlocking);
    }

    public void OnAttack(InputValue value)
    {
        controller.isAttacking = value.isPressed;
        if (!controller.isAttacking)
        {
            if (controller.energy > 20f)
            {
                controller.energy -= 20f;
            }
            else
                controller.isAttacking = false;
        }
            animator.SetBool(isAttackingHash, controller.isAttacking);
    }

    public void OnJump(InputValue value)
    {
        if (!controller.isJumping)
        {
            controller.isJumping = value.isPressed;
            rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            animator.SetBool(isJumpingHash, controller.isJumping);
        }
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

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground") && !controller.isJumping) return;
        {
            controller.isJumping = false;
            animator.SetBool(isJumpingHash, controller.isJumping);
        }
    }
}
