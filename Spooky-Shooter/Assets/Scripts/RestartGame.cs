using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void StartGameAgain() 
    {
        SceneManager.LoadScene("Game Scene");
    }

    public void GoBackToMenu()
    {
        SceneManager.LoadScene("Menu Scene");
    }
}
