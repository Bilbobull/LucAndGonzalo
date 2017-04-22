using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonAction : MonoBehaviour {

    public string message;

	// Use this for initialization
	void Start () {
        InputEvents.MenuBack.Subscribe(OnBackButton);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnBackButton(InputEventInfo info)
    {
        gameObject.SendMessage(message, SendMessageOptions.DontRequireReceiver);
        Debug.Log(message);
    }

    void OnDestroy()
    {
        InputEvents.MenuBack.Unsubscribe(OnBackButton);
    }
}
