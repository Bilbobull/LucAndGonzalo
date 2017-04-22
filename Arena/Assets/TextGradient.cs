using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextGradient : MonoBehaviour
{
    public Gradient gradient;

    [Range(0, 3)]
    public float speed;

    private Text text;
    private float t;

	// Use this for initialization
	void Start ()
    {
        text = GetComponent<Text>();	
	}
	
	// Update is called once per frame
	void Update () {
        t = (t + Time.deltaTime * speed) % 1.0f;
        text.color = gradient.Evaluate(t);
	}
}
