using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject m_Pausemenu;
    public GameObject m_Scoreboard;
    public GameObject m_InGameTimer;
    public GameObject m_GameOver;
    public GameObject m_WaveManager;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void gameStart()
    {
        Time.timeScale = 1;
        m_Pausemenu.SetActive(false);
    }

    public void GameEnd()
    {
        m_Scoreboard.SetActive(true);
        m_InGameTimer.SetActive(false);
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject obj in allObjects) {
            Destroy(obj);
        }
    }

    public void GameOver()
    {   
        m_WaveManager.GetComponent<WaveManager>().timesUp();
        m_GameOver.SetActive(true);
        m_InGameTimer.SetActive(false);
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject obj in allObjects) {
            Destroy(obj);
        }
    }

}
