using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public List<GameObject> tutorialUIs;
    public List<GameObject> enemies;

    private int currentPage = 0;

    public GameObject playerHead;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        //Invoke();
        //foreach (GameObject e in enemies)
        //{
        //    e.SetActive(false);
        //}
    }
    
    //private void disableEnemies()

    // Update is called once per frame
    void Update()
    {
        Vector3 targetLocation = new Vector3(this.transform.position.x,
            playerHead.transform.position.y - 0.5f, this.transform.position.z);
        this.transform.position = Vector3.SmoothDamp(this.transform.position, targetLocation, ref velocity, smoothTime);
    }

    public void nextPage()
    {
        tutorialUIs[currentPage].SetActive(false);
        currentPage += 1;
        tutorialUIs[currentPage].SetActive(true);
    }
    public void nextPageFirst()
    {
        tutorialUIs[currentPage].SetActive(false);
        currentPage += 1;
        tutorialUIs[currentPage].SetActive(true);
    }
    
    public void lastPage()
    {
        tutorialUIs[currentPage].SetActive(false);
        currentPage -= 1;
        tutorialUIs[currentPage].SetActive(true);
    }
    
    public void goToPage(int num)
    {
        tutorialUIs[currentPage].SetActive(false);
        currentPage = num;
        tutorialUIs[currentPage].SetActive(true);
    }

    public void closeWeaponPages()
    {
        for (int i = 1; i < 5; i++)
        {
            tutorialUIs[i].SetActive(false);
        }
    }
    public void openPage(int num)
    {
        closeWeaponPages();
        tutorialUIs[num].SetActive(true);
    }
}
