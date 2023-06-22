using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// [RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class DamageReciever : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent<float, Vector2> damageAble;
    public UnityEvent<float, float> healthChange;
    // public UnityEvent disAble;
    // CharacterEvent characterEvent;


    [SerializeField] public float baseHp = 300;
    [SerializeField] float hp;
    [SerializeField] bool isAlive = true;
    [SerializeField] public bool isInvincible;
    public UnityEvent damageableDeath;
    public bool IsHit
    {
        get =>

            animator.GetBool(AnimatorString.IsHit);
        private set
        {

            animator.SetBool(AnimatorString.IsHit, value);

        }


    }

    float timeSinceHit = 0;
    Animator animator;
    [SerializeField] private float invincibleTime = 0.5f;

    public bool IsAlive
    {
        get => isAlive;
        set
        {
            isAlive = value;
            animator.SetBool(AnimatorString.isAlive, value);
            if (value == false)
            {
                animator.SetTrigger(AnimatorString.isDead);
                damageableDeath?.Invoke();
                CharacterEvent.characterOutView.Invoke();
                if (transform.parent.CompareTag("Player"))
                {
                    Debug.Log("IS PLAYER");
                    CharacterEvent.playerDie.Invoke();
                }
            }

        }


    }
    public float Hp
    {
        get => hp;
        set
        {
            hp = value;
            if (hp <= 0)
            {
                IsAlive = false;
            }
        }
    }
    public void ReduceHp(float damage)
    {
        if (isAlive && !isInvincible)
        {
            Hp = Hp - damage;
            isInvincible = true;
        }
    }
    public void ReduceHp2(float damage, Vector2 KnockBack)
    {
        if (isAlive && !isInvincible)
        {
            Hp = Hp - damage;
            isInvincible = true;
            if (IsAlive)
            {
                animator.SetTrigger(AnimatorString.hit);
            }
            damageAble?.Invoke(damage, KnockBack);
            healthChange?.Invoke(Hp, baseHp);
            CharacterEvent.characterDamage.Invoke(transform.parent.gameObject, damage);

        }
    }
    public void Heal(float healAmount)
    {
        if (isAlive)
        {
            float maxHeal = Mathf.Max(baseHp - Hp, 0);
            float actualHeal = Mathf.Min(maxHeal, healAmount);
            Hp += actualHeal;
            healthChange?.Invoke(Hp, baseHp);

            CharacterEvent.characterHeal.Invoke(transform.parent.gameObject, actualHeal);

        }
    }
    void HealMax(float healAmount)
    {

    }
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        Hp = baseHp;
        // Debug.Log(transform.parent.name + Hp);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(transform.parent.name + " hp " + Hp);
        if (isInvincible)
        {
            if (timeSinceHit >= invincibleTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }


    }
}
