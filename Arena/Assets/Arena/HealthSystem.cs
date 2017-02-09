using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour {

    public int MaxHealth = 5;
    private int Health = 0;

	// Use this for initialization
	void Start () {
        Health = MaxHealth;
	}
	
    public void SubstractHealth(int hp)
    {
        Health -= hp;
        if (Health <= 0)
            Destroy(this.gameObject);
    }

}
