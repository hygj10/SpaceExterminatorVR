using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public string scene;
    public GameObject planetMenu;
    public void LoadScene()
    {
        if (scene == "")
        {   
            GetComponent<AudioSource>().Play();
            planetMenu.SetActive(!planetMenu.activeSelf);
        }
        else
        {
            SceneManager.LoadScene(scene);
        }
        
    }

    public void SimpleLoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ToggleMap()
    {
        planetMenu.SetActive(!planetMenu.activeSelf);
    }
}
