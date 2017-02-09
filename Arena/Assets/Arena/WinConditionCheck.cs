using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinConditionCheck : MonoBehaviour {

    public GameObject WinScreen;

	// Use this for initialization
	void Start ()
    {
        Events.GameLose.Subscribe(OnGameWin);

    }

    void OnGameWin()
    {
        Debug.Log("You Win!");
        Instantiate(WinScreen);
        Destroy(this);
    }

    private void OnDestroy()
    {
        Events.GameLose.Unsubscribe(OnGameWin);
    }
}
