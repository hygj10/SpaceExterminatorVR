using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetButton : MonoBehaviour
{
    public GameObject planet;

    public Text planetNameText;

    public Text actionText;
    public Button actionButton;

    public GameObject sceneController;
    
    public string levelName;
    public string buttonText;

    private bool isHovered = false;

    public bool isPressed = false;

    private Vector3 originalScale;
    // Start is called before the first frame update
    void Start()
    {
        originalScale = planet.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hover()
    {
        if (!isHovered && !isPressed)
        {
            planet.transform.localScale = originalScale * 1.2f;
            planetNameText.enabled = true;
            isHovered = true;
            GetComponent<AudioSource>().Play();
        }
    }
    
    public void End()
    {
        if (isHovered && !isPressed)
        {
            planet.transform.localScale = originalScale;
            planetNameText.enabled = false;
            isHovered = false;
        }
    }
    
    public void End2()
    {
        planet.transform.localScale = originalScale;
        planetNameText.enabled = false;
        isHovered = false;
    }

    public void Pressed()
    {
        sceneController.GetComponent<SceneController>().scene = levelName;
        
        // shrink all other planets
        GameObject[] planets = GameObject.FindGameObjectsWithTag("PlanetMenu");
        foreach (GameObject p in planets)
        {
            p.GetComponent<PlanetButton>().End2();
            p.GetComponent<PlanetButton>().isPressed = false;
        }
        
        // enlarge self
        isPressed = true;
        planet.transform.localScale = originalScale * 1.6f;
        planetNameText.enabled = true;
        
        actionButton.enabled = true;
        actionText.text = buttonText;
        
    }
}
