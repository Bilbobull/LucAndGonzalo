using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    public PlayerController player;
    public float hpSpacing;
    Text text;

    public GameObject heartObj;
    public List<GameObject> hearts = new List<GameObject>();
    private HealthSystem health;

	// Use this for initialization
	void Start ()
    {
        text = GetComponent<Text>();
        health = player.GetComponent<HealthSystem>();
        for(int i = 0; i < health.MaxHealth; ++i)
        {
            GameObject temp = Instantiate(heartObj,transform);
            hearts.Add(temp);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(!player)
        {
            text.text = "Press Start To Join";
            for (int i = 0; i < hearts.Count; ++i)
              hearts[i].SetActive(false);
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

            int hp = health.GetHealth();
            
            for (int i = 0; i < health.MaxHealth; ++i)
            {
                if (i < hp)
                    hearts[i].SetActive(true);
                else
                    hearts[i].SetActive(false);
            }
        }
	}
}
