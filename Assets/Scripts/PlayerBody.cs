using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    public GameObject head;
    //public Vector3 headtrans;

    // Start is called before the first frame update
    void Start()
    {
        FollowHead();
    }

    // Update is called once per frame
    void Update()
    {
        FollowHead();
    }

    void FollowHead()
    {
        Vector3 location = head.transform.position;
        Vector3 forward = Vector3.ProjectOnPlane(head.transform.forward, Vector3.up);
        this.transform.position = new Vector3(location.x, location.y - 0.5f, location.z);
        if (gameObject.name == "DamageEffect")
        {
            transform.rotation = Quaternion.LookRotation(forward);
        }
        else
        {
            this.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(forward), Time.deltaTime);
        }
//        Debug.Log(forward);
    }
}
