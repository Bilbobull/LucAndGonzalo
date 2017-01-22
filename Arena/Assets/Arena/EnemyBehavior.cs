using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
    public float speed = 1.0f;
    public string playertag = "Player";
    private GameObject[] players;
    private GameObject ClosestPlayer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        players = GameObject.FindGameObjectsWithTag(playertag);
        ClosestPlayer = FindClosestPlayer();

        Vector3 direction = ClosestPlayer.transform.position - this.transform.position;
        direction.Normalize();
        this.transform.position += direction * speed;

	}

    private GameObject FindClosestPlayer()
    {
        GameObject closest = null;
        float smallestDist = float.PositiveInfinity;
        foreach(GameObject p in players)
        {
            if (Vector3.Distance(p.transform.position, this.transform.position) < smallestDist)
                closest = p;
        }

        return closest;
    }
}
