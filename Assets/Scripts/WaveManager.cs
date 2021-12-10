using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject menuController;
    
    private int waveCounter;

    private int totalWave;
    private bool timesup;
    // Start is called before the first frame update
    void Start()
    {
        waveCounter = 0;
        totalWave = gameObject.transform.childCount;
        timesup = false;
    }

    // Update is called once per frame

    public void NextWave()
    {
        if (!timesup)
        {
            gameObject.transform.GetChild(waveCounter).gameObject.SetActive(false);
            waveCounter += 1;
            if (waveCounter >= totalWave)
            {
                GameEnd();
            }
            else
            {
                Debug.Log(("Next wave") + waveCounter);
                gameObject.transform.GetChild(waveCounter).gameObject.SetActive(true);
                gameObject.transform.GetChild(waveCounter).gameObject.GetComponent<SingleWaveControll>().WaveStart();
            }
        }
    }

    private void GameEnd()
    {
        menuController.GetComponent<MenuController>().GameEnd();
    }

    public void timesUp()
    {
        timesup = true;
    }
}
