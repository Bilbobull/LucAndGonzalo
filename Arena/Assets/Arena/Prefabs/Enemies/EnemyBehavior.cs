using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour
{
    public float TurnSpeed;
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

    public GameObject ClosestPlayer;
    private float timer = 0;
    private Vector3 MoveDirection;
    private Vector3 LookDirection;
    private float PassedTime = 0;
     
    private void ChangeState()
    {
        timer = Random.Range(1.0f, 5.0f);
        PassedTime = 0.0f;
        movement = (MovementType)Random.Range(1, 4);
    }
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        // Change to a random state
        //if (timer <= 0.0f)
        //{
        //    ChangeState();
        //}
        ClosestPlayer = FindClosestPlayer();
        // Break if no target
        if (ClosestPlayer == null)
        {
            // Debug.Log("No Player!");
            FindClosestPlayer();
            return;
        }
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
        MoveDirection.Normalize();
        Quaternion target = Quaternion.LookRotation(LookDirection, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, TurnSpeed * Time.deltaTime);

        transform.position += MoveDirection * speed * Time.deltaTime;
    }

    private GameObject FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float smallestDist = float.PositiveInfinity;
        foreach (GameObject p in players)
        {
            float dist = Vector3.Distance(p.transform.position, transform.position);
            if (dist < smallestDist)
            {
                closest = p;
                smallestDist = dist;
            }
        }

        return closest;
    }

    private void WalkTowardsClosestPlayer()
    {
        MoveDirection = ClosestPlayer.transform.position - transform.position;
        LookDirection = MoveDirection;
    }

    private void WalkAwayFromClosestPlayer()
    {
        MoveDirection = -(ClosestPlayer.transform.position - transform.position);
        LookDirection = -MoveDirection;
    }

    private void Wander()
    {
        MoveDirection = new Vector3(Random.value, 0, Random.value);
        LookDirection = MoveDirection;
    }

    private void ZigZag()
    {
        PassedTime += Time.deltaTime;   

        MoveDirection = ClosestPlayer.transform.position - this.transform.position;

        Vector3 RightVec = new Vector3(-MoveDirection.z, 0, MoveDirection.x) * (zigzagradius* Mathf.Cos(PassedTime));

        MoveDirection += RightVec;
    }

}
