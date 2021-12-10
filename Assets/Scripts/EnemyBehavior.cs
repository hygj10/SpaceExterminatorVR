using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;

public class EnemyBehavior: MonoBehaviour
{
    public float m_xspace;
    public float m_yspace;
    public float m_zspace;
    public float m_speed;

    public GameObject m_bullet;
    public GameObject m_bulletemitter;
    public float m_bulletrate;
    public GameObject m_player;
    public GameObject m_burningObject;
    public GameObject m_survivalController;
    // public GameObject m_leftHandController;
    // public GameObject m_rightHandController;
    
        
    private Vector3 targetPos;
    private Vector3 startPos;
    private Vector3 playerPos;
    private AudioSource[] audioData;
    private float targetX;
    private float targetY;
    private float targetZ;

    private ParticleSystem burningEffect;
    // private XRController lefthand;
    // private XRController righthand;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        targetPos = startPos;
        playerPos = m_player.transform.position;
        audioData = gameObject.GetComponents<AudioSource>();
        burningEffect = m_burningObject.GetComponent<ParticleSystem>();
        // lefthand = m_leftHandController.GetComponent<XRController>();
        // righthand = m_rightHandController.GetComponent<XRController>();
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {   
        transform.LookAt(playerPos + new Vector3(0,1f,0));
        
        if (Vector3.Distance(transform.position, targetPos) <= 0.1f)
        {
            getTargetPos();
            
            StartCoroutine (FireRoutine ());
        }
        
        if (Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * m_speed);
        }
        
    }

    private void getTargetPos()
    {
        targetX = startPos.x + Random.Range(-m_xspace, m_xspace);
        targetY = startPos.y + Random.Range(-m_yspace, m_yspace);
        targetZ = startPos.z + Random.Range(-m_zspace, m_zspace);
        targetPos = new Vector3(targetX, targetY, targetZ);
    }
    
    public void resetPosition()
    {
        transform.position = startPos;
    }
    
    private IEnumerator FireRoutine()
    {   
        yield return new WaitForSeconds(1.0f/m_bulletrate);
        
        audioData[0].Play(0);
        GameObject spawnedBullet = Instantiate(m_bullet, m_bulletemitter.transform.position, transform.rotation);
        spawnedBullet.transform.Rotate(90, 0, 0, Space.Self);
        spawnedBullet.SetActive(true);
        
        // spawnedBullet.GetComponent<EnemyBulletBehavior>().m_leftHandController = lefthand;
        // spawnedBullet.GetComponent<EnemyBulletBehavior>().m_rightHandController = righthand;
        //audioSource.PlayOneShot(audioClip);
        Destroy(spawnedBullet, 5);
        
    }

    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.CompareTag("PlayerBullet") || 
            other.gameObject.CompareTag("EnemyExplosion") ||
            other.gameObject.CompareTag("Shield"))
        {
          //  audioData[0].Stop();
            audioData[1].Play(0);
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
            StartCoroutine(LateCall(0.9f));
        }

        
    }

    private void OnParticleCollision(GameObject other)
    {
        
        if (other.gameObject.CompareTag("EnemyExplosion"))
        {
            explosion();
        }

        if (other.gameObject.CompareTag("Flame"))
        {   
            m_burningObject.SetActive(true);
            StartCoroutine(explosionLateCall(2f));
        }
    }

    IEnumerator explosionLateCall(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        explosion();
    }

    private void explosion()
    {
        audioData[1].Play(0);
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        m_burningObject.SetActive(false);
        if (m_survivalController != null)
        {
            m_survivalController.GetComponent<EnemySpawn>().EnemyDied(); 
        }
        StartCoroutine(LateCall(0.4f));
    }
    
    IEnumerator LateCall(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.gameObject.GetComponent<Renderer>().enabled = true;
        gameObject.GetComponent<Collider>().enabled = true;
        gameObject.SetActive(false);
    }
}
