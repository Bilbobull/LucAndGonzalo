using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject[] enemy;

    public float SpawnRad;
    public float spawnTime;
    private float timeToSpawn;

    private void Start()
    {
        timeToSpawn = spawnTime;
    }

    public void Update()
    {
        timeToSpawn -= Time.deltaTime;
        if(timeToSpawn <= 0)
        {
            timeToSpawn = spawnTime;
            // Get a random position in a radius
            Vector3 pos = transform.position + new Vector3(
                Random.Range(0, SpawnRad),
                0,
                Random.Range(0, SpawnRad)
            );
            // Get a random enemy
            GameObject e = enemy[Random.Range(0, enemy.Length)];

            Instantiate(e, pos, e.transform.rotation);
        }
    }



}
