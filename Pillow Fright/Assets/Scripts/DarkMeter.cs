using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkMeter : MonoBehaviour
{
    public float maxValue = 100f;
    public float rate = 0.05f;          //rate at which the meter fills up (per frame)
    public bool inSafeZone = false;
    public float safeRatio = 1.5f;      //adjusts recovery speed factor
    public bool isPaused = false;
    private bool lerping = false;

    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();

        slider.value = 0;
        slider.maxValue = maxValue;
    }

    void Update()
    {
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
            if (!inSafeZone)
                Mathf.Clamp(slider.value += rate, 0, maxValue);
            else
                Mathf.Clamp(slider.value -= (rate * safeRatio), 0, maxValue); 
        }

        if (slider.value >= maxValue)
        {
            //Game Over?
            Debug.Log("Meter at Max!");
        }

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
        if (!lerping)
        {
            float lerpDuration = 1f;
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
