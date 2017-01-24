using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerMeleeAbility : MonoBehaviour
{
    [Tooltip("The attack prefab we spawn on attack")]
    public GameObject AttackPrefab;
    [Tooltip("How long until we can use the speed boost after a full depletion")]
    public float MeleeRechargeTime;
    // What percent of our speed boost is avaliable
    private float MeleeMeter = 1.0f;

    PlayerController player;

    // Use this for initialization
    void Start ()
    {
        player = GetComponent<PlayerController>();
        InputEvents.MeleeAttack.Subscribe(OnMeleeAbility, player.PlayerNum);
	}
	
    void OnMeleeAbility(InputEventInfo info)
    {
        switch(info.inputState)
        {
            case InputState.Triggered:
                if(MeleeMeter >= 1.0f)
                    CreateAttack();
                break;
        }
    }

	// Update is called once per frame
	void Update ()
    {
        if (MeleeMeter < 1.0f)
            MeleeMeter += Time.deltaTime / MeleeRechargeTime;
	}

    void CreateAttack()
    {
        Debug.Log("Creating Melee attack!");
        // Create the attack as a child of the player in local space
        Instantiate(AttackPrefab, transform, false);
        MeleeMeter = 0.0f;
    }

}
