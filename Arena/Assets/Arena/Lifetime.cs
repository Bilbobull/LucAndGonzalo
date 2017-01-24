using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : MonoBehaviour {

    public float Remaining;

	
	// Update is called once per frame
	void Update ()
    {
        Remaining -= Time.deltaTime;
        if (Remaining < 0.0f)
            Destroy(gameObject);
    }
}
