using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Bound"))
        {
            Destroy(gameObject);
        }
        if(other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
