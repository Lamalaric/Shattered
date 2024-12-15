using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    public static float ElapsedTime;

    // Update is called once per frame
    void FixedUpdate()
    {
        ElapsedTime += (float)Math.Round(Time.deltaTime, 2);
        timerText.text = ElapsedTime.ToString("0.00") + "s";
    }

    public static float GetElapsedTime()
    {
        return ElapsedTime;
    }
}
