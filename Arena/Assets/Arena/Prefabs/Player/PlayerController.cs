﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int PlayerNum
    { get; private set; }

    static public int PlayerCount
    { get; private set; }

    /// DEPRECATED
    public float Speed;
    /// DEPRECATED
    public float TurnSpeed;

    void Start ()
    {
        // Get a new player number
        PlayerNum = PlayerCount;
        PlayerCount++;

        Debug.Log("Player " + PlayerNum + " has joined!");

        // Subscribe to our movement events
        InputEvents.Movement.Subscribe(OnMovement, PlayerNum);
    }

    private void OnDestroy()
    {
        PlayerCount--;
        InputEvents.Movement.Unsubscribe(OnMovement, PlayerNum);
    }

    void OnMovement(InputEventInfo info)
    {
        Vector3 movementVector = new Vector3(info.dualAxisValue.x, 0, info.dualAxisValue.y);
        // Apply the inverse of the camera rotation and normalize so that it's screen relative
        movementVector = Camera.main.transform.TransformDirection(movementVector);
        // Rotate towards our movement direction
        if(movementVector.sqrMagnitude > 0.01)
        {
            Quaternion target = Quaternion.LookRotation(movementVector, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, TurnSpeed * Time.deltaTime);
        }
        // Tell CharacterMovementController to do the rest
        GetComponent<CharacterMovementController>().moveDir = movementVector;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawRay(transform.position, transform.forward);
    //}


}
