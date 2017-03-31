using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveTextHandler : MonoBehaviour {
    public GameObject enemySpawner;
    public double TimeWaveDisplay;

    private EnemySpawner spawner;
    private Text text;
    private double WaveDisplayTime;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        spawner = enemySpawner.GetComponent<EnemySpawner>();
        WaveDisplayTime = TimeWaveDisplay;
    }
	
	// Update is called once per frame
	void Update () {
        if (spawner.BetweenWaves)
        {
            WaveDisplayTime = TimeWaveDisplay;
            text.text = "Time to Next Round: " + System.Math.Round(spawner.NextRoundTimer, 1).ToString();
        }

        else
        {
            if (WaveDisplayTime > 0)
            {
                text.text = "Round " + spawner.Wave.ToString();
                WaveDisplayTime -= Time.deltaTime;
            }
            else
                text.text = string.Empty;
        }
    }
}
