using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Color[] PlayerMaterials;

    public int PlayerNum
    {   get; private set;   }
    static public int PlayerCount
    { get; private set; }
    public float Speed;
    public float TurnSpeed;
	// Use this for initialization
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
    }

    void OnRangedAttack(InputEventInfo info)
    {
        switch(info.inputState)
        {
            case InputState.Triggered:
                Debug.Log("Starting ranged attack charge");
                break;

            case InputState.Active:
                Debug.Log("Ranged attack charging!");
                break;

            case InputState.Released:
                Debug.Log("Releasing ranged attack");
                break;
        }
    }

    void OnMeleeAttack(InputEventInfo info)
    {
        switch (info.inputState)
        {
            case InputState.Triggered:
                Debug.Log("Starting meleee attack charge");
                break;

            case InputState.Active:
                Debug.Log("Melee attack charging!");
                break;

            case InputState.Released:
                Debug.Log("Releasing melee attack");
                break;
        }
    }


    void OnMovement(InputEventInfo info)
    {
        Vector3 movementVector = new Vector3(info.dualAxisValue.x, 0, info.dualAxisValue.y);
        // Apply the inverse of the camera rotation and normalize so that it's screen relative
        movementVector = Camera.main.transform.TransformDirection(movementVector);
        // Project onto the world plane (y = 0)
        movementVector.y = 0.0f;
        movementVector.Normalize();
        // Rotate towards our movement direction
        Quaternion target = Quaternion.LookRotation(movementVector, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, TurnSpeed * Time.deltaTime);
        // Apply to position
        transform.position += movementVector * Speed * Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward);
    }


}
