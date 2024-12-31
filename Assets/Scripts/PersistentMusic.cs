using UnityEngine;

public class PersistentMusic : MonoBehaviour
{
    private static PersistentMusic instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); // Détruire les doublons
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Préserve cet objet entre les scènes
    }

    public void StopMusic()
    {
        Destroy(gameObject); // Arrête la musique et détruit l'objet
    }
}