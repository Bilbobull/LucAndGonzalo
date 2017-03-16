using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceTrigger : MonoBehaviour
{
    public Vector3 Force;
    public ForceMode Mode;
    private void OnTriggerStay(Collider other)
    {
        Rigidbody body = other.GetComponent<Rigidbody>();
        if(body)
            body.AddForce(Force, Mode);
    }
}
