using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] List<Transform> wayPoints;
    [SerializeField] int currentWaypoint = 1;
    [SerializeField] float speed;
    [SerializeField] bool hasTarget;
    [SerializeField] DamageReciever damageReciever;
    Collider2D deathColider;
    Rigidbody2D rb;

    Animator animator;
    DetectedZone detectedZone;
    bool HasTarget
    {
        get => hasTarget;
        set
        {


            hasTarget = value;
            animator.SetBool(AnimatorString.HasTarget, value);
            damageReciever = GetComponentInChildren<DamageReciever>();
            rb = GetComponent<Rigidbody2D>();


        }
    }
    bool CanMove
    {
        get => animator.GetBool(AnimatorString.CanMove);

    }
    void Start()
    {
        Transform wayPointsOjb = transform.parent.Find("WaypointHolder");
        foreach (Transform item in wayPointsOjb)
        {
            wayPoints.Add(item);
        }
        animator = GetComponent<Animator>();
        detectedZone = transform.Find("DetectionZone").GetComponent<DetectedZone>();
        deathColider = GameObject.Find("DeathDetecedZone").GetComponent<BoxCollider2D>();


    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(wayPoints.Count);


        if (detectedZone.detectedColliderAll.Count > 0 && damageReciever.IsAlive)
        {
            HasTarget = true;
        }
        else
        {
            HasTarget = false;
        }

        {
            if (damageReciever.IsAlive)
            {
                if (CanMove)
                {
                    Move();

                }
            }


        }
    }
    void Move()
    {
        if (Vector2.Distance(transform.position, wayPoints[currentWaypoint].position) < 0.1f)
        {
            currentWaypoint++;
            if (currentWaypoint > wayPoints.Count - 1)
            {
                currentWaypoint = 0;
            }
            transform.localScale = new Vector2(transform.localScale.x > 0 ? -1 : 1, transform.localScale.y);
        }
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentWaypoint].position, Time.deltaTime * speed);
    }

    public void OnDeath()
    {
        rb.gravityScale = 2;
        if (deathColider)
        {
            deathColider.enabled = true;

        }
        Debug.Log("cc");
    }

}
