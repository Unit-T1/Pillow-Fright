using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void RestartButton()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void MenuButton()
    {   
        SceneManager.LoadScene("Menu");
    }
}
