using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public GameObject m_playerStatus;
    public GameObject m_timeCountDown;
    
    private Text time;
    private Text hitNum;
    private Text Score;
    void Start()
    {
        time = gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        hitNum = gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
        Score = gameObject.transform.GetChild(2).gameObject.GetComponent<Text>();
    }

    private void Update()
    {
        setResults();
    }

    // Update is called once per frame
    public void setResults()
    {
        time.text = "Time Remained: " + m_timeCountDown.GetComponent<TimeCountdown>().getRemainTime();
        hitNum.text = "Hits Taken: " + m_playerStatus.GetComponent<PlayerStatus>().getHitNum();
        int score = m_timeCountDown.GetComponent<TimeCountdown>().getRemainTime() * 100 - m_playerStatus.GetComponent<PlayerStatus>().getScoreLoss();
        Score.text = "Final Score: " + score;
    }
}
