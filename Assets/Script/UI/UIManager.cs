using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject damageText;
    public GameObject healDamageText;
    public GameObject BossHealthBar;
    public GameObject GameoverScreen;



    public Canvas canvas;
    UnityEvent unityEvent;
    // [SerializeField] GolemBoss golemBoss;


    void Start()
    {
        // canvas = GameObject.FindAnyObjectByType<Canvas>();
        CharacterEvent.characterDamage += CharacterTakeDamage;
        CharacterEvent.characterHeal += CharacterHealDamage;
        CharacterEvent.characterInView += ShowHealthBar;
        CharacterEvent.characterOutView += HideHealthBar;
        CharacterEvent.playerDie += ShowGameOverScreen;


        // golemBoss = GameObject.Find("Boss").GetComponent<GolemBoss>();


    }
    public void ShowGameOverScreen()
    {
        // Create text at character hit
        if (!GameoverScreen) return;
        GameoverScreen.SetActive(true);

    }
    public void ShowHealthBar()
    {
        // Create text at character hit
        if (!BossHealthBar) return;

        BossHealthBar.SetActive(true);

    }
    public void HideHealthBar()
    {
        if (!BossHealthBar) return;
        // Create text at character hit
        BossHealthBar.SetActive(false);

    }
    // Update is called once per frame
    public void CharacterTakeDamage(GameObject character, float damage)
    {
        // Create text at character hit
        if (!canvas) return;
        Vector3 textSpawnLocation = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpText = Instantiate(damageText, textSpawnLocation, Quaternion.identity, canvas.transform).GetComponent<TMP_Text>();
        tmpText.text = damage.ToString();

    }
    public void CharacterHealDamage(GameObject character, float damage)
    {
        Vector3 textSpawnLocation = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpText = Instantiate(healDamageText, textSpawnLocation, Quaternion.identity, canvas.transform).GetComponent<TMP_Text>();
        tmpText.text = damage.ToString();

    }
    public void OnEscape()
    {
#if (UNITY_EDITOR)

        UnityEditor.EditorApplication.isPlaying = false;
#elif (DEVELOPMENT_BUILD)
        Application.Quit();
        
#elif (UNITY_WEBGL)
    SceneManager.LoadScene("QuitScene");    
#endif
    }
}
