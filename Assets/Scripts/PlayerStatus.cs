using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public GameObject DamageEffect;
    public float EffectDuration;

    private AudioSource[] audioData;
    private bool ishit;
    private Material DamageMaterial;
    private float curMetallic;
    private int hitNum;
    private int scoreLoss;

    private int HP;
    void Start()
    {
        audioData = GetComponents<AudioSource>();
        ishit = false;
        DamageMaterial = DamageEffect.GetComponent<MeshRenderer>().materials[0];
        curMetallic = 1f;
        hitNum = 0;
        scoreLoss = 0;
        HP = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (ishit)
        {
            curMetallic = curMetallic - Mathf.Lerp(0, 1, Time.deltaTime / EffectDuration);
            DamageMaterial.SetFloat("_Metallic", curMetallic);
        }
    }

    public void playerGetHit(int Score)
    {
        hitNum += 1;
        scoreLoss += Score;
        HP -= Score / 20;
        if (!audioData[0].isPlaying)
        {
            audioData[0].Play(0);
        }

        if (!ishit)
        {
            ishit = true;
            DamageEffect.GetComponent<MeshRenderer>().enabled = true;
            StartCoroutine(LateCall(1f));
        }
    }
    
    IEnumerator LateCall(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ishit = false;
        DamageMaterial.SetFloat("_Metallic", 1f);
        curMetallic = 1f;
        DamageEffect.GetComponent<MeshRenderer>().enabled = false;
    }

    public int getScoreLoss()
    {
        return scoreLoss;
    }

    public int getHitNum()
    {
        return hitNum;
    }

    public int getHP()
    {
        return HP;
    }
}
