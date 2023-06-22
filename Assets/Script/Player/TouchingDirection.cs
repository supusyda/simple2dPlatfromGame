using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirection : MonoBehaviour
{
    CapsuleCollider2D capsuleCollider2D;
    [SerializeField] ContactFilter2D contactFilter2D;
    public float groundDistant = 0.05f;
    public float wallDistant = 0.2f;
    public float cellDistant = 0.05f;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] cellingHits = new RaycastHit2D[5];

    // Start is called before the first frame update
    public Vector2 WallCheckDir => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
    Animator animator;
    public bool isGrounded = true;
    public bool IsGrounded
    {
        get => isGrounded;
        set
        {

            isGrounded = value;
            animator.SetBool(AnimatorString.isGrounded, value);

        }
    }

    public bool isOnWall;
    public bool IsOnWall
    {
        get => isOnWall;
        set
        {

            isOnWall = value;
            animator.SetBool(AnimatorString.isOnWall, value);

        }
    }
    public bool isOnCelling;
    public bool IsOnCelling
    {
        get => isOnCelling;
        set
        {

            isOnCelling = value;
            animator.SetBool(AnimatorString.isOnCelling, value);

        }
    }
    void Start()
    {
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (capsuleCollider2D.Cast(Vector2.down, contactFilter2D, groundHits, groundDistant) > 0)
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;

        }
        IsOnWall = capsuleCollider2D.Cast(WallCheckDir, contactFilter2D, wallHits, wallDistant) > 0;

        // if (capsuleCollider2D.Cast(WallCheckDir, contactFilter2D, wallHits, wallDistant) > 0)
        // {
        //     IsOnWall = true;
        // }
        // else
        // {
        //     IsOnWall = false;

        // }
        IsOnCelling = capsuleCollider2D.Cast(Vector2.up, contactFilter2D, cellingHits, cellDistant) > 0;
        // Debug.Log(WallCheckDir);

    }
    void OnDrawGizmosSelected()
    {
        // Gizmos.DrawLine(transform.position, transform.position * wallDistant);
    }
}
