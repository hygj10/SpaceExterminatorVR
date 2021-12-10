using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyManagement : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    
    //private float timer;
    // Start is called before the first frame update
    // void Start()
    // {
    //     timer = 0;
    // }
    //
    // // Update is called once per frame
    // void Update()
    // {
    //     if(!enemy1.activeSelf)
    //     {
    //         timer += Time.deltaTime;
    //         if(timer > 2f)
    //         {
    //             timer = 0f;
    //             enemy1.SetActive(true);
    //             if (enemy1.name == "Rhino_PBR")
    //             {
    //                 enemy1.GetComponent<EnemyBehavior2>().resetPosition();   
    //             }
    //             if (enemy1.name == "CrabMonster")
    //             {
    //                 enemy1.GetComponent<EnemyBehavior3>().resetPosition();   
    //             }
    //         }
    //     }
    // }

    public void EnemyToggle1()
    {
        enemy1.SetActive(!enemy1.activeSelf);
        enemy1.GetComponent<EnemyBehavior>().resetPosition();
    }
    
    public void EnemyToggle2()
    {
        enemy2.SetActive(!enemy2.activeSelf);
        enemy2.GetComponent<EnemyBehavior2>().resetPosition();
    }
    
    public void EnemyToggle3()
    {
        enemy3.SetActive(!enemy3.activeSelf);
        enemy3.GetComponent<EnemyBehavior3>().resetPosition();
    }
}
