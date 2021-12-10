using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class TimeCountdown : MonoBehaviour
{
    public float timeValue;
    public GameObject menuController;
    
    private Text timerText;

    private void Start()
    {
        timerText = gameObject.GetComponent<Text>();
    }

    void Update()
    {
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0;
        }
        
        DisplayTime(timeValue);
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay <= 0)
        {
            gameOver();
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public int getRemainTime()
    {
        return Mathf.FloorToInt(timeValue);
    }

    private void gameOver()
    {
        menuController.GetComponent<MenuController>().GameOver();
    }
}
