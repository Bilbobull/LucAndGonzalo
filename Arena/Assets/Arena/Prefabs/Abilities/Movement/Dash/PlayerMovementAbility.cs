using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AbilityMeter))]
public class PlayerMovementAbility : BaseAbility
{
    [Tooltip("How much our speed increases while we use the boost")]
    public float SpeedBoostAmount;

    PlayerController player;
    AbilityMeter meter;

	// Use this for initialization
	void Start ()
    {
        meter = GetComponent<AbilityMeter>();
        player = GetPlayer();
        InputEvents.MovementAbility.Subscribe(OnMovementAbility, player.PlayerNum);
    }

    void OnMovementAbility(InputEventInfo info)
    {
        switch(info.inputState)
        {
            // TRIGGER
            case InputState.Triggered:
                if (!meter.IsCharging && meter.IsFull)
                    StartMovementAbility();
                    break;

            // RELEASE
            case InputState.Released:
                // Turn off the speed boost if it's active
                if(meter.IsCharging)
                    EndMovementAbility();
                break;
        }
    }

    void Update()
    {
        // Turn off the boost if we've used it all up
        if (meter.IsCharging && meter.IsEmpty)
        {
            EndMovementAbility();
        }
    }

    void StartMovementAbility()
    {
        meter.StartCharging();
        player.Speed *= SpeedBoostAmount;
    }

    void EndMovementAbility()
    {
        meter.EndCharging();
        player.Speed /= SpeedBoostAmount;
    }

    private void OnDestroy()
    { 
        if (meter.IsCharging)
            EndMovementAbility();
        InputEvents.MovementAbility.Unsubscribe(OnMovementAbility, player.PlayerNum);
    }
}
