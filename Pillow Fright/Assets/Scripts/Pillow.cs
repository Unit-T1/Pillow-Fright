using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillow : MonoBehaviour
{
    public PlayerControls player;


    void Update()
    {
        //Pillow will slowly move up and down here
    }

    //Player obtains pillow
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            player.gotPillow();
            Destroy(this.gameObject);
        }
    }

}
