using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{
    private Animator animator;
    private float grip;
    private float trigger;
    private float gripCurrent;
    private float triggerCurrent;
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();
    }

    public void Grip(float f)
    {
        grip = f;
    }

    public void Trigger(float f)
    {
        trigger = f;
    }

    void AnimateHand()
    {
        if (grip != gripCurrent)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, grip, Time.deltaTime * speed);
            animator.SetFloat("Grip", gripCurrent);
        }

        if (trigger != triggerCurrent)
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, trigger, Time.deltaTime * speed);
            animator.SetFloat("Trigger", triggerCurrent);
        }
    }
}
