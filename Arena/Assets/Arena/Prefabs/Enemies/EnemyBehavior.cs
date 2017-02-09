﻿using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour
{
    public float speed = 1.0f;
    public string playertag = "Player";
    public int zigzagradius = 2;

    public enum MovementType
    {
        WalkTowardsPlayer,
        WalkAwayFromPlayer,
        Wander,
        ZigZag
    };
    public MovementType movement;

    private GameObject ClosestPlayer;
    private float timer = 0;
    private Vector3 Direction;
    private float PassedTime = 0;
     

    private void ChangeState()
    {
        movement = (MovementType)Random.Range(1, 4);
        timer = Random.Range(1.0f, 3.0f);
        PassedTime = 0.0f;
    }
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        // Change to a random state
        if (timer <= 0.0f || !ClosestPlayer)
        {
            ClosestPlayer = FindClosestPlayer();
            ChangeState();
        }
        // Break if no target
        if (!ClosestPlayer)
            return;
        // Do movement
        switch (movement)
        {
            case MovementType.WalkTowardsPlayer:
                WalkTowardsClosestPlayer();
                break;

            case MovementType.WalkAwayFromPlayer:
                WalkAwayFromClosestPlayer();
                break;

            case MovementType.Wander:
                Wander();
                break;

            case MovementType.ZigZag:
                ZigZag();
                break;

            default:
                break;
        }
        Direction.Normalize();
        this.transform.position += Direction * speed * Time.deltaTime;
    }

    private GameObject FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float smallestDist = float.PositiveInfinity;
        foreach (GameObject p in players)
        {
            if (Vector3.Distance(p.transform.position, this.transform.position) < smallestDist)
                closest = p;
        }

        return closest;
    }

    private void WalkTowardsClosestPlayer()
    {
        Direction = ClosestPlayer.transform.position - this.transform.position;
    }

    private void WalkAwayFromClosestPlayer()
    {
        Direction = ClosestPlayer.transform.position - this.transform.position;
    }

    private void Wander()
    {
        Direction = new Vector3(Random.value, 0, Random.value);
    }

    private void ZigZag()
    {
        PassedTime += Time.deltaTime;   

        Direction = ClosestPlayer.transform.position - this.transform.position;

        Vector3 RightVec = new Vector3(-Direction.z, 0, Direction.x) * (zigzagradius* Mathf.Cos(PassedTime));

        Direction += RightVec;
    }

}