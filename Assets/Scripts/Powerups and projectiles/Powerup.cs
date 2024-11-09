using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private float speed = 50f;

    private PlayerController playerController;
    
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if(!playerController.gameOver)
        {
            transform.Rotate(Vector3.forward * speed * Time.deltaTime);
        }
        
    }
}
