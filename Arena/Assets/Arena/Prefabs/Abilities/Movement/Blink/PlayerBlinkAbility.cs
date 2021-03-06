﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlinkAbility : BaseAbility
{
    [Tooltip("How far we blink")]
    public float BlinkDistance;

    public GameObject BlinkEffect;

    PlayerController player;
    AbilityMeter meter;

	// Use this for initialization
	void Start ()
    {
        player = GetPlayer();
        meter = GetComponent<AbilityMeter>();
        InputEvents.MovementAbility.Subscribe(OnMovementAbility, player.PlayerNum);
    }

    void OnMovementAbility(InputEventInfo info)
    {
        switch (info.inputState)
        {
            // TRIGGER
            case InputState.Triggered:
                if (meter.IsFull)
                    StartMovementAbility();
                break;
        }

    }

    void StartMovementAbility()
    {
        if (BlinkEffect)
            Instantiate(BlinkEffect, transform.position, transform.rotation);
        player.transform.position += player.transform.forward * BlinkDistance;
        meter.Amount = 0.0f;
    }

    private void OnDestroy()
    {
        InputEvents.MovementAbility.Unsubscribe(OnMovementAbility, player.PlayerNum);
    }
}
