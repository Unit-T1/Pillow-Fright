using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelAdministrator : MonoBehaviour
{
    Vector2 playerStartingPosition;

    void Start()
    {
        playerStartingPosition = FindObjectOfType<PlayerControls>().transform.position;
    }

    public void Restart()
    {
        // Reset the position of the player back to the starting point when died
        FindObjectOfType<PlayerControls>().transform.position = playerStartingPosition;
    }

    void Update()
    {

    }
}
