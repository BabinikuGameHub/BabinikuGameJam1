using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void StartGameClick()
    {
        SceneManager.LoadScene(1);

    }

    public void EndGameClick()
    {
        Application.Quit();
    }
}
