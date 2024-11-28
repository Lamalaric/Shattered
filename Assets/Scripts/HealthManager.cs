using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private TMP_Text healthText;
    private PlayerDamage playerInfos;

    //Get the player damage component
    void Start()
    {
        playerInfos = GetComponent<PlayerDamage>();
    }

    //Fill the health jauge bar
    void Update()
    {
        float maxHealth = playerInfos.getMaxHealth();
        float currentHealth = playerInfos.getCurrHealth();

        healthBar.fillAmount = currentHealth / maxHealth;
        healthText.text = currentHealth.ToString();
        Debug.Log(healthBar.fillAmount);
    }
}
