using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    //Sauvegarde les données du jeu
    public void SaveGame()
    {
        int currentLevel = GameManager.currentSceneSave;
        float currentHealth = GameObject.FindWithTag("Player").GetComponent<PlayerDamage>().getCurrHealth();
        float currentTime = Timer.ElapsedTime;

        PlayerPrefs.SetInt("Level", currentLevel);
        PlayerPrefs.SetFloat("PlayerHealth", currentHealth);
        PlayerPrefs.SetFloat("Time", currentTime);
    }

    //Load les données du jeu
    public void LoadGame()
    {
        GameManager.currentSceneSave = PlayerPrefs.GetInt("Level");
        GameObject.FindWithTag("Player").GetComponent<PlayerDamage>().setCurrHealth(PlayerPrefs.GetFloat("PlayerHealth"));
        Timer.ElapsedTime = PlayerPrefs.GetFloat("Time");
    }
}
