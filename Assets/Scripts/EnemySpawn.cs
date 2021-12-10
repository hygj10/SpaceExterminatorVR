using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    public GameObject rhino_Prefab;
    public GameObject crab_Prefab;
    public GameObject spaceship_Prefab;
    public GameObject survivalTime;
    public GameObject HP;
    public GameObject playerStatus;
    public GameObject Scoreboard;
    
    public float SpawnTime;
    private float timeValue;
    private float displayTime;
    private Text survivaltimeText;
    private Text HPText;
    private bool gameover;

    private int kills;
    // Start is called before the first frame update
    void Start()
    {
        timeValue = 8;
        displayTime = 0;
        kills = 0;
        survivaltimeText = survivalTime.GetComponent<Text>();
        HPText = HP.GetComponent<Text>();
        gameover = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeValue += Time.deltaTime;
        displayTime += Time.deltaTime;
        if (!gameover)
        {
            updateStatus();
            if (Mathf.FloorToInt(timeValue % 60) == 0)
            {
                SpawnTime -= 1;
            }
            
            if (Mathf.FloorToInt(timeValue % SpawnTime) == 0)
            {
                timeValue += 1;
                SpawnEnemies();
            }
        }
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < Mathf.FloorToInt(Random.Range(0, 4)); i++)
        {
            Instantiate(rhino_Prefab,
                transform.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)),
                Quaternion.identity).SetActive(true);
        }

        for (int k = 0; k < Mathf.FloorToInt(Random.Range(0, 15)); k++)
        {
            Instantiate(crab_Prefab,
                transform.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)),
                Quaternion.identity).SetActive(true);
        }

        for (int j = 0; j < Mathf.FloorToInt(Random.Range(0, 3)); j++)
        {
            Instantiate(spaceship_Prefab,
                    transform.position + new Vector3(Random.Range(-10, 10), 4, Random.Range(-10, 10)),
                    Quaternion.identity).SetActive(true);
        }
    }

    private void updateStatus()
    {
        int hp = playerStatus.GetComponent<PlayerStatus>().getHP();
        if (hp <= 0)
        {
            hp = 0;
            Gameover();
        }

        survivaltimeText.text = "Survival time: " + Mathf.FloorToInt(displayTime);
        HPText.text = "HP: " + hp;
    }

    private void Gameover()
    {
        gameover = true;
        Scoreboard.SetActive(true);
        Scoreboard.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Time: " + Mathf.FloorToInt(displayTime);
        Scoreboard.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Kills: " + kills;
        Scoreboard.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Score: " + (kills+Mathf.FloorToInt(displayTime))*100;
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject obj in allObjects) {
            Destroy(obj);
        }
        
    }

    public void EnemyDied()
    {
        kills += 1;
    }


}
