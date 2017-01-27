using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {
    public float speed = 1.0f;
    public string playertag = "Player";
    private GameObject[] players;
    private GameObject ClosestPlayer;
    private float timer = 0;
    private int MovementType = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (timer <= 0.0f)
        {
            players = GameObject.FindGameObjectsWithTag(playertag);
            ClosestPlayer = FindClosestPlayer();
            MovementType = Random.Range(1, 1);

            switch (MovementType)
            {
                case 1:
                    timer = Random.Range(1.0f, 3.0f);
                    break;


                default:
                    break;
            }
        }

        switch (MovementType)
        {
            case 1:
                WalkTowardsClosestPlayer();
                break;


            default:
                break;
        }

        timer -= Time.deltaTime;
	}

    private GameObject FindClosestPlayer()
    {
        GameObject closest = null;
        float smallestDist = float.PositiveInfinity;
        foreach(GameObject p in players)
        {
            if (Vector3.Distance(p.transform.position, this.transform.position) < smallestDist)
                closest = p;
        }

        return closest;
    }

    private void WalkTowardsClosestPlayer()
    {
        Vector3 direction = ClosestPlayer.transform.position - this.transform.position;
        direction.Normalize();
        this.transform.position += direction * speed;
    }

}
