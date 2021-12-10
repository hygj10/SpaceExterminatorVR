using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponPos : MonoBehaviour
{
    public GameObject AttachPoint;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    private Quaternion deriv = Quaternion.identity;

    private bool JustReleased = false;
    // Start is called before the first frame update
    private void FixedUpdate()
    {   
        if (Shield_isSlow() && JustReleased && AttachPoint.transform != this.transform)
        {
            this.transform.position = Vector3.SmoothDamp(this.transform.position, AttachPoint.transform.position, ref velocity, smoothTime);
            this.transform.rotation =
                SmoothDamp(this.transform.rotation, AttachPoint.transform.rotation, ref deriv, smoothTime);
            // really close
            Vector3 diff = this.transform.position - AttachPoint.transform.position;
            if (diff.sqrMagnitude < .000001)
            {
                this.transform.position = AttachPoint.transform.position;
                this.transform.rotation = AttachPoint.transform.rotation;
                JustReleased = false;
            }
        }
    }

    private Boolean Shield_isSlow()
    {
        if (gameObject.name == "Shield")
        {
            return gameObject.GetComponent<Rigidbody>().velocity.magnitude < 1f;
        }

        return true;
    }

    public void WeaponReleased()
    {
        // if (gameObject.name == "Shield" && gameObject.GetComponent<Rigidbody>().velocity.magnitude >= 0.1f)
        if (gameObject.name == "Shield")
        {
            StartCoroutine(WaitForSeconds());
        }

        JustReleased = true;
    }
    
    private IEnumerator WaitForSeconds()
    {   
        yield return new WaitForSeconds(1.0f);
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void WeaponGrabbed()
    {
        JustReleased = false;
    }
    
    // helper code
    // https://gist.github.com/maxattack/4c7b4de00f5c1b95a33b
    public static Quaternion SmoothDamp(Quaternion rot, Quaternion target, ref Quaternion deriv, float time) {
        if (Time.deltaTime < Mathf.Epsilon) return rot;
        // account for double-cover
        var Dot = Quaternion.Dot(rot, target);
        var Multi = Dot > 0f ? 1f : -1f;
        target.x *= Multi;
        target.y *= Multi;
        target.z *= Multi;
        target.w *= Multi;
        // smooth damp (nlerp approx)
        var Result = new Vector4(
            Mathf.SmoothDamp(rot.x, target.x, ref deriv.x, time),
            Mathf.SmoothDamp(rot.y, target.y, ref deriv.y, time),
            Mathf.SmoothDamp(rot.z, target.z, ref deriv.z, time),
            Mathf.SmoothDamp(rot.w, target.w, ref deriv.w, time)
        ).normalized;
		
        // ensure deriv is tangent
        var derivError = Vector4.Project(new Vector4(deriv.x, deriv.y, deriv.z, deriv.w), Result);
        deriv.x -= derivError.x;
        deriv.y -= derivError.y;
        deriv.z -= derivError.z;
        deriv.w -= derivError.w;		
		
        return new Quaternion(Result.x, Result.y, Result.z, Result.w);
    }
}
