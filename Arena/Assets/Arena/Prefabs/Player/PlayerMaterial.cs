using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaterial : MonoBehaviour
{
    public Color[] materials;
    public int materialIdx = 0;

    public Color color
    {
        get { return materials[materialIdx]; }
    }

    public bool hasColor
    {
        get { return (materials.Length > 0); }
    }

    void SetColorFromPlayer()
    {
        PlayerController player = GetComponentInParent<PlayerController>();
        if (player)
            materialIdx = player.PlayerNum;
    }

    // Use this for initialization
    void Start()
    {
        SetColorFromPlayer();

        // Give up if we don't have a color
        if (!hasColor) return;

        // Set our Material based on our player number
        Renderer rend = GetComponentInChildren<Renderer>();
        PlayerController player = GetComponentInParent<PlayerController>();
        if (player)
            rend.material.SetColor("_Color", color);

        // If we have any lines, set their colors too
        LineRenderer line = GetComponentInChildren<LineRenderer>();
        if (player && line)
        {
            line.material.SetColor("_Color", color);
        }
    }
}
