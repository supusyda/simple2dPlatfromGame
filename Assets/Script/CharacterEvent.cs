using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class CharacterEvent
{
    public static UnityAction<GameObject, float> characterDamage;
    public static UnityAction<GameObject, float> characterHeal;
    public static UnityAction characterInView;
    public static UnityAction characterOutView;
    public static UnityAction playerDie;




}
