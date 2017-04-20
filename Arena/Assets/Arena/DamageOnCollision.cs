using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    public string DamagingUnitTag = "Player";
    public bool IgnoreDamaging = false;
    public bool DestroyAfterDamage = false;
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
        if (IgnoreDamaging || other.gameObject.tag == DamagingUnitTag)
        {
            HealthSystem hp = other.gameObject.GetComponent<HealthSystem>();
            // Debug.Log(gameObject.name + " hit " + other.gameObject.name + " for " + Damage);
            if(hp)
                hp.SubstractHealth(Damage);
            if (DestroyAfterDamage)
                Destroy(gameObject);
        }
    }


}
