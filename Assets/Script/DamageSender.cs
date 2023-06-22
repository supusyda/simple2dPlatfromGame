using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float damageDeal = 100;


    // Update is called once per frame

    public void CheckCanDealDamage(Transform ojb)
    {
        DamageReciever dameReceiver = ojb.GetComponentInChildren<DamageReciever>();
        if (!dameReceiver)
        {
            return;
        }
        this.DealDamage(dameReceiver);



    }
    public void DealDamage(DamageReciever dameReceiver)
    {
        dameReceiver.ReduceHp(this.damageDeal);
        // this.DestroyOjb();
    }
    public void DestroyOjb()
    {
        Destroy(transform.parent.gameObject);
    }


}
