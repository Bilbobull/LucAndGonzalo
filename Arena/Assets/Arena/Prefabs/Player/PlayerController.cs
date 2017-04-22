using System.Collections;
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

    void Awake()
    {
        PlayerNum = FindPlayerNum();

    }

    void Start ()
    {
        // Get a new player number
        PlayerCount++;

        // Debug.Log("Player " + PlayerNum + " has joined!");

        // Subscribe to our movement events
        InputEvents.Movement.Subscribe(OnMovement, PlayerNum);
    }

    int FindPlayerNum()
    {
        List<PlayerController> allplayers = new List<PlayerController>(FindObjectsOfType<PlayerController>());
        // Find the first number that isnt taken
        int num;
        for (num = 0; num < allplayers.Count;)
        {
            // If we can find another player with this number, give up & keep looking
            if (allplayers.Exists((x) => {
                if (x == this)
                    return false;
                return x.PlayerNum == num;
            }))
            {
                ++num;
            }
            else
            {
                return num;
            }
        }
        return num; // Should be allplayers.Count
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
        // Tell CharacterMovementController to do the rest
        GetComponent<CharacterMovementController>().moveDir = movementVector;
    }
}
