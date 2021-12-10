using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class EnemyBulletBehavior : MonoBehaviour
{
    public GameObject m_leftHandController;
    public GameObject m_rightHandController;
    public GameObject m_player;
    public GameObject m_playerBody;

    private Vector3 targetPos;
    private float xrange;
    private float yrange;
    private float zrange;
    private Vector3 playerPos;
    private ActionBasedController xr_left;
    private ActionBasedController xr_right;
    //private AudioSource[] audioData;
    void Start()
    {   
        xrange = Random.Range(-0.25f, 0.25f);
        yrange = Random.Range(-0.3f, 0.3f);
        playerPos = m_player.transform.position;
        targetPos = playerPos + new Vector3(xrange, yrange ,-1f);
        xr_left = m_leftHandController.GetComponent<ActionBasedController>();
        xr_right = m_rightHandController.GetComponent<ActionBasedController>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Bodyzone")
        {
            // audioData = other.gameObject.GetComponents<AudioSource>();
            // if (!audioData[0].isPlaying)
            // {
            //     audioData[0].Play(0);
            // }
            m_playerBody.GetComponent<PlayerStatus>().playerGetHit(100);
            xr_left.SendHapticImpulse(0.7f, 0.6f);
            xr_right.SendHapticImpulse(0.7f, 0.6f);
            
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
        
        if (other.gameObject.name == "ShieldPlane")
        {
            if (m_leftHandController.GetComponent<XRRayInteractor>().selectTarget != null &&
                m_leftHandController.GetComponent<XRRayInteractor>().selectTarget.name == "Shield")
            {
                xr_left.SendHapticImpulse(0.4f, 0.1f);
            }
            else if (m_rightHandController.GetComponent<XRRayInteractor>().selectTarget != null &&
            m_rightHandController.GetComponent<XRRayInteractor>().selectTarget.name == "Shield")
            {
                xr_right.SendHapticImpulse(0.4f, 0.1f);
            }
            // xr_left.SendHapticImpulse(0.4f, 0.1f);
            // xr_right.SendHapticImpulse(0.4f, 0.1f);
            
            gameObject.SetActive(false);
        }
    }
}
