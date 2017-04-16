using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowOrbitCam : MonoBehaviour
{
    public Transform Anchor;

    // How much input causes the camera to move
    public float Sensitivity;
    // How far away from the anchor is the camera
    public float Distance;
    public float SmoothTime;
    public float MaxSpeed;
    // As A percentage of Distance
    [Range(-1, 1)]
    public float MaxHeight;
    [Range(-1, 1)]
    public float MinHeight;

    public Vector2 input
        { set; private get; }

    Vector3 currentVelocity;
    Vector3 targetOffset;

	// Use this for initialization
	void Start ()
    {
        targetOffset = transform.position;
        InputEvents.CameraMovement.Subscribe(OnCameraMovement);
	}
	
    void OnCameraMovement(InputEventInfo info)
    {
        // Get their input as camera local up & right
        input = new Vector2(info.dualAxisValue.x, info.dualAxisValue.y);
    }

    void ApplyMovement()
    {
        // Transform into world space
        Vector3 localDir = transform.TransformDirection(new Vector3(input.x, input.y, 0));
        // move our target offset
        targetOffset += localDir * Sensitivity * Time.deltaTime;
        // Apply the height constraints
        targetOffset.y = Mathf.Clamp(targetOffset.y, Distance * MinHeight, Distance * MaxHeight);
        // Apply the distance constraint
        Vector3 offset = targetOffset - Anchor.position;
        offset = offset.normalized * Distance;
        targetOffset = Anchor.position + offset;
    }

	void FixedUpdate ()
    {
        // Apply any inputs
        ApplyMovement();
        // Update our rotation
        transform.LookAt(Anchor, Vector3.up);
        // Smoothdamp to target offset
        transform.position = Vector3.SmoothDamp(transform.position, targetOffset, ref currentVelocity, SmoothTime, MaxSpeed);
	}

    private Quaternion GetInputRotation()
    {
        throw new NotImplementedException();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, Anchor.position);
        Gizmos.DrawLine(targetOffset, Anchor.position);
        Gizmos.color = Color.green;
        Vector3 heightMax = transform.position;
        heightMax.y = Anchor.position.y + Distance * MaxHeight;
        Gizmos.DrawLine(transform.position, heightMax);
        Gizmos.color = Color.yellow;
        Vector3 heightMin = transform.position;
        heightMin.y = Anchor.position.y + Distance * MinHeight;
        Gizmos.DrawLine(transform.position, heightMin);

    }
}
