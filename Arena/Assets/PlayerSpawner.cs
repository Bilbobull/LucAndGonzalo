using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
        InputEvents.CreatePlayer.Subscribe(OnCreatePlayer);
	}

    private void OnCreatePlayer(InputEventInfo _inputEventInfo)
    {
        int count = GameObject.FindGameObjectsWithTag("Player").Length;
        if((count < 2) && player)
        {
            Debug.Log("Creating player!");
            Instantiate(player);
        }
        else
        {
            Debug.Log("Too many players :(");
        }
    }
}
