using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    // public DamageReciever damageReciever;
    [SerializeField] Rigidbody2D bulletRB;
    // [SerializeField] AudioSource shootingAudio;
    // public Animator animator;
    [SerializeField] DamageReciever damageReciever;
    public Vector2 knockBack = Vector2.zero;

    public float thrust = 10f;
    public float damage = 10f;

    void Reset()
    {
        bulletRB = GetComponent<Rigidbody2D>();

        damageReciever = GetComponentInChildren<DamageReciever>();
    }
    private void Update()
    {
        bulletRB.velocity = new Vector3(thrust * transform.localScale.x, 0);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log(other.name);
        damageReciever = other.GetComponentInChildren<DamageReciever>();
        if (damageReciever != null)

        {
            knockBack = new Vector2(knockBack.x * transform.localScale.x, knockBack.y);

            Debug.Log(other.transform.name + "hp " + damageReciever.Hp);
            damageReciever.ReduceHp2(damage, knockBack);
            Destroy(gameObject);




        }
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);

        }
    }
}
