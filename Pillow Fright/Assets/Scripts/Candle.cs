using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
            GameObject.Find("Darkness Meter").GetComponent<DarkMeter>().inSafeZone = true;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player")
            GameObject.Find("Darkness Meter").GetComponent<DarkMeter>().inSafeZone = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
            GameObject.Find("Darkness Meter").GetComponent<DarkMeter>().inSafeZone = false;
    }
}
