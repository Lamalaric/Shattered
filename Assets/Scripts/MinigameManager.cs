using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinigameManager : MonoBehaviour
{
    public GameObject miniGameCanvas;   // Canvas principal du mini-jeu
    public GameObject targetPrefab;     // Prefab des cibles
    public Button playButton;           // Bouton Play
    public GameObject sidePanel;        // Side panel avec score
    public RectTransform spawnZone;

    public TextMeshProUGUI timerText;              // Texte affichant le temps restant
    public TextMeshProUGUI scoreText;
    public int gameDuration = 10;       // Durée du mini-jeu en secondes
    public int targetsToSpawn = 20;     // Nombre total de cibles à faire apparaître

    private float timeRemaining;
    private int score = 0;
    public bool miniGameActive = false;

    void Start()
    {
        // Assure-toi que le Canvas est désactivé au départ
        miniGameCanvas.SetActive(false);
        playButton.onClick.AddListener(StartMiniGame);
    }

    // Fonction appelée quand on clique sur la barre de vie
    public void ShowMiniGameCanvas()
    {
        miniGameCanvas.SetActive(true);
        ClearAllTargets();
        score = 0;
        scoreText.text = "0";
        timeRemaining = gameDuration;
        miniGameActive = true;
        Timer.PauseTimer();
    }

    void StartMiniGame()
    {
        playButton.gameObject.SetActive(false);
        sidePanel.gameObject.SetActive(true);
        StartCoroutine(MiniGameCoroutine());
    }

    IEnumerator MiniGameCoroutine()
    {
        timeRemaining = gameDuration; // Initialise le timer
        score = 0;

        // Démarre le timer en parallèle
        StartCoroutine(StartTimer());

        // Apparition des cibles en parallèle du timer
        for (int i = 0; i < targetsToSpawn; i++)
        {
            SpawnTarget();
            yield return new WaitForSeconds(1f); // Intervalle entre les cibles
        }

        // Quand toutes les cibles sont apparues, on attend que le timer atteigne 0
        while (timeRemaining > 0)
        {
            yield return null; // Attend chaque frame
        }

        EndMiniGame(); // Termine le mini-jeu
    }

    IEnumerator StartTimer()
    {
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = Mathf.Clamp(timeRemaining, 0, gameDuration).ToString("F1");
            yield return null; // Attend chaque frame
        }
    }


    void SpawnTarget()
    {
        // Récupère les dimensions locales de la zone de spawn (Panel)
        RectTransform spawnRect = spawnZone.GetComponent<RectTransform>();

        // Position locale aléatoire dans le RectTransform
        float randomX = Random.Range(spawnRect.rect.xMin+100, spawnRect.rect.xMax-100);
        float randomY = Random.Range(spawnRect.rect.yMin+100, spawnRect.rect.yMax-100);
        Vector2 localPosition = new Vector2(randomX, randomY);

        // Instancie la cible en tant qu'enfant du Canvas
        GameObject target = Instantiate(targetPrefab, spawnZone);
        RectTransform targetRect = target.GetComponent<RectTransform>();

        // Positionne correctement dans le Canvas
        targetRect.anchoredPosition = localPosition;

        // Ajuste l'échelle pour qu'elle soit visible
        targetRect.localScale = Vector3.one * 4f;

        // Abonne l'événement OnClicked de la cible à la fonction TargetClicked
        Target targetScript = target.GetComponent<Target>();
        if (targetScript != null)
        {
            targetScript.OnClicked += TargetClicked; // Abonnement ici
        }
        else
        {
            Debug.LogError("Le prefab Target n'a pas de script Target attaché !");
        }
    }


    void TargetClicked(GameObject target)
    {
        Destroy(target);
        score++;
        scoreText.text = score.ToString() + "/" + targetsToSpawn.ToString();

        if (score >= targetsToSpawn)
        {
            StopAllCoroutines();
            EndMiniGame();
        }
    }

    public void EndMiniGame()
    {
        playButton.gameObject.SetActive(true);
        miniGameCanvas.SetActive(false);
        miniGameActive = false;
        Timer.PauseTimer();

        ClearAllTargets(); // Nettoyer les cibles restantes

        if (score >= targetsToSpawn) // Exemple de condition pour regagner de la vie
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamage>().addHealth(5);
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamage>().TakeDamage(5);
        }
    }

    void ClearAllTargets()
    {
        GameObject[] allTargets = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject target in allTargets)
        {
            Destroy(target);
        }
    }
}
