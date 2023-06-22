using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarBoss : HeathBar
{
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        GameObject boss = GameObject.Find("Boss");
        // Debug.Log(player);
        damageReciever = boss.GetComponentInChildren<DamageReciever>();

    }
}
