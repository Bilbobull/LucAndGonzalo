using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDefaultAbilities : MonoBehaviour {

    public BaseAbility DefaultMovementAbility;
    public BaseAbility DefaultRangedAbility;
    public BaseAbility DefaultMeleeAbility;


    void AddAbility(GameObject g, BaseAbility Ability)
    {
        Instantiate(Ability.gameObject, g.transform, false);
    }

    // Use this for initialization
    void Start () {
        AddAbility(gameObject, DefaultMovementAbility);
        AddAbility(gameObject, DefaultRangedAbility);
        AddAbility(gameObject, DefaultMeleeAbility);
        Destroy(this);
	}
}
