using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : LevelAdministrator
{
    public Image[] hearts;
    public Sprite hasLife;
    public Sprite noLife;
     
    public int lives;
    public int numOfLives;

    void Update()
    {
        // Testing health bar
        if (Input.GetKeyDown(KeyCode.Return))
            LoseLife();
    }

    // Decrement live bar
    public void LoseLife()
    {
        // If we run out of lives we lose the game
        if (lives <= 1)
        {
            lives = numOfLives+1;   //reset heart back to max
            FindObjectOfType<PlayerControls>().isDead();
        }
        // Decrement life
        lives--;
        // if there are more lives then the limit, then set lives to the limit
        if (lives > numOfLives)
        {
            lives = numOfLives;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < lives)   // Add full heart sprite
            {
                hearts[i].sprite = hasLife;
            }
            else    // add empty heart sprite 
            {
                hearts[i].sprite = noLife;
            }

            if (i < numOfLives)  // if there are lives then show the heart sprite
            {
                hearts[i].enabled = true;
            }
            else // else empty the heart bar accordingly
            {
                hearts[i].enabled = false;
            }
        }
    }
}
