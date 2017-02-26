using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject[] enemy;

    public float SpawnRad;
    public int BaseEnemyNum;
    public int EnemyIncrement;
    public int MaxEnemiesAtTime;
    public float EnemySpawnTime;

    private int Wave = -1;
    private List<GameObject> enemies = new List<GameObject>();
    private int EnemiesWave = 0;
    private float TimeForSpawn;

    private void Start()
    {
        TimeForSpawn = EnemySpawnTime;
    }

    public void Update()
    {
        // Remove all dead enemies
        enemies.RemoveAll((e) => !e);

        if(enemies.Count == 0 && EnemiesWave == 0)
        {
            ++Wave;
            EnemiesWave = BaseEnemyNum + Wave * EnemyIncrement;
        }

        if(enemies.Count < MaxEnemiesAtTime && EnemiesWave > 0)
        {
            TimeForSpawn -= Time.deltaTime;
            if (TimeForSpawn <= 0)
            {
                TimeForSpawn = EnemySpawnTime;
                // Get a random position in a radius
                Vector3 pos = transform.position + new Vector3(
                    Random.Range(0, SpawnRad),
                    0,
                    Random.Range(0, SpawnRad)
                );
                // Get a random enemy
                GameObject e = enemy[Random.Range(0, enemy.Length)];

                e = Instantiate(e, pos, e.transform.rotation);

                enemies.Add(e);
            }
            --EnemiesWave;
        }
    }



}
