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
        if(player)
            InputEvents.MeleeAttack.Subscribe(OnMeleeAbility, player.PlayerNum);
	}
	
    void OnMeleeAbility(InputEventInfo info)
    {
        switch(info.inputState)
        {
            case InputState.Triggered:
                if(meter.IsFull)
                    CreateAttack("Enemy");
                break;
        }
    }

    public GameObject CreateAttack(string target)
    {
        GameObject attack;
        // Create the attack as a child of the player in local space
        attack = Instantiate(AttackPrefab, transform.parent, false);
        meter.Ammount = 0.0f;
        // If we're attacking somthing, set the damage type if it has some
        attack.GetComponentInChildren<DamageOnCollision>().DamagingUnitTag = target;

        return attack;
    }

    private void OnDestroy()
    {
        if(player)
            InputEvents.MeleeAttack.Unsubscribe(OnMeleeAbility, player.PlayerNum);
    }

    public override bool AIShouldUseAbility(GameObject currentTarget)
    {
        if (!meter.IsFull) return false;
        return base.AIShouldUseAbility(currentTarget);
    }

    // AI hooks
    public override IEnumerator AIAttackRoutine(GameObject target)
    {
        CreateAttack(target.tag);
        yield break;
    }

}
