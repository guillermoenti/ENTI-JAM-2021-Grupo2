using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD_Manager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] TextMeshProUGUI meters;

    public float timeRemaining = 100;
    private bool timerIsRunning;

    private float metersRunned = 0000;

    // Start is called before the first frame update
    void Start()
    {
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
                metersRunned++;
                DisplayMeters(metersRunned);
            }
            else
            {
                Debug.Log("Time finished");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void DisplayMeters(float actualMeters)
    {
        meters.text = string.Format("{0:0000}", Mathf.FloorToInt(actualMeters / 60));
    }

    void AddTime(float timeAddition) 
    {
        timeRemaining += timeAddition;
    }

    void SubtractTime(float timeSubtraction)
    {
        timeRemaining -= timeSubtraction;
    }
}
