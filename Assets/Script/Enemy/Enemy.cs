using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection))]

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected float walkSpeed = 3f;
    // [SerializeField] DamageSender damageSender;
    // [SerializeField] DetectedZone detectedZone;
    [SerializeField] protected DetectedZone detectedAttackZone;

    [SerializeField] protected DetectedZone cliffDetectedZone;
    [SerializeField] protected DetectedZone PlayerDetected;

    [SerializeField] protected Animator animator;
    [SerializeField] protected Rigidbody2D rigidbody2D;
    [SerializeField] protected DamageReciever damageReciever;
    public float AttackCooldown
    {
        get => animator.GetFloat(AnimatorString.attackCooldown);

        set => animator.SetFloat(AnimatorString.attackCooldown, value);
    }
     public float isAlive
    {
        get => animator.GetFloat(AnimatorString.attackCooldown);

        set => animator.SetFloat(AnimatorString.attackCooldown, value);
    }
    [SerializeField] bool canMove;

    protected bool CanMove
    {
        get => animator.GetBool(AnimatorString.CanMove);

    }
    public enum WalkingAbleDiraction { Left, Right };
    public WalkingAbleDiraction walkDirect = WalkingAbleDiraction.Left;
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
    [SerializeField] protected Vector2 walkingDirVector = Vector2.right;
    [SerializeField] protected TouchingDirection touchingDirection;
    [SerializeField] protected bool hasTarget;

  protected  bool HasTarget
    {
        get => hasTarget;
        set
        {


            hasTarget = value;
            animator.SetBool(AnimatorString.HasTarget, value);


        }


    }

    protected virtual void Reset()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        touchingDirection = GetComponent<TouchingDirection>();
        PlayerDetected = transform.parent.Find("DetectionZone").GetComponent<DetectedZone>();
        animator = GetComponent<Animator>();
        // damageSender = GetComponentInChildren<DamageSender>();
        cliffDetectedZone = transform.Find("CliffDetectedZone").GetComponent<DetectedZone>();
        detectedAttackZone = transform.Find("AttackDetectionZone").GetComponent<DetectedZone>();
        damageReciever = transform.GetComponentInChildren<DamageReciever>();


    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (detectedAttackZone.detectedColliderAll.Count > 0)
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
    protected virtual void FixedUpdate()
    {
        if (rigidbody2D.velocity.x >= 0.0001f)
        {
            if (WalkDirect == WalkingAbleDiraction.Left)
            {
                WalkDirect = WalkingAbleDiraction.Right;
            }

        }
        else
        {
            if (WalkDirect == WalkingAbleDiraction.Right)
            {
                WalkDirect = WalkingAbleDiraction.Left;
            }
        }
    }

}
