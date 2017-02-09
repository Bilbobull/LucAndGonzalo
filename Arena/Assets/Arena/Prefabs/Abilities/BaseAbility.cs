using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseAbility : MonoBehaviour
{
    // Each player can have only one of each ability type
    public enum AbilityType
    {
        Ranged, Melee, Movement
    };

    public AbilityType type;

    public string abilityName;
    protected PlayerController GetPlayer()
    {
        return GetComponentInParent<PlayerController>() as PlayerController;
    }
}
