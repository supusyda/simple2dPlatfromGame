using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarPlayer : HeathBar
{
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        // Debug.Log(player);
        damageReciever = player.GetComponentInChildren<DamageReciever>();

    }
}
