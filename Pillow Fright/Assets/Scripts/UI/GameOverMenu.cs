using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    private int previousScene;

    public void Start()
    {
        previousScene = SceneManager.GetActiveScene().buildIndex - 1;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(previousScene);
    }

    public void MenuButton()
    {   
        SceneManager.LoadScene("Menu");
    }
}
