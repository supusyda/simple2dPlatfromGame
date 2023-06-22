using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameoverScreen : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] DamageReciever playerHealth;
    void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent<DamageReciever>();
    }

    // Update is called once per frame
    public void RestartGame()
    {
        SceneManager.LoadScene("hi");
    }
}
