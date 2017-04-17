using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelector : MonoBehaviour {

    public float speed;

    public MenuButton Target;
    Vector3 targetPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Target)
        {
            targetPos = Target.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
		
	}
}
