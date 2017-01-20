using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int PlayerNum;
    public float Speed;

	// Use this for initialization
	void Start ()
    {
        InputEvents.Movement.Subscribe(OnMovement, PlayerNum);
        InputEvents.RangedAttack.Subscribe(OnRangedAttack, PlayerNum);
        InputEvents.MeleeAttack.Subscribe(OnMeleeAttack, PlayerNum);
        InputEvents.MovementAbility.Subscribe(OnMovementAbility, PlayerNum);
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

    void OnMovementAbility(InputEventInfo info)
    {
        switch (info.inputState)
        {
            // Increase speed while held
            case InputState.Triggered:
                Speed += 5.0f;
                break;

            // Return speed to normal when released
            case InputState.Released:
                Speed -= 5.0f;
                break;
        }
    }

    void OnMovement(InputEventInfo info)
    {
        Vector3 movementVector = new Vector3(info.dualAxisValue.x, 0, info.dualAxisValue.y);
        // Apply the inverse of the camera rotation and normalize so that it's screen relative
        movementVector = Camera.main.transform.InverseTransformDirection(movementVector);
        // Project onto the world plane (y = 0)
        movementVector.y = 0.0f;
        movementVector.Normalize();
        // Apply to position
        transform.position += movementVector * Speed * Time.deltaTime;
    }
}
