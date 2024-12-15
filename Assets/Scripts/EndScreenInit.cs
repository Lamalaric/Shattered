using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreenInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //retrieve le temps pour l'afficher
        Debug.Log(GameManager.time);
        float formattedTime = float.Parse(GameManager.time.Remove(GameManager.time.Length - 1));
        GameObject.Find("ScoreValue").GetComponent<TextMeshProUGUI>().text = Math.Round(formattedTime, 2) + "s";
    }
}
