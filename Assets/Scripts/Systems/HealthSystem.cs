using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    //Float variables
    private float defaultSpeed = 5f;

    //Int variables
    public int health = 10;

    //Scripts variables
    private PlayerController playerCon;
    private Enemy enemyScript;
    private GameManager gameManager;
    private CheatSystem cheatSystem;

    //GameObjects variables
    public GameObject pointer;
    public GameObject enemyPrefab;

    //Particles variables
    public ParticleSystem bleedingPlayerParticle;
    public ParticleSystem bleedingPlayerOnPowerupParticle;

    void Start()
    {
        playerCon = GetComponent<PlayerController>();
        cheatSystem = GetComponent<CheatSystem>();
        enemyScript = enemyPrefab.gameObject.GetComponent<Enemy>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Enemy") && health >= 1)
       {
            health--;
            Destroy(other.gameObject);
            ParticlesOnPlayer();
            gameManager.HurtSound();
       }  
        if(other.gameObject.CompareTag("Enemy") && health == 0)
       {
           playerCon.gameOver = true;

           gameObject.SetActive(false);
           pointer.gameObject.SetActive(false);

           enemyScript.speed = defaultSpeed;

           gameManager.HurtSound(); 
           gameManager.UISetFalse(); 
           gameManager.GameOver();
           gameManager.StopMusic();
       }
    }
    void ParticlesOnPlayer()
    {
        if(!playerCon.hasPowerup)
        {
            Instantiate(bleedingPlayerParticle, transform.position, bleedingPlayerParticle.transform.rotation);
        }
        else 
        {
            Instantiate(bleedingPlayerOnPowerupParticle, transform.position, bleedingPlayerOnPowerupParticle.transform.rotation);
        }
        
    }
}
