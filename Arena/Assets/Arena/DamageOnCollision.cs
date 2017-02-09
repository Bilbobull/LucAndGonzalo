using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour {
    public string DamagingUnitTag = "Player";
    public int Damage = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //On collision damage the hit target 
    public void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == DamagingUnitTag)
        {
            Debug.Log(gameObject.name + " hit " + hit.gameObject.name + " for " + Damage);
            hit.gameObject.GetComponent<HealthSystem>().SubstractHealth(Damage);
        }
    }
}
