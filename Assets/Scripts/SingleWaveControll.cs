using System.Collections;
using System.Collections.Generic;
using CSCore.Streams;
using UnityEngine;

public class SingleWaveControll : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject WaveManager;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!EnemyExisted())
        {   
            Debug.Log(("Next Wave"));
            WaveManager.GetComponent<WaveManager>().NextWave();
        }
    }

    private bool EnemyExisted()
    {
        for (int i = 0; i< gameObject.transform.childCount; i++)
        {
            if(gameObject.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                return true;
            }
        }
        return false;
    }

    public void WaveStart()
    {
        for (int i = 0; i< gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
