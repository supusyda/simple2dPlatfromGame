using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage = 2f;
    public DamageReciever damageReciever;
    public Vector2 knockBack = Vector2.zero;
    private void OnTriggerEnter2D(Collider2D other)
    {

        damageReciever = other.GetComponentInChildren<DamageReciever>();
        if (damageReciever != null)

        {
            knockBack = new Vector2(knockBack.x * transform.parent.localScale.x, knockBack.y);

            Debug.Log(other.transform.name + "hp " + damageReciever.Hp);
            damageReciever.ReduceHp2(damage, knockBack);
            // damageReciever.ReduceHp(damage);

            Debug.Log("enemy hit player for" + damage);




        }
    }
}
