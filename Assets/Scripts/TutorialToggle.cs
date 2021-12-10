using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialToggle : MonoBehaviour
{
    // Start is called before the first frame update
    public InputActionReference toggleReference = null;
    
    // Start is called before the first frame update
    void Awake()
    {
        toggleReference.action.started += Toggle;
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        toggleReference.action.started -= Toggle;
    }

    private void Toggle(InputAction.CallbackContext context)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
