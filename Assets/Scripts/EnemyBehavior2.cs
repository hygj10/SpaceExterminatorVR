using System.Collections;
using System.Collections.Generic;
using Oculus.Platform.Samples.VrHoops;
using UnityEngine;

public class EnemyBehavior2 : MonoBehaviour
{   public GameObject m_player;
    public GameObject m_ParticleObject;
    public GameObject m_playerBody;
    public GameObject m_burningObject;
    public GameObject m_survivalController;
    
    private ParticleSystem m_Particle;
    private ParticleSystem burningEffect;

    public float m_enemySpeed;
    
    private Vector3 targetPos;
    private Vector3 playerPos;
    private Vector3 startPos;

    private Animator m_Animator;
    private AudioSource[] audioData;
    private AudioSource[] audioBody;
    private bool stop;
    private bool burned;
    private int bulletstaken;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = m_player.transform.position;
        targetPos = playerPos + new Vector3(0,0,-2);
        startPos = transform.position;
        m_Particle = m_ParticleObject.GetComponent<ParticleSystem>();
        m_Animator = GetComponent<Animator>();
        m_Animator.updateMode = AnimatorUpdateMode.UnscaledTime; 
        m_Animator.Play("Walk");
        audioData = gameObject.GetComponents<AudioSource>();
        stop = false;
        burned = false;
        burningEffect = m_burningObject.GetComponent<ParticleSystem>();
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerPos - Vector3.up);
        
        if (Vector3.Distance(transform.position, targetPos) > 0.1f && !stop)
        {
            
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * m_enemySpeed);
            
        }
        
        attackPlayer();
    }

    private void attackPlayer()
    {
        if (Vector3.Distance(transform.position, playerPos) < 3f)
        {
            if (!m_Particle.isPlaying)
            {
                audioData[1].Stop();
                m_playerBody.GetComponent<PlayerStatus>().playerGetHit(500);
                explosion();
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // audioData[0].Stop();
        // audioData[1].Play(0);
        if (other.gameObject.CompareTag("PlayerBullet") || 
            other.gameObject.CompareTag("EnemyExplosion"))
        {
            //gameObject.SetActive(false);
            explosion();
        }
        
        if(other.gameObject.name == "Bodyzone")
        {
            audioBody = other.gameObject.GetComponents<AudioSource>();
            if (!audioBody[0].isPlaying)
            {
                audioBody[0].Play(0);
            }
        }
        
        if (other.gameObject.CompareTag("Shield"))
        {
            //gameObject.SetActive(false);
            stunned();
        }
        
    }

    private void OnParticleCollision(GameObject other)
    {
        // audioData[0].Stop();
        // audioData[1].Play(0);
        if (other.gameObject.CompareTag("PlayerBullet") || 
            other.gameObject.CompareTag("EnemyExplosion"))
        {
            //gameObject.SetActive(false);
            explosion();
        }
        
        if (other.gameObject.CompareTag("Flame") && burned == false)
        {
            burned = true;
            m_burningObject.SetActive(true);
            StartCoroutine(explosionLateCall(3f));
        }
    }

    IEnumerator explosionLateCall(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        explosion();
    }

    public void resetPosition()
    {
        transform.position = startPos;
    }

    private void explosion()
    {   if (m_survivalController != null)
        {
            m_survivalController.GetComponent<EnemySpawn>().EnemyDied(); 
        }
        m_ParticleObject.SetActive(true);
        m_Particle.Play();
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            if (r.gameObject.name != "PlasmaExplosionEffect")
            {
                r.enabled = false;
            }
        }
        foreach (Collider c in GetComponentsInChildren<Collider>())
        {
            if (c.gameObject.name != "PlasmaExplosionEffect")
            {
                c.enabled = false;
            }
        }
        audioData[2].Play(0);
        m_burningObject.SetActive(false);
        StartCoroutine(LateCall(1.2f));
    }
    
    IEnumerator LateCall(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        m_ParticleObject.SetActive(false);
        audioData[2].Stop();
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            if (r.gameObject.name != "Shield")
            {
                r.enabled = true;
            }
        }
        foreach (Collider c in GetComponentsInChildren<Collider>())
        {
            c.enabled = true;
        }
        gameObject.SetActive(false);
    }
    
    public void stunned()
    {
        stop = true;
        audioData[0].Play(0);
        m_Animator.Play("Get_Hit");
        StartCoroutine(stunLateCall(5f));
    }
    
    IEnumerator stunLateCall(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        stop = false;
        //m_Animator.enabled = true;
    }
    
}
