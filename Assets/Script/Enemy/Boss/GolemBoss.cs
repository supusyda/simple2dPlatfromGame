using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBoss : Enemy
{
    // Start is called before the first frame update
    Vector3 startingPos;
    [SerializeField] protected bool hasRangeTarget;
    [SerializeField] protected DetectedZone RangePlayerDetected;
    [SerializeField] protected RangeAttack rangeAttack;
    private string ArrowDetectionZoneString = "ArrowDetectionZone";
    [SerializeField] GameObject rangeAttackDectecGameOjb;

    string laserName = "Laser";
    string golemArmName = "ArmProject";

    // protected float AttackCooldown { get => animator.GetFloat(AnimatorString.attackCooldown); }
    // bool isFiring;
    protected bool IsFiring
    {
        get => animator.GetBool("isFiring");
    }
    bool HasRangeTarget
    {
        get => hasRangeTarget;
        set
        {


            hasRangeTarget = value;
            animator.SetBool(AnimatorString.HasRangeTarget, value);

            if (value == true && base.AttackCooldown <= 0)
            {
                // Debug.Log(AttackCooldown);
                int rand = Random.RandomRange(0, 100);
                // Debug.Log(rand);
                Debug.Log(IsFiring);
                if (base.damageReciever.Hp <= base.damageReciever.baseHp / 2)
                {

                    if (IsFiring == false)
                    {
                        if (rand <= 50)
                        {
                            // CanMove = false;
                            rangeAttack.SetPrefab(laserName);

                            animator.SetTrigger(AnimatorString.rangeAttackLazer);

                        }
                        else
                        {
                            // Debug.Log(rand);

                            rangeAttack.SetPrefab(golemArmName);

                            animator.SetTrigger(AnimatorString.rangeAttack);

                        }
                    }

                }
                else
                {

                    rangeAttack.SetPrefab(golemArmName);

                    animator.SetTrigger(AnimatorString.rangeAttack);
                }

            }



        }


    }
    // [SerializeField] bool canMove;

    // bool CanMove
    // {
    //     get => animator.GetBool(AnimatorString.CanMove);

    // }
    private void Start()
    {
        startingPos = transform.position;
        rangeAttack = GetComponent<RangeAttack>();
        rangeAttackDectecGameOjb = transform.Find("ArrowDetectionZone").gameObject;
    }
    protected override void Update()
    {
        base.Update();


    }
    protected override void Reset()
    {
        base.Reset();
        RangePlayerDetected = transform.Find("RangeAttackDetectionZone").GetComponent<DetectedZone>();

    }
    void DetectedPlayerAtRange()
    {
        if (base.PlayerDetected.detectedColliderAll.Count > 0)
        {
            if (RangePlayerDetected.detectedColliderAll.Count > 0)
            {
                HasRangeTarget = true;
            }
            else
            {
                HasRangeTarget = false;

            }
        }
    }
    void MoveToPlayer()
    {
        if (base.PlayerDetected.detectedColliderAll.Count > 0)
        {
            Vector2 moveDir = (PlayerDetected.detectedColliderAll[0].transform.position - transform.position) * Time.deltaTime * base.walkSpeed;
            base.rigidbody2D.velocity = new Vector2(moveDir.x, moveDir.y);
            CharacterEvent.characterInView?.Invoke();

        }
        else
        {
            Vector2 moveDir = (startingPos - transform.position) * Time.deltaTime * base.walkSpeed;

            base.rigidbody2D.velocity = new Vector2(moveDir.x, moveDir.y);
            CharacterEvent.characterOutView.Invoke();

        }
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        DetectedPlayerAtRange();
        // if(base.Ca)
        if (base.CanMove)
        {
            MoveToPlayer();

        }
        if (base.damageReciever.Hp <= base.damageReciever.baseHp / 2 && base.HasTarget == true)
        {
            rangeAttackDectecGameOjb.SetActive(true);
        }

    }

    // Update is called once per frame

}
