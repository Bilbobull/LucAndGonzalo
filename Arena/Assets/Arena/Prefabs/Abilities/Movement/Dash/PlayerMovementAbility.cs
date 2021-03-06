﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AbilityMeter))]
public class PlayerMovementAbility : BaseAbility
{
    [Tooltip("How much our speed increases while we use the boost")]
    public float SpeedBoostAmount;

    PlayerController player;
    CharacterMovementController controller;
    AbilityMeter meter;

	// Use this for initialization
	void Start ()
    {
        meter = GetComponent<AbilityMeter>();
        controller = GetComponentInParent<CharacterMovementController>();
        player = GetPlayer();
        if(player)
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
        if(controller)
        {
            Debug.Log("Running!");
            controller.maxSpeed *= SpeedBoostAmount;
        }
    }

    void EndMovementAbility()
    {
        meter.EndCharging();
        if(controller)
            controller.maxSpeed /= SpeedBoostAmount;
    }

    // AI Ability check hook
    public override bool AIShouldUseAbility(GameObject currentTarget)
    {
        if (meter.IsCharging || !meter.IsFull)
            return false;
        return base.AIShouldUseAbility(currentTarget);
    }

    // AI Attack routine
    public override IEnumerator AIAttackRoutine(GameObject target)
    {
        // Start running 
        StartMovementAbility();
        // Wait until we're out of charge or our player dies
        yield return new WaitUntil(() => (meter.IsCharging && meter.IsEmpty));
        // Stop running
        EndMovementAbility();
        Debug.Log("Done Running!");

        yield break;
    }

    private void OnDestroy()
    { 
        if (meter.IsCharging)
            EndMovementAbility();
        if(player)
            InputEvents.MovementAbility.Unsubscribe(OnMovementAbility, player.PlayerNum);
    }
}
