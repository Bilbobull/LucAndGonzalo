using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour {

    public Gradient highlightColor;
    Color regularColor;

    public string selectMessage;
    public bool highlighted;
    float t; // Gradient progress

    // Use this for initialization
    void Start() {
        regularColor = GetComponent<TextMesh>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (highlighted)
        {
            GetComponent<TextMesh>().color = highlightColor.Evaluate(t);
            t =(t + Time.deltaTime) % 1.0f;
        }
    }

    public void Highlight()
    {
        highlighted = true;
        t = 0;

    }

    public void Unhighlight()
    {
        highlighted = false;
        GetComponent<TextMesh>().color = regularColor;
    }

    public void Select()
    {
        // Debug.Log(name);
        gameObject.SendMessage(selectMessage, SendMessageOptions.DontRequireReceiver);
    }
}
