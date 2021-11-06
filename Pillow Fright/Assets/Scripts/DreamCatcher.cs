using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DreamCatcher : MonoBehaviour
{
    [SerializeField] int dreamsLeft;
    [SerializeField] bool allFound;
    GameObject[] DRs;
    public Text dreamsLeftText;

    // Start is called before the first frame update
    void Start()
    {
        DRs = GameObject.FindGameObjectsWithTag("Dream Catcher");
        dreamsLeft = DRs.Length;
        allFound = false;

        setText();
    }

    public void gotDreamCatcher()
    {
        //play sound
        dreamsLeft--;
        setText();

        if (dreamsLeft <= 0)
        {
            allFound = true;
        }
    }

    private void setText()
    {
        if (dreamsLeft > 0)
        {
            try
            {
                dreamsLeftText.text = "Find all Dream Catchers (Blue Orbs)!" +
                                      "\nCatchers Left: " + dreamsLeft;
            }
            catch (System.Exception)
            {
                Debug.Log("Dream Catcher Text not set");
            }
        }
        else
        {
            try
            {
                dreamsLeftText.text = "All Catchers Found!" +
                                      "\nGo back to bed!";
            }
            catch (System.Exception)
            {
                Debug.Log("Dream Catcher Text not set");
            }
        }
    }

    public bool isAllFound()
    {
        return allFound;
    }
}
