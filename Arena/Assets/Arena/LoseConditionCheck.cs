using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseConditionCheck : MonoBehaviour
{
    public GameObject LoseScreen;

    private void Start()
    {
        Events.GameLose.Subscribe(OnGameLose);
    }

    // Update is called once per frame
    void Update ()
    {
        // See if all players have no health
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 0)
            Events.GameLose.Send();
	}

    void OnGameLose()
    {
        Debug.Log("Game over!");
        Instantiate(LoseScreen);
        Destroy(this);
    }

    private void OnDestroy()
    {
        Events.GameLose.Unsubscribe(OnGameLose);
    }
}
