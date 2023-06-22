using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Animator animator;
    Vector2 moveInput;
    [SerializeField] DamageSender damageSender;
    [SerializeField] DamageReciever damageReciever;


    [SerializeField] DetectedZone detectedZone;
    TouchingDirection touchingDirection;
    [SerializeField] float jumpForce;
    [SerializeField] bool hasTarget;
    bool HasTarget
    {
        get => hasTarget;
        set
        {


            hasTarget = value;
            animator.SetBool(AnimatorString.HasTarget, value);


        }


    }
    bool isMoving = false;
    float walkSpeed = 5f;
    float runSpeed = 8f;
    float airSpeed = 3f;
    public bool CanMove { get => animator.GetBool(AnimatorString.CanMove); }
    public bool IsHit { get => animator.GetBool(AnimatorString.IsHit); }

    public float CurrentSpeed
    {
        get
        {
            if (CanMove)
            {


                if (IsMoving == true && !touchingDirection.IsOnWall)
                {
                    if (touchingDirection.IsGrounded == true)
                    {
                        if (IsRunning == true)
                        {
                            return runSpeed;

                        }
                        else
                        {
                            return walkSpeed;
                        }
                    }
                    else
                    {
                        return airSpeed;

                    }

                }

                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
    }
    public bool isFacingRight = true;
    public bool IsFacingRight
    {
        get => isFacingRight;
        set
        {

            if (isFacingRight != value)
            {

                if (value == true)
                {
                    transform.localScale = new Vector2(1, 1);

                }
                else
                {
                    transform.localScale = new Vector2(-1, 1);

                }

            }

            isFacingRight = value;

        }




    }

    public bool IsMoving
    {
        get => isMoving;
        private set
        {
            isMoving = value;
            animator.SetBool(AnimatorString.isMoving, isMoving);
        }
    }
    bool isRunning = false;
    public bool IsRunning
    {
        get => isRunning;
        private set
        {
            isRunning = value;
            animator.SetBool(AnimatorString.isRunning, isRunning);

        }
    }
    Rigidbody2D rigidbody2D;
    [SerializeField] float moveSpeed = 5f;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirection = GetComponent<TouchingDirection>();
        detectedZone = transform.Find("DetectionZone").GetComponent<DetectedZone>();
        damageSender = GetComponentInChildren<DamageSender>();
        damageReciever = GetComponentInChildren<DamageReciever>();

    }


    // Update is called once per frame
    void FixedUpdate()

    {
        // Debug.Log(moveInput.x * moveSpeed);
        if (!damageReciever.IsHit)
        {
            rigidbody2D.velocity = new Vector2(moveInput.x * CurrentSpeed, rigidbody2D.velocity.y);
            animator.SetFloat(AnimatorString.yVelocity, rigidbody2D.velocity.y);
        }


    }
    void DealDamageToEnemy()
    {
        if (detectedZone.detectedColliderAll.Count > 0)
        {
            foreach (Collider2D detectedCollider in detectedZone.detectedColliderAll)
            {
                Debug.Log(detectedCollider.transform);
                damageSender.CheckCanDealDamage(detectedCollider.transform);

            }
        }



    }
    void SetFacing(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;

        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {

        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
        SetFacing(moveInput);



    }
    public void OnRun(InputAction.CallbackContext context)
    {

        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;

        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirection.IsGrounded == true)
        {
            // IsJumping = true;
            rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            animator.SetTrigger(AnimatorString.isJumping);

        }

    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimatorString.Attack);

        }

    }
    public void OnRangeAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimatorString.rangeAttack);

        }

    }

    public void OnHit(float damage, Vector2 KnockBack)
    {
        rigidbody2D.velocity = new Vector2(KnockBack.x, rigidbody2D.velocity.y + KnockBack.y);
        // rigidbody2D.AddForce(KnockBack, ForceMode2D.Impulse);

    }

}
