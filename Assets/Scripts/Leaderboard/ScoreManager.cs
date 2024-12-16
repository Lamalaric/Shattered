using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inputScore;
    [SerializeField] private TMP_InputField inputName;
    public UnityEvent<string, int> submitScoreEvent;


    public void SubmitScore()
    {
        //Retire le char 's' à la fin du text score
        Debug.Log((int)float.Parse(inputScore.text.Remove(inputScore.text.Length - 1)));
        int formattedScore = (int)float.Parse(inputScore.text.Remove(inputScore.text.Length - 1));

        Debug.Log(inputName.text + "///" + formattedScore);
        submitScoreEvent.Invoke(inputName.text, formattedScore);
    }
}
