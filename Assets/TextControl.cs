using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Use TMPro if using TextMeshPro
using TMPro;
using UnityEngine.SceneManagement;


public class SequentialTextDisplay : MonoBehaviour
{

    //public Text[] textLines; // For Unity's Text elements
    public TMP_Text[] textLines; // Uncomment if using TextMeshPro
    public float delayBetweenLines = 1.5f; // Time between lines appearing
    public float delayBeforeSceneChange = 2.5f;
    void Start()
    {
        StartCoroutine(ShowTextLines());
    }

    IEnumerator FadeInText(TMP_Text text)
    {
        float duration = 1f; // Duration of fade
        Color originalColor = text.color;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float alpha = t / duration;
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f); // Fully visible
    }

    IEnumerator ShowTextLines()
    {
        foreach (TMP_Text text in textLines)
        {
            yield return StartCoroutine(FadeInText(text));
            yield return new WaitForSeconds(delayBetweenLines);
        }
        yield return new WaitForSeconds(delayBeforeSceneChange);
        SceneManager.LoadScene(nextScene);
    }

}
