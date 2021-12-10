using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
[RequireComponent(typeof(ActionBasedController))]
public class WeaponBehavior : MonoBehaviour
{
    public GameObject AttachPoint;
    public GameObject ShootingPoint;
    private GameObject lastTouched;
    private ActionBasedController controller;
    public GameObject m_Bullet;
    private bool released = true;
    private bool gun_released = true;
    XRGrabInteractable m_GrabInteractable;
    bool m_Held;
    //private string m_Hand;
    private AudioSource audioData;
    [SerializeField]
    GameObject ParticleObject;
    ParticleSystem m_Particle;
    ParticleSystem m_Particle2;
    private float coolDownPeriod = 3.0f;
    private float timeStamp;
    
    
    private void Awake()
    {
        audioData = ShootingPoint.GetComponent<AudioSource>();
        controller = GetComponent<ActionBasedController>();
        m_GrabInteractable = AttachPoint.GetComponent<XRGrabInteractable>();
        if (ParticleObject != null)
        {
            m_Particle = ParticleObject.transform.GetChild(0).GetComponent<ParticleSystem>();
            m_Particle2 = ParticleObject.transform.GetChild(1).GetComponent<ParticleSystem>();
        }
        
        if (m_Particle != null)
        {
            m_Particle.Stop();
        }

        if (m_Particle2 != null)
        {
            m_Particle2.Stop();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            
            XRBaseInteractable m_child = GetComponent<XRRayInteractor>().selectTarget;
            if (controller.activateAction.action.ReadValue<float>() > 0.0 
                && m_Held && released)
            {
                Debug.Log(m_child.name);
                if (m_child != null && 
                    m_child.name == "Sci_Fi_Pistol_#1_prefab")
                {
                    m_Particle.Play();
                    Debug.Log(m_Particle.IsAlive());
                    controller.SendHapticImpulse(0.7f, 1f);
                    if (!audioData.isPlaying)
                    {
                        audioData.time = 23.9f;
                        audioData.Play(0);
                    }
                    if (!audioData.isPlaying || audioData.time > 25.0)
                    {
                        audioData.Stop();
                    }
                    m_Particle2.Play();
                    
                }
                
                // else if (m_child != null && 
                //     m_child.name == "Launcher")
                // {
                //     audioData.Play(0);
                //     if (timeStamp <= Time.time)
                //     {
                //         Rigidbody instantiatedProjectile = Shoot();
                //         instantiatedProjectile.velocity = ShootingPoint.transform.TransformDirection(new Vector3(0, 0, 10));
                //         timeStamp = Time.time + coolDownPeriod;
                //         // call indicator function
                //         AttachPoint.GetComponent<GrenadeLauncherCooldown>().ResetCoolTime();
                //     }
                // }
                //
                // else if (m_child != null && 
                //          m_child.name == "Weapon1")
                // {
                //     audioData.Play(0);
                //     Rigidbody instantiatedProjectile = Shoot();
                //     instantiatedProjectile.velocity = ShootingPoint.transform.TransformDirection(new Vector3(0, 0,80));
                // }
                
                else 
                {
                    released = false;
                    
                    controller.SendHapticImpulse(0.5f, 0.1f);
                    
                    if (m_child != null && 
                        m_child.name == "Launcher" &&
                        m_child.name == AttachPoint.name)
                    {
                        Debug.Log((m_child.name + " " + AttachPoint.name));
                        audioData.Play(0);
                        if (timeStamp <= Time.time)
                        {
                            Rigidbody instantiatedProjectile = Shoot();
                            instantiatedProjectile.velocity = ShootingPoint.transform.TransformDirection(new Vector3(0, 0, 10));
                            timeStamp = Time.time + coolDownPeriod;
                            // call indicator function
                            AttachPoint.GetComponent<GrenadeLauncherCooldown>().ResetCoolTime();
                        }
                    }
                    
                    // else if (m_child != null && 
                    //     m_child.name == "Weapon1")
                    else if (m_child != null && 
                             m_child.name == "Weapon1" &&
                             m_child.name == AttachPoint.name)
                    {   
                        Debug.Log((m_child.name + " " + AttachPoint.name));
                        audioData.Play(0);
                        Rigidbody instantiatedProjectile = Shoot();
                        instantiatedProjectile.velocity = ShootingPoint.transform.TransformDirection(new Vector3(0, 0,80));
                    }
                }
            }
            
            else if (!released && controller.activateAction.action.ReadValue<float>() == 0)
            {
                released = true;
            }
            
            else if (controller.activateAction.action.ReadValue<float>() == 0)
            {
                if (m_Particle != null)
                {
                    m_Particle.Stop();
                }
                if (m_Particle2 != null)
                {
                    m_Particle2.Stop();
                }
            }
        
    }

    private Rigidbody Shoot()
    {
        Rigidbody projectile = m_Bullet.GetComponent<Rigidbody>();
        Rigidbody instantiatedProjectile = Instantiate(projectile,
                ShootingPoint.transform.position,
                m_Bullet.transform.rotation)
            as Rigidbody;
        return instantiatedProjectile;
    }
    
    protected void OnEnable()
    {
        
        m_GrabInteractable.selectEntered.AddListener(OnSelectEntered);
        m_GrabInteractable.selectExited.AddListener(OnSelectExited);
    }

    protected void OnDisable()
    {
        m_GrabInteractable.selectEntered.RemoveListener(OnSelectEntered);
        m_GrabInteractable.selectExited.RemoveListener(OnSelectExited);
    }

    protected virtual void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("held");
        m_Held = true;
    }

    protected virtual void OnSelectExited(SelectExitEventArgs args)
    {
        Debug.Log("released");
        m_Held = false;
        //m_Hand = "";
    }
    
    // void OnTriggerStay(Collider other)
    // {
    //     GameObject m_child = AttachPoint.transform.GetChild(0).gameObject;
    //     if (other.gameObject == m_child)
    //     {
    //         m_Hand = gameObject.name;
    //         
    //     }
    //     else if (other.gameObject != lastTouched)
    //     {
    //         m_Hand = "";
    //     }
    //
    //     lastTouched = other.gameObject;
    // }

    // void OnTriggerExit(Collider other)
    // {
    //     GameObject m_child = AttachPoint.transform.GetChild(0).gameObject;
    //     if (m_child.name == "Sci_fi_Pistol")
    //     {
    //         audioData.Stop();
    //     }
    //     if (other.gameObject == m_child)
    //     {
    //         if (m_Particle != null)
    //         {
    //             m_Particle.Stop();
    //         }
    //         if (m_Particle2 != null)
    //         {
    //             m_Particle2.Stop();
    //         }
    //     }
    // }
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.name == "Cube1")
        {
            other.gameObject.SetActive(false);
        }
    }
    
}
