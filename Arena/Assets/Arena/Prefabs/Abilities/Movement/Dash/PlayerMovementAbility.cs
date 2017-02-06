using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAbility : BaseAbility
{
    [Tooltip("How much our speed increases while we use the boost")]
    public float SpeedBoostAmount;
    [Tooltip("The maximum time we can use the speed boost")]
    public float SpeedBoostTime;
    [Tooltip("How long until we can use the speed boost after a full depletion")]
    public float SpeedBoostRechargeTime;
    // What percent of our speed boost is avaliable
    private float SpeedBoostMeter = 1.0f;
    // Whether our speedboost is active
    private bool SpeedBoostActive = false;

    PlayerController player;

	// Use this for initialization
	void Start ()
    {
        player = GetPlayer();
        InputEvents.MovementAbility.Subscribe(OnMovementAbility, player.PlayerNum);
    }

    void OnMovementAbility(InputEventInfo info)
    {
        switch(info.inputState)
        {
            // TRIGGER
            case InputState.Triggered:
                if (SpeedBoostActive == false && SpeedBoostMeter >= 1.0f)
                    StartMovementAbility();
                    break;

            // RELEASE
            case InputState.Released:
                // Turn off the speed boost if it's active
                if(SpeedBoostActive == true)
                    EndMovementAbility();
                break;
        }
    }

    void Update()
    {
        if(SpeedBoostActive)
        {
            // Deplete our meter
            SpeedBoostMeter -= (Time.deltaTime / SpeedBoostTime);
            // Turn off the boost if we've used it all up
            if (SpeedBoostMeter <= 0.0f)
                EndMovementAbility();
        }
        else if(SpeedBoostMeter < 1.0f)
        {
            // Replenish our meter
            SpeedBoostMeter += (Time.deltaTime / SpeedBoostRechargeTime);
        }
    }

    void StartMovementAbility()
    {
        Debug.Log("Activating movement ability");
        Debug.Assert(SpeedBoostActive == false, "Tried to activate speed boost twice!");
        player.Speed *= SpeedBoostAmount;
        SpeedBoostActive = true;
    }

    void EndMovementAbility()
    {
        Debug.Log("Deactivating movement ability");
        Debug.Assert(SpeedBoostActive == true, "Tried to use deactivate boost twice!");
        player.Speed /= SpeedBoostAmount;
        SpeedBoostActive = false;
    }

    private void OnDestroy()
    {
        if (SpeedBoostActive)
            EndMovementAbility();
        InputEvents.MovementAbility.Unsubscribe(OnMovementAbility, player.PlayerNum);
    }
}
