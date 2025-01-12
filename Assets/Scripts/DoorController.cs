using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField] private int nextScene;
    private int nbOfLevels = 5;
    
    public AudioSource audioSource;  // Audio Source attachée au joueur
    public AudioClip openSFX;

    public void CompleteLevel()
    {
        StartCoroutine(PlaySoundAndLoadScene());
    }

    private IEnumerator PlaySoundAndLoadScene()
    {
        // Jouer le son
        audioSource.PlayOneShot(openSFX);
        Debug.Log($"Level {nextScene} completed");
        GameManager.currentSceneSave = nextScene;

        // Attendre la durée du son avant de charger la prochaine scène
        yield return new WaitForSeconds(openSFX.length);

        // Charger la prochaine scène (menu de win si tous les niveaux sont faits)
        if (nextScene > nbOfLevels)
            GameManager.UpdateGameState(GameState.Win);
        else
            SceneManager.LoadScene(nextScene);
    }
}