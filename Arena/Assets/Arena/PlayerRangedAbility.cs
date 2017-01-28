using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedAbility : BaseAbility
{
    public GameObject RangedAttackPrefab;
    public PlayerController player;
    [Range(0,1)]
    public float MinCharge;
    public float RangedAbilityChargeTime;
    private float RangedAbilityMeter;

    bool Charging;
    float SpeedMult = -0.1f;

    // Use this for initialization
    void Start ()
    {
        player = GetPlayer();
        InputEvents.RangedAttack.Subscribe(OnRangedAttack, player.PlayerNum);
    }

    // Update is called once per frame
    void Update ()
    {
		if(Charging)
        {
            RangedAbilityMeter += Time.deltaTime / RangedAbilityChargeTime;
            RangedAbilityMeter = Mathf.Clamp01(RangedAbilityMeter);
        }
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
                if (RangedAbilityMeter > MinCharge)
                    DoAttack();
                break;
        }
    }

    void StartCharging()
    {
        // Turn off movement here
        InputEvents.Movement.Subscribe(OnChargeMovement, player.PlayerNum);
        Charging = true;
        RangedAbilityMeter = 0.0f;
        player.Speed *= SpeedMult;
    }

    void OnChargeMovement(InputEventInfo info)
    {
        // Face movement direction
        Vector3 lookDir = new Vector3(info.dualAxisValue.x, 0, info.dualAxisValue.y);
        // Apply the inverse of the camera rotation and normalize so that it's screen relative
        lookDir = Camera.main.transform.InverseTransformDirection(lookDir);
        // Project onto the world plane (y = 0)
        lookDir.y = 0.0f;
        lookDir.Normalize();
        transform.LookAt(transform.position + lookDir, Vector3.up);
    }

    void EndCharging()
    {
        InputEvents.Movement.Unsubscribe(OnChargeMovement, player.PlayerNum);
        Charging = false;
        player.Speed /= SpeedMult;
    }

    void DoAttack()
    {
        // Create our attack projectile
        // Debug.Log("Creating Ranged attack!");
        // Create the attack as a child of the player in local space
        GameObject proj = Instantiate(RangedAttackPrefab, transform.position, transform.rotation);
        proj.SendMessage("ChargeValue", RangedAbilityMeter, SendMessageOptions.DontRequireReceiver);
    }

    void OnRemoveAbility()
    {
        InputEvents.Movement.Unsubscribe(OnChargeMovement, player.PlayerNum);
        InputEvents.RangedAttack.Unsubscribe(OnRangedAttack, player.PlayerNum);
    }
}
