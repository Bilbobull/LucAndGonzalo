using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SlowOrbitCam))]
public class OrbitCamWander : MonoBehaviour
{
    public float wanderTime;
    float wanderCountdown;

    SlowOrbitCam cam;
    // Use this for initialization
    void Start()
    {
        cam = GetComponent<SlowOrbitCam>();

        ChangeTarget();
    }

    // Update is called once per frame
    void Update()
    {
        wanderCountdown -= Time.deltaTime;
        if(wanderCountdown < 0.0f)
        {
            ChangeTarget();
        }
    }

    void ChangeTarget()
    {
        //  Debug.Log("Changig Camera Targets");
        cam.input = Random.insideUnitCircle;
        wanderCountdown = wanderTime;
    }
}
