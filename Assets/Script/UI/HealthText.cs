using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthText : MonoBehaviour
{
    public Vector3 moveUp = new Vector3(0, 75, 0);
    public TextMeshProUGUI textMeshProUGUI;
    public RectTransform rectTransform;
    public float timeTofade = 1f;
    float timeElapse;
    Color startColor;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        startColor = textMeshProUGUI.color;
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.position = rectTransform.position + moveUp * Time.deltaTime;
        timeElapse += Time.deltaTime;
        if (timeElapse < timeTofade)
        {
            float fadeValue = startColor.a * (1 - (timeElapse / timeTofade));
            textMeshProUGUI.color = new Color(startColor.r, startColor.g, startColor.b, fadeValue);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
