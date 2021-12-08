using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    int previousScene = 0;

    public void Start()
    {
        previousScene = PlayerControls.currentScene;
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
