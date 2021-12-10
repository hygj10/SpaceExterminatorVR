using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    GameObject m_ParticleObject;

    private ParticleSystem m_Particle;
    private AudioSource audioData;
    private bool playing = false;
    // Start is called before the first frame update
    void Start()
    {
       m_Particle = m_ParticleObject.GetComponent<ParticleSystem>();
       audioData = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(audioData.time > 2.8f){
            audioData.Stop();
        }
    }
    void OnCollisionEnter(Collision other){
    
        //if the collided object is the object with a name of ball
        if (!playing)
        {
            if (other.gameObject.name != "Launcher_Handle")
            {
                audioData.Play(0);
            }
            
            Debug.Log("Collided with");
            Debug.Log(other.collider.name);
            Debug.Log(this.gameObject.name);
            m_Particle.Play();
            this.gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;
            this.gameObject.transform.GetChild(0).gameObject.GetComponent<Collider>().enabled = false;
            Destroy(this.gameObject, 1.2f);
        }
        
            //m_Particle.Stop();
        
        
        
    }
}


