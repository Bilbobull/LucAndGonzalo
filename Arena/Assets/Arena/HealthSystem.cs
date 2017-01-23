using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour {

    public int MaxHealth = 5;
    public int Damage = 1;
    private int Health = 0;

	// Use this for initialization
	void Start () {
        Health = MaxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionEnter(Collision hit)
    {
        if ((this.name == "Enemy" && hit.gameObject.tag == "Player") || (this.tag == "Player" && hit.gameObject.name == "Enemy"))
        {
            Health -= Damage;
            if (Health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

}
