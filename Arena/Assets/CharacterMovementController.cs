using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour {

    // How fast the player moves at max speed (the y axis of the accelerationCurve
    [Tooltip("How fast the player moves at max speed.")]
    public float maxSpeed;

    // How fast the player moves at max speed (the t axis of the accelerationCurve
    [Tooltip("How long until the player moves at max speed.")]
    public float accelerationTime;

    public float currentSpeed
    { get; private set; }

    public float gravity;

    // How the player accelerates
    public AnimationCurve accelerationCurve;

    // which way you want to move
    public Vector3 moveDir;
    // How tall the character is
    public float height;
    // How fat the character is
    public float radius;

    [Header("Advanced")]
    public LayerMask collisionMask;

    public float currentSteepness
    { get; private set; }

    public bool isFalling
    { get; private set; }

    [Range(0, 90)]
    public float maxSteepness;

    private float accelerationPercent;
    private Vector3 groundNormal;
    private Vector3 groundPosition;
    
   

	// Use this for initialization
	void Start ()
    {

    }

    private void FixedUpdate()
    {
        bool isAccelerating = moveDir.sqrMagnitude >= Mathf.Epsilon;
        // Increase acceleration
        if(isAccelerating)
            accelerationPercent += (Time.fixedDeltaTime / accelerationTime);
        else
            accelerationPercent = 0.0f;
        // Clamp if necessary
        accelerationPercent =  Mathf.Clamp01(accelerationPercent);
        // Compute velocity
        currentSpeed = maxSpeed * accelerationCurve.Evaluate(accelerationPercent);
        
        // Raycast and find our ground point
        CastDown();

        if (isFalling)
            ApplyGravity();
        else
        {
            SnapToGround();
        }
        

        ApplyMovement();
    }

    private void SnapToGround()
    {
        transform.position = groundPosition + Vector3.up * height;
    }

    private void CastDown()
    {
        // Compute ground contact information
        Ray down = new Ray(transform.position, Vector3.down);
        RaycastHit hitInfo;
        bool castHit = Physics.Raycast(down, out hitInfo, height + float.Epsilon);
        // Update ground info
        if (castHit)
        {
            groundNormal = hitInfo.normal;
            groundPosition = hitInfo.point;
        }
        else
        {
            groundNormal = Vector3.up;
            groundPosition = hitInfo.point;
        }
        // Set current steepness
        currentSteepness = castHit ? Vector3.Angle(Vector3.up, hitInfo.normal) : 0.0f ;
        // Falling is the opposite of landing on the ground
        isFalling = !(castHit && currentSteepness < maxSteepness);
    }


    private void ApplyMovement()
    {
        // Normalize if necessary
        moveDir = Vector3.ClampMagnitude(moveDir, 1.0f);
        // Flatten movement to plane surface, preserving magnitude
        float mag = moveDir.magnitude;
        moveDir = Vector3.ProjectOnPlane(moveDir, groundNormal);
        moveDir = moveDir.normalized * mag;

        if(CheckPosition(moveDir, mag * currentSpeed * Time.fixedDeltaTime))
        {
            // Translate in that direction
            transform.position += moveDir * currentSpeed * Time.fixedDeltaTime;
        }
    }

    private bool CheckPosition(Vector3 moveDir, float dist)
    {
        // Just spherecast at it
        Ray movement = new Ray(transform.position, moveDir);
        bool hit = Physics.SphereCast(movement, radius, dist, collisionMask);
        return !hit;
    }

    private void ApplyGravity()
    {
        transform.position += Vector3.down * gravity * Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        // Draw our raycast
        Gizmos.DrawRay(transform.position, Vector3.down);
        // Draw our ground contact
        Gizmos.DrawWireSphere(groundPosition, 2.0f);
        // Draw our ground normal
        Gizmos.DrawRay(groundPosition, groundNormal);
    }



}
