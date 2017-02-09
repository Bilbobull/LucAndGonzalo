using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    public PlayerController player;

    Text text;

	// Use this for initialization
	void Start ()
    {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(!player)
        {
            text.text = "Press Start To Join";
        }
        else
        {
            BaseAbility[] currentAbilities = player.GetComponentsInChildren<BaseAbility>();
            string temp;
            temp = "Player " + (player.PlayerNum + 1) + '\n';
            foreach(BaseAbility ability in currentAbilities)
            {
                switch(ability.type)
                {
                    case BaseAbility.AbilityType.Melee:
                        temp += "(B): ";
                        break;
                    case BaseAbility.AbilityType.Ranged:
                        temp += "(X): ";
                        break;
                    case BaseAbility.AbilityType.Movement:
                        temp += "(A): ";
                        break;
                }
                temp += ability.abilityName + '\n';
            }
            text.text = temp;
        }
	}
}
