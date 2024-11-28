using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HeartManager : MonoBehaviour
{
    public GameObject heartPrefab; // Le prefab de cœur
    public Transform heartContainer; // Le conteneur pour les cœurs
    private List<Image> hearts = new List<Image>(); // Liste des cœurs générés

    // Initialiser les cœurs en fonction de la santé maximale
    public void InitializeHearts(int maxHealth)
    {
        // Nettoyer les cœurs existants
        foreach (Transform child in heartContainer)
        {
            Destroy(child.gameObject);
        }
        hearts.Clear();

        // Générer les cœurs
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, heartContainer);
            hearts.Add(newHeart.GetComponent<Image>());
        }
    }

    // Mettre à jour les cœurs en fonction de la santé actuelle
    public void UpdateCurrentHealth(float currentHealth)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < Mathf.Floor(currentHealth))
            {
                hearts[i].fillAmount = 1f; // Plein
            }
            else if (i < currentHealth)
            {
                hearts[i].fillAmount = currentHealth - Mathf.Floor(currentHealth); // Partiel
            }
            else
            {
                hearts[i].fillAmount = 0f; // Vide
            }
        }
    }
}