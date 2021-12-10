using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShield : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject rhino;
    public GameObject mesh;
    private AudioSource audioData;
    void Start()
    {   
        audioData = gameObject.GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {   
        if (other.gameObject.CompareTag("PlayerBullet") || 
            other.gameObject.CompareTag("EnemyExplosion"))
        {
            audioData.Play(0);
            mesh.GetComponent<MeshRenderer>().enabled = true;
            StartCoroutine(LateCall(0.2f));
        }
        
        if (other.gameObject.CompareTag("Shield"))
        {   
            mesh.GetComponent<MeshRenderer>().enabled = true;
            StartCoroutine(LateCall(0.2f));
            rhino.GetComponent<EnemyBehavior2>().stunned();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("PlayerBullet") || 
            other.gameObject.CompareTag("EnemyExplosion") ||
            other.gameObject.CompareTag("Flame"))
        {
            mesh.GetComponent<MeshRenderer>().enabled = true;
            StartCoroutine(LateCall(0.2f));
        }
    }
    
    IEnumerator LateCall(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        mesh.GetComponent<MeshRenderer>().enabled = false;
    }
}
