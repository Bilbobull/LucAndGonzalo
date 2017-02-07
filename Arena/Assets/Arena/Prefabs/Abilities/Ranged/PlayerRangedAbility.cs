using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AbilityMeter))]
public class PlayerRangedAbility : BaseAbility
{
    public GameObject RangedAttackPrefab;
    public PlayerController player;
    private AbilityMeter meter;

    float SpeedMult = -0.1f;

    // Use this for initialization
    void Start ()
    {
        player = GetPlayer();
        meter = GetComponent<AbilityMeter>();
        InputEvents.RangedAttack.Subscribe(OnRangedAttack, player.PlayerNum);
    }

    void OnRangedAttack(InputEventInfo info)
    {
        switch (info.inputState)
        {
            case InputState.Triggered:
                StartCharging();
                break;

            case InputState.Released:
                EndCharging();
                break;
        }
    }

    void StartCharging()
    {
        // Turn off regular movement here
        InputEvents.Movement.Subscribe(OnChargeMovement, player.PlayerNum);
        // Apply our speed reduction
        player.Speed *= SpeedMult;
        meter.StartCharging();
    }

    void OnChargeMovement(InputEventInfo info)
    {
        // Face movement direction
        Vector3 lookDir = new Vector3(info.dualAxisValue.x, 0, info.dualAxisValue.y);
        // Apply the inverse of the camera rotation and normalize so that it's screen relative
        lookDir = Camera.main.transform.TransformDirection(lookDir);
        // Project onto the world plane (y = 0)
        lookDir.y = 0.0f;
        lookDir.Normalize();
        transform.LookAt(transform.position + lookDir, Vector3.up);
    }

    void EndCharging()
    {
        InputEvents.Movement.Unsubscribe(OnChargeMovement, player.PlayerNum);
        player.Speed /= SpeedMult;
        float charge = meter.EndCharging();
        if (charge > 0.0f)
            DoAttack(charge);
    }

    void DoAttack(float charge)
    {
        // Create our attack projectile
        // Debug.Log("Creating Ranged attack!");
        // Create the attack as a child of the player in local space
        GameObject proj = Instantiate(RangedAttackPrefab, transform.position, transform.rotation);
        proj.SendMessage("ChargeValue", charge, SendMessageOptions.DontRequireReceiver);
        proj.SendMessage("SetAttacker", GetComponentInParent<PlayerController>(), SendMessageOptions.DontRequireReceiver);
    }

    void OnRemoveAbility()
    {
        InputEvents.Movement.Unsubscribe(OnChargeMovement, player.PlayerNum);
        InputEvents.RangedAttack.Unsubscribe(OnRangedAttack, player.PlayerNum);
    }
}
