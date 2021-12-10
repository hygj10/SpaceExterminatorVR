using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTutorial : MonoBehaviour
{
    private LineRenderer line;

    public GameObject weapon;

    public GameObject hand;

    public GameObject weaponPos;
    public GameObject targetEnemy;
    public int weaponNum;
    //public GameObject[] threeBasicEnemies;
    public GameObject secondWave;
    public GameObject tutorialElement;
    private int step;

    private float cur_time;
    // Start is called before the first frame update
    void Start()
    {
        line = this.GetComponent<LineRenderer>();
        cur_time = 0;
    }

    private void OnEnable()
    {
        //GameObject[] allWeapons = GameObject.FindGameObjectsWithTag("WeaponPos");
        //foreach (GameObject w in allWeapons)
        //{
            //w.SetActive(false);
        //}
        if (weaponPos)
        {
            weaponPos.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        updateLine();
        cur_time += Time.deltaTime;
        if (this.isActiveAndEnabled && cur_time >= 5f)
        {
            if (step == 0)
            {
                /*if (weaponNum == 1)
                {
                    enemies.GetComponent<EnemyManagement>().EnemyToggle1();
                    targetEnemy.GetComponent<EnemyBehavior>().m_bulletrate = 0;
                } else if (weaponNum == 2)
                {
                    enemies.GetComponent<EnemyManagement>().EnemyToggle3();
                } else if (weaponNum == 3)
                {
                    enemies.GetComponent<EnemyManagement>().EnemyToggle2();
                } else
                {
                    enemies.GetComponent<EnemyManagement>().EnemyToggle1();
                    targetEnemy.GetComponent<EnemyBehavior>().m_bulletrate = 1;
                }*/
                targetEnemy.SetActive(true);
                step += 1;
            } 
            else if (step == 1)
            {
                /*if (weaponNum == 1 && !threeBasicEnemies[0].activeSelf)
                {
                    secondWave.SetActive(true);
                } else if (weaponNum == 2 && !threeBasicEnemies[2].activeSelf)
                {
                    secondWave.SetActive(true);
                } else if (weaponNum == 3 && !threeBasicEnemies[1].activeSelf)
                {
                    secondWave.SetActive(true);
                } else if (weaponNum == 4 && !threeBasicEnemies[0].activeSelf)
                {
                    secondWave.SetActive(true);
                }*/
                bool alldead = true;
                for (int i = 0; i< targetEnemy.transform.childCount; i++)
                {
                    if(targetEnemy.transform.GetChild(i).gameObject.activeInHierarchy)
                    {
                        alldead = false;
                    }
                }

                if (alldead)
                {
                    secondWave.SetActive(true);
                    step += 1;
                }
            }
            else if (step == 2)
            {
                bool alldead2 = true;
                for (int i = 0; i< secondWave.transform.childCount; i++)
                {
                    if(secondWave.transform.GetChild(i).gameObject.activeInHierarchy)
                    {
                        alldead2 = false;
                    }
                }

                if (alldead2)
                {
                    tutorialElement.GetComponent<TutorialController>().nextPage();
                    step += 1;
                }
            }
        }
    }

    private void updateLine()
    {
        if (line)
        {
            line.SetPosition(0, weapon.transform.position);
            line.SetPosition(1, hand.transform.position);
        }
    }
}
