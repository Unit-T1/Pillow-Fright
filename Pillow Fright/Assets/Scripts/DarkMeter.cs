using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkMeter : MonoBehaviour
{
    public float maxValue = 100f;       //Maximum meter value
    public float rate = 0.05f;          //rate at which the meter fills up (per frame)
    public bool inSafeZone = false;     //Checks whether player is in a safezone
    public float safeRatio = 1.5f;      //adjusts recovery speed factor
    public bool isPaused = false;       //pauses the meter if necessary
    public float lerpDuration = 1f;     //Determines the duration of the lerp animation (in sec)

    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();

        slider.value = 0;
        slider.maxValue = maxValue;
    }

    void Update()
    {
        //Debug Checks
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //addMeter(20);
            StartCoroutine(addMeter2(20));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(addMeter2(-20));
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            resetMeter();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            toggleSafeZone();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            pauseMeter();
        }
    }

    void FixedUpdate()
    {
        if (!isPaused)
        {
            //constantly fills or empties meter depending on the rate and safezone check
            if (!inSafeZone)
                Mathf.Clamp(slider.value += rate, 0, maxValue);
            else
                Mathf.Clamp(slider.value -= (rate * safeRatio), 0, maxValue); 
        }

        //Slider Max Check
        if (slider.value >= maxValue && !FindObjectOfType<PlayerControls>().isInvulnerable)
        {
            //Game Over?
            //taking damange when dark meter is filled
            FindObjectOfType<PlayerControls>().takeDamage();
            //Debug.Log("Meter at Max!");
        }

    }

    public bool isMeterFilled()
    {
        if (slider.value >= maxValue)
            return true;
        return false;
    }


    void resetMeter()
    {
        StartCoroutine(addMeter2(-maxValue));
    }

    void addMeter(float value)
    {
        Mathf.Clamp(slider.value += value, 0, maxValue);
    }

    //Same as above except with an "ease out" function
    //Functions found here: https://www.febucci.com/2018/08/easing-functions/
    IEnumerator addMeter2(float value)
    {
        bool lerping = false;
        if (!lerping)
        {
            float start = slider.value;
            float end = Mathf.Clamp(slider.value + value, 0, maxValue);
            float timeElapsed = 0f;

            while (slider.value != end)
            {
                lerping = true;
                if (timeElapsed < lerpDuration)
                {
                    slider.value = Mathf.Lerp(start, end, (1 - (Mathf.Pow(1 - (timeElapsed / lerpDuration), 2))));  //time uses an ease out function
                    timeElapsed += Time.deltaTime;
                    yield return null;
                }
                else
                {
                    slider.value = end;
                }
            }
            lerping = false;
            slider.value = end;
        }
    }

    void pauseMeter()
    {
        isPaused = !isPaused;
    }

    void toggleSafeZone()
    {
        inSafeZone = !inSafeZone;
    }

    public void setSafeZone(bool safe) { inSafeZone = safe; }
    public bool getSafeZone() { return inSafeZone; }

}
