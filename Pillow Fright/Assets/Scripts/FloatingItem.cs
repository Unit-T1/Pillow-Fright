using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    public float floatDistance = 0.4f;  //Determines how far item floats
    public float floatSpeed = 0.2f;     //Determines speed of item bounce

    private Vector3 startPos;
    private Vector3 endPos;

    public PlayerControls player;

    void Start()
    {
        startPos = transform.position;
        endPos = transform.position + new Vector3(0, floatDistance);
    }

    private void Update()
    {
        //Time bounces between 0 and 1 at a rate of floatSpeed
        float time = Mathf.PingPong(Time.time * floatSpeed, 1);

        //Ease in out function
        //Found here: https://easings.net/#easeInOutSine
        transform.position = Vector3.Lerp(startPos, endPos, -(Mathf.Cos(Mathf.PI * time) - 1) / 2);
    }

    
    private void OnTriggerEnter2D(Collider2D col)
    {
        //Pillow Item
        if (name == "Pillow")
        {
            if (col.tag == "Player")
            {
                player.gotPillow();
                Destroy(this.gameObject);
            }
        }


    }

}
