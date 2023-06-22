using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class PickFruit : MonoBehaviour
{
    // Start is called before the first frame update
    public float hpRestore = 10;
    private void OnTriggerEnter2D(Collider2D other)
    {

        DamageReciever damageReciever = other.transform.GetComponentInChildren<DamageReciever>();
        if (damageReciever.Hp == damageReciever.baseHp)
        {
            return;
        }
        if (damageReciever)
        {
            damageReciever.Heal(hpRestore);
            Destroy(gameObject);
        }
    }
}
