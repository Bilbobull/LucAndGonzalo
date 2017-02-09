using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    public string DamagingUnitTag = "Player";
    public int Damage = 1;

    void SetDamageTag(string newTag)
    {
        DamagingUnitTag = newTag;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == DamagingUnitTag)
        {
            // Debug.Log(gameObject.name + " hit " + other.gameObject.name + " for " + Damage);
            other.gameObject.GetComponent<HealthSystem>().SubstractHealth(Damage);
        }
    }


}
