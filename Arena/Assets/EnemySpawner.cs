using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {

    public GameObject[] enemy;

    public float SpawnRad;
    public int BaseEnemyNum;
    public int EnemyIncrement;
    public int MaxEnemiesAtTime;
    public float EnemySpawnTime;

    public int Wave = -1;
    private List<GameObject> enemies = new List<GameObject>();
    private int EnemiesWave = 0;
    private float TimeForSpawn;

    private const float NEXT_ROUND_TIMER = 3.0f;
    public float NextRoundTimer;

    public bool BetweenWaves = false;

    public BaseAbility[] powerups;
    public AbilityPickup abilitypickup;


    private void Start()
    {
        TimeForSpawn = EnemySpawnTime;
        NextRoundTimer = NEXT_ROUND_TIMER;
    }

    public void Update()
    {
        // Remove all dead enemies
        enemies.RemoveAll((e) => !e);

        if (enemies.Count == 0 && EnemiesWave == 0)
        {
            if (NextRoundTimer > 0.0f)
            {
                BetweenWaves = true;
                NextRoundTimer -= Time.deltaTime;
            }

            else
            {
                BetweenWaves = false;
                ++Wave;
                EnemiesWave = BaseEnemyNum + Wave * EnemyIncrement;
                NextRoundTimer = NEXT_ROUND_TIMER;

                if(Random.Range(1, 3) == 1)
                {
                    Vector3 pos = transform.position + new Vector3(Random.Range(-SpawnRad, SpawnRad), 0, Random.Range(-SpawnRad, SpawnRad));
                    // Get a random enemy 
                    AbilityPickup ap = abilitypickup;
                    ap.Ability = powerups[Random.Range(0, powerups.Length)];
                    ap = Instantiate(ap, pos, ap.transform.rotation);
                }
            }
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
                --EnemiesWave;
            }

        }
    }



}
