using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTargets : MonoBehaviour
{
    public GameObject[] targets;
    public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Compute the midpoint
        Vector3 Midpoint = new Vector3(0,0,0);
        foreach(GameObject g in targets)
        {
            Midpoint += g.transform.position;
        }
        Midpoint /= targets.Length;

        Vector3 TargetForward = (transform.position- Midpoint).normalized;
        transform.forward = Vector3.Slerp(transform.forward, TargetForward, Time.deltaTime * speed);
	}
}
