using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int PlayerNum;
    public float Speed;

	// Use this for initialization
	void Start ()
    {
        InputEvents.Movement.Subscribe(OnMovement, PlayerNum);
	}

    void OnMovement(InputEventInfo info)
    {
        Vector3 worldMovement = new Vector3(info.dualAxisValue.x, 0, info.dualAxisValue.y);
        transform.position += worldMovement * Speed * Time.deltaTime;
    }
}
