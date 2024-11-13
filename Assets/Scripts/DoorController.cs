using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public void CompleteLevel()
    {
        Debug.Log("Level completed");
        SceneManager.LoadScene("Win Menu");
    }
}