using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonAction : MonoBehaviour {

    public string message;

    // Use this for initialization
    void Start()
    {
        InputEvents.MenuConfirm.Subscribe(OnStartButton);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnStartButton(InputEventInfo info)
    {
        gameObject.SendMessage(message, SendMessageOptions.DontRequireReceiver);
    }

    void OnDestroy()
    {
        InputEvents.MenuConfirm.Unsubscribe(OnStartButton);
    }
}
