using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPickup : MonoBehaviour {

    public BaseAbility Ability;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Remove the ability we are replacing
            RemoveAbilityType(other.gameObject, Ability.type);
            // Add our ability
            AddAbility(other.gameObject, Ability);
            // Destroy ourselves
            Destroy(gameObject);
        }
    }

    void AddAbility(GameObject g, BaseAbility Ability)
    {
        Instantiate(Ability.gameObject, g.transform, false);
    }

    void RemoveAbilityType(GameObject g, BaseAbility.AbilityType t)
    {
        // Get all abilities, and loop through to find the match
        BaseAbility[] components = g.GetComponentsInChildren<BaseAbility>();
        foreach (BaseAbility b in components)
        {
            if (b.type == t)
            {
                Debug.Log("Switching abilities: " + t);
                // Send a message incase the ability has to do stuff
                b.SendMessage("OnRemoveAbility", SendMessageOptions.DontRequireReceiver);
                Destroy(b.gameObject);
                return;
            }
        }
        // Uh oh, they didn't have one!
        Debug.LogError("Expected gameobject " + g.name + " to have ability of type " + t);
    }

}
