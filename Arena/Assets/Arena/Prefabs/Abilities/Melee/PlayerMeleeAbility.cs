using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AbilityMeter))]
public class PlayerMeleeAbility : BaseAbility
{
    [Tooltip("The attack prefab we spawn on attack")]
    public GameObject AttackPrefab;


    PlayerController player;
    AbilityMeter meter;

    // Use this for initialization
    void Start ()
    {
        player = GetPlayer();
        meter = GetComponent<AbilityMeter>();
        InputEvents.MeleeAttack.Subscribe(OnMeleeAbility, player.PlayerNum);
	}
	
    void OnMeleeAbility(InputEventInfo info)
    {
        switch(info.inputState)
        {
            case InputState.Triggered:
                if(meter.IsFull)
                    CreateAttack();
                break;
        }
    }

    void CreateAttack()
    {
        Debug.Log("Creating Melee attack!");
        // Create the attack as a child of the player in local space
        Instantiate(AttackPrefab, transform.parent, false);
        meter.Ammount = 0.0f;
    }

    private void OnDestroy()
    {
        InputEvents.MeleeAttack.Unsubscribe(OnMeleeAbility, player.PlayerNum);
    }

}
