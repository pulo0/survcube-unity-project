using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatSystem : MonoBehaviour
{
    //GameObjects variables
    public GameObject enemyPrefab;

    //Scripts variables
    private HealthSystem healthSystem;
    private Enemy enemy;

    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        enemy = enemyPrefab.GetComponent<Enemy>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H) && Input.GetKeyDown(KeyCode.P))
        {
            healthSystem.health = 1000000;
        }

        if(Input.GetKeyDown(KeyCode.K) && Input.GetKeyDown(KeyCode.I))
        {
            healthSystem.health = 0;
        }

        if(Input.GetKeyDown(KeyCode.E) && Input.GetKeyDown(KeyCode.N))
        {
            enemy.speed = 100;
        }

        if(Input.GetKeyDown(KeyCode.L) && Input.GetKeyDown(KeyCode.M))
        {
            enemy.speed = 1;
        }
    }
}
