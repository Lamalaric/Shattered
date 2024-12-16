using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private GameObject textToEdit;

    //Sauvegarde les données du jeu
    public void SaveGame()
    {
        StartCoroutine(SaveGameRoutine());
    }
    //Load les données du jeu
    public void LoadGame()
    {
        StartCoroutine(LoadGameRoutine());
    }

    private IEnumerator SaveGameRoutine()
    {
        int currentLevel = GameManager.currentSceneSave;
        float currentHealth = GameObject.FindWithTag("Player").GetComponent<PlayerDamage>().getCurrHealth();
        float currentTime = Timer.ElapsedTime;

        PlayerPrefs.SetInt("Level", currentLevel);
        PlayerPrefs.SetFloat("PlayerHealth", currentHealth);
        PlayerPrefs.SetFloat("Time", currentTime);

        textToEdit.GetComponent<TextMeshProUGUI>().text = "Game has been saved !";
        yield return new WaitForSeconds(2);
        textToEdit.GetComponent<TextMeshProUGUI>().text = "";
    }

    private IEnumerator LoadGameRoutine()
    {
        GameManager.currentSceneSave = PlayerPrefs.GetInt("Level");
        GameObject.FindWithTag("Player").GetComponent<PlayerDamage>().setCurrHealth(PlayerPrefs.GetFloat("PlayerHealth"));
        Timer.ElapsedTime = PlayerPrefs.GetFloat("Time");

        textToEdit.GetComponent<TextMeshProUGUI>().text = "Game has been loaded !";
        yield return new WaitForSeconds(2);
        textToEdit.GetComponent<TextMeshProUGUI>().text = "";
    }
}
