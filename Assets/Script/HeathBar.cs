using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HeathBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected DamageReciever damageReciever;
    [SerializeField] protected Slider healthSlider;
    [SerializeField] protected TMP_Text healthBarText;



    private void Start()
    {
        healthSlider.value = CalSliderPercent(damageReciever.baseHp, damageReciever.baseHp);
        healthBarText.text = "HP:" + damageReciever.baseHp + "/" + damageReciever.baseHp;
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnEnable()
    {
        damageReciever.healthChange.AddListener(OnPlayHealChanged);
    }
    private void OnDisable()
    {
        damageReciever.healthChange.RemoveListener(OnPlayHealChanged);
    }
    float CalSliderPercent(float currentHP, float maxHP)

    {
        return currentHP / maxHP;
    }
    void OnPlayHealChanged(float currentHP, float maxHP)
    {
        healthSlider.value = CalSliderPercent(damageReciever.Hp, damageReciever.baseHp);
        healthBarText.text = "HP:" + damageReciever.Hp + "/" + damageReciever.baseHp;
    }
}
