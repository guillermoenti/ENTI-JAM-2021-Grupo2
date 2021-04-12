using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using System.IO;

public class HUD_Manager : MonoBehaviour
{
    //public static HUD_Manager HInstance { get; private set; }

    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] TextMeshProUGUI meters;

    private void Awake()
    {
        /*if(HInstance = null)
        {
            HInstance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.Log("Warning: multiple" + this + " in scene");
        }*/

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GInstance.GetTimerIsRunning() && GameManager.GInstance.timeRemaining > 0)
        {
            DisplayTime(GameManager.GInstance.timeRemaining);
            DisplayMeters(GameManager.GInstance.metersRunned);
        }
    }

    void DisplayTime(float _timeToDisplay)
    {
        _timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(_timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(_timeToDisplay % 60);

        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void DisplayMeters(int _actualMeters)
    {
        meters.text = string.Format("{0:0000}", Mathf.FloorToInt(_actualMeters / 60));
    }

    void AddTime(float _timeAddition) 
    {
        GameManager.GInstance.timeRemaining += _timeAddition;
    }

    void SubtractTime(float _timeSubtraction)
    {
        GameManager.GInstance.timeRemaining -= _timeSubtraction;
    }



}
