using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

    const int MaxPlayers = 2;
    public GameObject player;

    public PlayerUI[] PlayerUIs;

    // Use this for initialization
    void Start () {
        InputEvents.CreatePlayer.Subscribe(OnCreatePlayer);
	}

    private void OnCreatePlayer(InputEventInfo _inputEventInfo)
    {
        int count = GameObject.FindGameObjectsWithTag("Player").Length;
        if((count < MaxPlayers) && player)
        {
            Debug.Log("Creating player!");
            GameObject instance = Instantiate(player);

            PlayerController pc = instance.GetComponent<PlayerController>();
            if(PlayerUIs.Length > pc.PlayerNum)
            {
                PlayerUI ui = PlayerUIs[pc.PlayerNum];
                ui.SetPlayer(pc);
            }
        }
        else
        {
            Debug.Log("Too many players :(");
        }
    }
}
