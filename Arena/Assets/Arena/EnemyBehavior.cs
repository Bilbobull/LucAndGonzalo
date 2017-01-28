using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour
{
    public float speed = 1.0f;
    public string playertag = "Player";
    public int zigzagradius = 2;
    private GameObject[] players;
    private GameObject ClosestPlayer;
    private float timer = 0;
    private int MovementType = 0;
    private Vector3 Direction;
    private float PassedTime = 0;
     

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0.0f || !ClosestPlayer)
        {
            players = GameObject.FindGameObjectsWithTag(playertag);
            ClosestPlayer = FindClosestPlayer();
            MovementType = Random.Range(1, 4);
            timer = Random.Range(1.0f, 3.0f);
            PassedTime = 0.0f;
        }

        switch (MovementType)
        {
            case 1:
                WalkTowardsClosestPlayer();
                break;

            case 2:
                WalkAwayFromClosestPlayer();
                break;

            case 3:
                Wander();
                break;

            case 4:
                ZigZag();
                break;

            default:
                break;
        }

        Direction.Normalize();
        this.transform.position += Direction * speed;

        timer -= Time.deltaTime;
    }

    private GameObject FindClosestPlayer()
    {
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
