using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlinkAbility : BaseAbility
{
    [Tooltip("How far we blink")]
    public float BlinkDistance;
    [Tooltip("How long until we can use the blink")]
    public float SpeedBoostRechargeTime;
    // What percent of our speed boost is avaliable
    private float SpeedBoostMeter = 1.0f;

    PlayerController player;

	// Use this for initialization
	void Start ()
    {
        player = GetPlayer();
        InputEvents.MovementAbility.Subscribe(OnMovementAbility, player.PlayerNum);
    }

    void OnMovementAbility(InputEventInfo info)
    {
        switch (info.inputState)
        {
            // TRIGGER
            case InputState.Triggered:
                if (SpeedBoostMeter >= 1.0f)
                    StartMovementAbility();
                break;
        }

    }

    void Update()
    {
        if(SpeedBoostMeter < 1.0f)
        {
            // Replenish our meter
            SpeedBoostMeter += (Time.deltaTime / SpeedBoostRechargeTime);
        }
    }

    void StartMovementAbility()
    {
        player.transform.position += player.transform.forward * BlinkDistance;
        SpeedBoostMeter = 0.0f;
    }

    private void OnDestroy()
    {
        InputEvents.MovementAbility.Unsubscribe(OnMovementAbility, player.PlayerNum);
    }
}
