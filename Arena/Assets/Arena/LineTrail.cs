using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineTrail : MonoBehaviour {

    public int NumPoints;
    private LineRenderer line;

	// Use this for initialization
	void Start ()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = NumPoints;
        for(int i = 0; i < NumPoints; ++i)
        {
            line.SetPosition(i, transform.position);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Move all points down one spot
		for(int i = line.positionCount - 2; i >= 0; --i)
        {
            Vector3 temp = line.GetPosition(i);
            line.SetPosition(i+1, temp);
        }
        // Then replace the duplicate with our current position
        line.SetPosition(0, transform.position);
	}

    private void OnDestroy()
    {

    }
}
