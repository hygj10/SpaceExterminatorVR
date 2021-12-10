using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class menuToggle : MonoBehaviour
{
    public InputActionReference toggleReference = null;

    public GameObject m_startButton;

    public GameObject m_RestartButton;

    public GameObject Scoreboard;

    public GameObject Gameover;
    
    // Start is called before the first frame update
    void Awake()
    {
        toggleReference.action.started += Toggle;
        Time.timeScale = 0;
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        toggleReference.action.started -= Toggle;
    }

    private void Toggle(InputAction.CallbackContext context)
    {   
        if (Scoreboard.activeSelf)
        {
            return;
        }

        if (Gameover != null && Gameover.activeSelf)
        {
            return;
        }

        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            gameObject.SetActive(true);
            Time.timeScale = 0;
            m_startButton.SetActive(false);
            m_RestartButton.SetActive(true);
        }
    }
}
