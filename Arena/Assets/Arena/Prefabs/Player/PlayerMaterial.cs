using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaterial : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        // Set our Material based on our player number
        Renderer rend = GetComponentInChildren<Renderer>();
        PlayerController player = GetComponentInParent<PlayerController>();
        if (player)
            rend.material.SetColor("_Color", player.PlayerMaterials[player.PlayerNum]);

        // Hack for line renderer
        LineRenderer line = GetComponentInChildren<LineRenderer>();
        if (line && player)
        {
            line.material.SetColor("_Color", player.PlayerMaterials[player.PlayerNum]);
        }
    }
	
	void SetAttacker (PlayerController player)
    {
        // Set our Material based on our player number
        Renderer rend = GetComponentInChildren<Renderer>();
        rend.material.SetColor("_Color", player.PlayerMaterials[player.PlayerNum]);

        // Hack for line renderer
        LineRenderer line = GetComponentInChildren<LineRenderer>();
        if (line && player)
        {
            line.material.SetColor("_Color", player.PlayerMaterials[player.PlayerNum]);
        }
    }
}
