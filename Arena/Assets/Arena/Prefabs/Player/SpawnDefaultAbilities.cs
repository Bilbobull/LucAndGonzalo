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
        if(DefaultMovementAbility)
            AddAbility(gameObject, DefaultMovementAbility);
        if (DefaultRangedAbility)
            AddAbility(gameObject, DefaultRangedAbility);
        if (DefaultMeleeAbility)
            AddAbility(gameObject, DefaultMeleeAbility);
        Destroy(this);
	}
}
