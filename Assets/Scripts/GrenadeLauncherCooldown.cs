using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncherCooldown : MonoBehaviour
{
    public Material alertMat;

    public Material cooledMat;

    public Material originalMat;

    private float currentTime = 5.0f;

    public GameObject block1;
    public GameObject block2;
    public GameObject block3;
    public GameObject block4;
    public GameObject block5;
    public GameObject block6;
    
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 6.0f;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = currentTime + Time.deltaTime;
        if (currentTime > 3.0f)
        {
            block1.GetComponent<MeshRenderer>().material = originalMat;
            block2.GetComponent<MeshRenderer>().material = originalMat;
            block3.GetComponent<MeshRenderer>().material = cooledMat;
            block4.GetComponent<MeshRenderer>().material = cooledMat;
            block5.GetComponent<MeshRenderer>().material = cooledMat;
            block6.GetComponent<MeshRenderer>().material = cooledMat;
        } else if (currentTime > 2.8f)
        {
            
            block3.GetComponent<MeshRenderer>().material = cooledMat;
            block4.GetComponent<MeshRenderer>().material = cooledMat;
        } 
        else if (currentTime > 2.0)
        {
            block3.GetComponent<MeshRenderer>().material = originalMat;
            block4.GetComponent<MeshRenderer>().material = originalMat;
            block5.GetComponent<MeshRenderer>().material = originalMat;
            block6.GetComponent<MeshRenderer>().material = originalMat;
        }
        else if (currentTime > 1.0)
        {
            block1.GetComponent<MeshRenderer>().material = originalMat;
            block2.GetComponent<MeshRenderer>().material = originalMat;
        }
        else
        {
            block1.GetComponent<MeshRenderer>().material = alertMat;
            block2.GetComponent<MeshRenderer>().material = alertMat;
            block3.GetComponent<MeshRenderer>().material = alertMat;
            block4.GetComponent<MeshRenderer>().material = alertMat;
            block5.GetComponent<MeshRenderer>().material = alertMat;
            block6.GetComponent<MeshRenderer>().material = alertMat;
        }
    }

    public void ResetCoolTime()
    {
        currentTime = 0;
    }
}
