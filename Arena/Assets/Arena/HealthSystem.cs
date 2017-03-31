using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour {

    public int MaxHealth = 5;
    private int Health = 0;
    private ScoreTracker score;

	// Use this for initialization
	void Start () {
        Health = MaxHealth;
        score = GameObject.Find("Canvas/Score_UI").GetComponent<ScoreTracker>();
    }
	
    public void SubstractHealth(int hp)
    {
        Health -= hp;
        if (Health <= 0)
        {
            if (this.tag == "Enemy")
                score.score += 5;

            Destroy(this.gameObject);
        }
    }

    public int GetHealth()
    {
        return Health;
    }

}
