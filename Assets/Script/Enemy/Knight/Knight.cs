using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection))]
public class Knight : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float walkSpeed = 3f;
    [SerializeField] DamageSender damageSender;
    [SerializeField] DetectedZone detectedZone;
    [SerializeField] DetectedZone cliffDetectedZone;

    Animator animator;
    Rigidbody2D rigidbody2D;
    public float AttackCooldown
    {
        get => animator.GetFloat(AnimatorString.attackCooldown);

        set => animator.SetFloat(AnimatorString.attackCooldown, value);
    }
    [SerializeField] bool canMove;

    bool CanMove
    {
        get => animator.GetBool(AnimatorString.CanMove);

    }
    public enum WalkingAbleDiraction { Left, Right };
    public WalkingAbleDiraction walkDirect = WalkingAbleDiraction.Right;
    [SerializeField] private Vector2 walkingDirVector = Vector2.right;
    [SerializeField] TouchingDirection touchingDirection;
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
    public WalkingAbleDiraction WalkDirect
    {
        get => walkDirect;
        set
        {
            if (walkDirect != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if (value == WalkingAbleDiraction.Left)
                {
                    walkingDirVector = Vector2.left;
             
                    walkDirect = WalkingAbleDiraction.Left;

                }
                else if (value == WalkingAbleDiraction.Right)
                {
                    walkingDirVector = Vector2.right;
                    walkDirect = WalkingAbleDiraction.Right;
                }
            }
        }
    }
    void DealDamageToPlayer()
    {
        if (!HasTarget) return;
        damageSender.CheckCanDealDamage(detectedZone.detectedColliderAll[0].transform);


    }

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        touchingDirection = GetComponent<TouchingDirection>();
        detectedZone = transform.Find("DetectionZone").GetComponent<DetectedZone>();
        animator = GetComponent<Animator>();
        damageSender = GetComponentInChildren<DamageSender>();
        cliffDetectedZone = transform.Find("CliffDetectedZone").GetComponent<DetectedZone>();

    }

    void Update()
    {
        if (detectedZone.detectedColliderAll.Count > 0)
        {
            HasTarget = true;
        }
        else
        {
            HasTarget = false;
        }
        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (touchingDirection.IsGrounded && touchingDirection.isOnWall)
        {
            if (WalkDirect == WalkingAbleDiraction.Left)
            {
                WalkDirect = WalkingAbleDiraction.Right;
            }
            else if (WalkDirect == WalkingAbleDiraction.Right)
            {
                WalkDirect = WalkingAbleDiraction.Left;
            }
        }
        if (CanMove && touchingDirection.isGrounded)
        {
            if (HasTarget == true)
            {
                rigidbody2D.velocity = new Vector2(Mathf.Lerp(rigidbody2D.velocity.x, 0, 0.05f), rigidbody2D.velocity.y);

            }
            else
            {
                rigidbody2D.velocity = new Vector2(walkingDirVector.x * walkSpeed, rigidbody2D.velocity.y);

            }
        }

    }
    public void OnHit(float damage, Vector2 KnockBack)
    {
        rigidbody2D.velocity = new Vector2(KnockBack.x, rigidbody2D.velocity.y + KnockBack.y);
        // rigidbody2D.AddForce(KnockBack, ForceMode2D.Impulse);

    }
    public void OnNoCollider()
    {
        if (touchingDirection.isGrounded)
        {
            if (WalkDirect == WalkingAbleDiraction.Left)
            {
                WalkDirect = WalkingAbleDiraction.Right;
            }
            else if (WalkDirect == WalkingAbleDiraction.Right)
            {
                WalkDirect = WalkingAbleDiraction.Left;
            }
        }
    }

}
