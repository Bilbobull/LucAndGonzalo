using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TextMesh))]
public class TextMeshGradient : MonoBehaviour
{
    public Gradient gradient;

    [Range(0, 3)]
    public float speed;

    private TextMesh text;
    private float t;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        t = (t + Time.deltaTime * speed) % 1.0f;
        text.color = gradient.Evaluate(t);
    }
}
