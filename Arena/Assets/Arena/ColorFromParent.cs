using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorFromParent : MonoBehaviour
{

    Renderer GetParentRenderer()
    {
        // We only want our own renderer as a last resort 
        // Grumble grumble ...
        Renderer r;
        if (transform.parent)
            r = transform.parent.GetComponentInParent<Renderer>();
        else
            r = GetComponent<Renderer>();
        return r;
    }

	// Use this for initialization
	void Start ()
    {

        Renderer parentRend = GetParentRenderer();
        Renderer rend = GetComponentInChildren<Renderer>();

        if(parentRend && rend)
        {
            rend.material.SetColor("_Color", parentRend.material.color);
        }

        LineRenderer line = GetComponentInChildren<LineRenderer>();
        if (line && parentRend)
        {
            line.material.SetColor("_Color", parentRend.material.color);
        }
    }

    void SetAttacker(GameObject attacker)
    {
        Renderer parentRend = attacker.GetComponentInParent<Renderer>();
        Renderer rend = GetComponentInChildren<Renderer>();

        if (parentRend && rend)
        {
            rend.material.SetColor("_Color", parentRend.material.color);
        }

        LineRenderer line = GetComponentInChildren<LineRenderer>();
        if (line && parentRend)
        {
            line.material.SetColor("_Color", parentRend.material.color);
        }
    }

}
