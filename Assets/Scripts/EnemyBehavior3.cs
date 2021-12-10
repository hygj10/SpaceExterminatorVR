using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System.Text;
using Oculus.Platform.Samples.VrHoops;
using UnityEngine;

public class EnemyBehavior3 : MonoBehaviour
{   public GameObject m_player;
    public GameObject m_ParticleObject;
    public GameObject m_playerBody;
    public GameObject m_burningObject;
    public GameObject m_survivalController;
    private ParticleSystem burningEffect;
    private ParticleSystem m_Particle;

    public float m_enemySpeed;
    
    private Vector3 targetPos;
    private Vector3 playerPos;
    private Vector3 startPos;

    private Animator m_Animator;
    private AudioSource[] audioData;
    private AudioSource[] audioBody;

    private bool stop;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = m_player.transform.position;
        targetPos = playerPos + new Vector3(0,-0.5f,0);
        startPos = transform.position;
        m_Particle = m_ParticleObject.GetComponent<ParticleSystem>();
        m_Animator = GetComponent<Animator>();
        m_Animator.updateMode = AnimatorUpdateMode.UnscaledTime; 
        m_Animator.Play("Armature|Walk_Cycle_1");
        audioData = gameObject.GetComponents<AudioSource>();
        stop = false;
        burningEffect = m_burningObject.GetComponent<ParticleSystem>();
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(targetPos);
        

        if (Vector3.Distance(transform.position, targetPos) > 1.5f && !stop)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * m_enemySpeed);
            
        }
        
        attackPlayer();
    }

    private void attackPlayer()
    {
        if (Vector3.Distance(transform.position, playerPos) >= 1.8f)
        {
            stop = false;
            m_Animator.Play("Armature|Walk_Cycle_1");
        }

        if (Vector3.Distance(transform.position, playerPos) < 1.8f)
        {
            stop = true;
            int randomNumber = Random.Range(1, 5);
            if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Armature|Walk_Cycle_1" ) || 
                0.9f <= m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
            {
                m_Animator.Play("Armature|Attack_" + randomNumber);
                if (randomNumber == 1)
                {
                    audioData[1].PlayDelayed(0.22f);
                    StartCoroutine(AttackLateCall(0.22f));
                }
                else if (randomNumber == 4)
                {
                    audioData[1].PlayDelayed(0.15f);
                    StartCoroutine(AttackLateCall(0.15f));
                }
                else
                {
                    audioData[1].PlayDelayed(0.18f);
                    StartCoroutine(AttackLateCall(0.18f));
                }
                
                
            }
        }
    }
    
    IEnumerator AttackLateCall(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        m_playerBody.GetComponent<PlayerStatus>().playerGetHit(100);
    }
    
    

    private void OnCollisionEnter(Collision other)
    {
        // audioData[0].Stop();
        // audioData[1].Play(0);
        if (other.gameObject.CompareTag("PlayerBullet") || 
            other.gameObject.CompareTag("EnemyExplosion") ||
            other.gameObject.CompareTag("Shield"))
        {
            //other.gameObject.GetComponent<Renderer>().enabled = false;
            audioData[2].Play();
            foreach (Renderer r in GetComponentsInChildren<Renderer>())
            {
                r.enabled = false;
            }
            foreach (Collider c in GetComponentsInChildren<Collider>())
            {
                c.enabled = false;
            }
            
            StartCoroutine(LateCall(0.6f));
        }
        
        // if(other.gameObject.name == "Bodyzone")
        // {
        //     audioBody = other.gameObject.GetComponents<AudioSource>();
        //     if (!audioBody[0].isPlaying)
        //     {
        //         audioBody[0].Play(0);
        //     }
        // }
        //
        // if (other.gameObject.CompareTag("Shield"))
        // {
        //     stunned();
        // }
    }

    private void OnParticleCollision(GameObject other)
    {
        // audioData[0].Stop();
        // audioData[1].Play(0);
        if (other.gameObject.CompareTag("PlayerBullet") || 
            other.gameObject.CompareTag("EnemyExplosion"))
        {
            explosion();
        }
        
        if (other.gameObject.CompareTag("Flame"))
        {   
            m_burningObject.SetActive(true);
            //burningEffect.Play();
            StartCoroutine(explosionLateCall(2f));
        }
    }

    IEnumerator explosionLateCall(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        explosion();
    }

    private void explosion()
    {   if (m_survivalController != null)
        {
            m_survivalController.GetComponent<EnemySpawn>().EnemyDied(); 
        }
        audioData[2].Play();
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = false;
        }
        foreach (Collider c in GetComponentsInChildren<Collider>())
        {
            c.enabled = false;
        }
        m_burningObject.SetActive(false);
        StartCoroutine(LateCall(0.6f));
    }

    IEnumerator LateCall(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        audioData[2].Stop();
        
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        { 
            r.enabled = true;
        }
        foreach (Collider c in GetComponentsInChildren<Collider>())
        {
            c.enabled = true;
        }
        gameObject.SetActive(false);
    }

    public void resetPosition()
    {
        transform.position = startPos;
        stop = false;
        m_Animator.Play("Armature|Walk_Cycle_1");
    }

    private void stunned()
    {
        stop = true;
        //m_Animator.enabled = false;
        int randomNumber = Random.Range(1, 4);
        m_Animator.Play("Armature|Take_Damage_" + randomNumber);
        StartCoroutine(stunLateCall(2f));
    }
    
    IEnumerator stunLateCall(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        stop = false;
        m_Animator.Play("Armature|Walk_Cycle_1");
        //m_Animator.enabled = true;
    }

}


